using Export.Base;
using Orleans;
using Orleans.Hosting;
using RedacteurPortaal.Api;
using RedacteurPortaal.Api.Middleware;
using RedacteurPortaal.Grains.GrainInterfaces;
using RedacteurPortaal.Grains.Grains;
using System.Runtime.Loader;

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
            siloBuilder.AddAdoNetGrainStorage("OrleansStorage", options =>
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
    .ConfigureServices(services =>
    {
        services.AddSingleton<IExportPluginService, ExportPluginService>();
    })
    .RunConsoleAsync();
