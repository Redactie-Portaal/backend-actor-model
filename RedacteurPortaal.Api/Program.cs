using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Orleans.Hosting;
using RedacteurPortaal.Api;
using RedacteurPortaal.Api.Middleware;
using RedacteurPortaal.Data.Context;
using RedacteurPortaal.DomainModels.Adress;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.DomainModels.Profile;
using RedacteurPortaal.DomainModels.Shared;
using RedacteurPortaal.Grains.GrainInterfaces;
using RedacteurPortaal.Grains.GrainServices;
using Serilog.Sinks.Elasticsearch;
using Serilog.Exceptions;
using Serilog;
using RedacteurPortaal.Helpers;
using RedacteurPortaal.DomainModels.Agenda;

await Host.CreateDefaultBuilder(args)
    .UseOrleans((ctx, siloBuilder) => 
    {
        if (ctx.HostingEnvironment.IsDevelopment())
        {
            siloBuilder.UseLocalhostClustering();
            siloBuilder.AddMemoryGrainStorage("OrleansStorage");
        }
        else
        {
            // In Kubernetes, we use environment variables and the pod manifest
            siloBuilder.UseKubernetesHosting();

            // Use Redis for clustering & persistence
            var redisConnectionString = $"{Environment.GetEnvironmentVariable("REDIS")}:6379";
            var postgresqlConnString = Environment.GetEnvironmentVariable("POSTGRESQL");
            siloBuilder.UseRedisClustering(options => options.ConnectionString = redisConnectionString);

            siloBuilder.AddAdoNetGrainStorage(
                "OrleansStorage",
                options =>
                {
                    options.Invariant = "Npgsql";
                    options.UseJsonFormat = true;
                    options.ConnectionString = postgresqlConnString;
                });

            siloBuilder.ConfigureLogging(logging => logging.AddConsole());
        }
    })
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.ConfigureServices(services => services.AddControllers());
        webBuilder.ConfigureServices(services => services.AddSwaggerGen());
        webBuilder.Configure((ctx, app) => 
        {
            if (ctx.HostingEnvironment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            // global error handler
            app.UseMiddleware<ExceptionHandelingMiddleware>();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        });
    })
    .ConfigureServices((ctx, services) =>
    {
        services.AddSingleton<FileSystemProvider>();
        services.AddScoped<IExportPluginService, ExportPluginService>();
        services.AddScoped<FileSystemProvider>();
        services.AddScoped<IGrainManagementService<INewsItemGrain>, GrainManagementService<INewsItemGrain, NewsItemModel>>();
        services.AddScoped<IGrainManagementService<IProfileGrain>, GrainManagementService<IProfileGrain, Profile>>();
        services.AddScoped<IGrainManagementService<IAddressGrain>, GrainManagementService<IAddressGrain, AddressModel>>();
        services.AddScoped<IGrainManagementService<IAgendaGrain>, GrainManagementService<IAgendaGrain, AgendaModel>>();
        services.AddScoped<IGrainManagementService<IContactGrain>, GrainManagementService<IContactGrain, Contact>>();
        services.AddScoped<IGrainManagementService<IMediaAudioGrain>, GrainManagementService<IMediaAudioGrain, MediaAudioItem>>();
        services.AddScoped<IGrainManagementService<IMediaVideoGrain>, GrainManagementService<IMediaVideoGrain, MediaVideoItem>>();
        services.AddScoped<IGrainManagementService<IMediaPhotoGrain>, GrainManagementService<IMediaPhotoGrain, MediaPhotoItem>>();
        services.AddScoped<IGrainManagementService<ILocationGrain>, GrainManagementService<ILocationGrain, Location>>();

        services.AddDbContext<DataContext>(options => 
        {
            var connString = ctx.Configuration.GetConnectionString("DefaultConnection");
            options.UseNpgsql(connString);
            options.ConfigureWarnings(x =>
            {
                x.Ignore(CoreEventId.ManyServiceProvidersCreatedWarning);
            });
        });

        // migrate ef.
#pragma warning disable ASP0000 // Do not call 'IServiceCollection.BuildServiceProvider' in 'ConfigureServices'
        if (Environment.GetEnvironmentVariable("InTest") == null)
        {
            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<DataContext>();
                _ = context ?? throw new Exception("Failed to retrieve Database context");
                context.Database.Migrate();
            }
        }

        TypeAdapterConfig.GlobalSettings.Default.MapToConstructor(true);

#pragma warning restore ASP0000 // Do not call 'IServiceCollection.BuildServiceProvider' in 'ConfigureServices'
    })
      .UseSerilog((context, configuration) =>
      {
          configuration.Enrich.FromLogContext()
          .Enrich.WithMachineName()
          .WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Warning)
          .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(context.Configuration["ElasticConfiguration:Uri"]))
          {
              IndexFormat = $"{context.Configuration["ApplicationName"]}-logs-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
              AutoRegisterTemplate = true,
              NumberOfShards = 2,
              NumberOfReplicas = 1,
              MinimumLogEventLevel = Serilog.Events.LogEventLevel.Warning
          })
          .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
          .Enrich.WithExceptionDetails()
          .ReadFrom.Configuration(context.Configuration);
      })
    .RunConsoleAsync();