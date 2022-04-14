using Export.Base;
using Microsoft.EntityFrameworkCore;
using Orleans;
using Orleans.Hosting;
using RedacteurPortaal.Api;
using RedacteurPortaal.Api.Middleware;
using RedacteurPortaal.Data.Context;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.DomainModels.Profile;
using RedacteurPortaal.Grains.GrainInterfaces;
using RedacteurPortaal.Grains.Grains;
using RedacteurPortaal.Grains.GrainServices;
using System.Runtime.Loader;

await Host.CreateDefaultBuilder(args)
    .UseOrleans((ctx, siloBuilder) =>
    {
        if (ctx.HostingEnvironment.IsDevelopment())
        {
            siloBuilder.UseLocalhostClustering();
            //var postgresqlConnString = Environment.GetEnvironmentVariable("POSTGRESQL");
            //siloBuilder.AddMemoryGrainStorage("OrleansStorage");
            var conn = ctx.Configuration.GetConnectionString("DefaultConnection");
            siloBuilder.AddAdoNetGrainStorage("OrleansStorage",
                options => {
                    options.Invariant = "Npgsql";
                    options.UseJsonFormat = true;
                    options.ConnectionString = conn;
                });

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
        services.AddScoped<IExportPluginService, ExportPluginService>();
        services.AddScoped< IGrainManagementService<INewsItemGrain>, GrainManagementService<INewsItemGrain, NewsItemModel>>();
        services.AddScoped<IGrainManagementService<IProfileGrain>, GrainManagementService<IProfileGrain, Profile>>();

        services.AddDbContext<DataContext>(options =>
        {
            var connString = ctx.Configuration.GetConnectionString("DefaultConnection");
            options.UseNpgsql(connString);
        });

        // migrate ef.
#pragma warning disable ASP0000 // Do not call 'IServiceCollection.BuildServiceProvider' in 'ConfigureServices'
        using (var scope = services.BuildServiceProvider().CreateScope())
        {
            var context = scope.ServiceProvider.GetService<DataContext>();
            _ = context ?? throw new Exception("Failed to retrieve Database context");
            context.Database.Migrate();
        }
#pragma warning restore ASP0000 // Do not call 'IServiceCollection.BuildServiceProvider' in 'ConfigureServices'
    })
    .RunConsoleAsync();