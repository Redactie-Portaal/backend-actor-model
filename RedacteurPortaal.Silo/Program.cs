using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Clustering.Kubernetes;
using Orleans.Configuration;
using Orleans.Hosting;

namespace RedacteurPortaal.Silo
{
    class Program
    {
        public static async Task Main()
        {
            try
            {
                Console.WriteLine("Starting silo...");
                var host = await StartSilo();
                Console.WriteLine("Press Enter to terminate...");
                await Task.Delay(int.MaxValue);
                Console.ReadLine();
                await host.StopAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static async Task<IHost> StartSilo()
        {
            var clusterId = "Test";
            var serviceId = "Test";
            if (IsDebug())
            {
                var builder = new HostBuilder()
                    .UseOrleans(builder =>
                    {
                        builder.UseLocalhostClustering()
                            .Configure<ClusterOptions>(options =>
                            {
                                options.ClusterId = clusterId;
                                options.ServiceId = serviceId;
                            })
                            .ConfigureLogging(logging => logging.AddConsole())
                            .ConfigureLogging(logging => logging.SetMinimumLevel(LogLevel.Warning));
                    });
                var host = builder.Build();
                await host.StartAsync();
                return host;
            }
            else
            {
                var builder = new HostBuilder()
                    .UseOrleans(builder =>
                    {
                        builder.UseKubernetesHosting()
                            .AddMemoryGrainStorageAsDefault()
                            .ConfigureLogging(logging => logging.AddConsole())
                            .ConfigureLogging(logging => logging.SetMinimumLevel(LogLevel.Warning));
                    });


                var host = builder.Build();
                await host.StartAsync();
                return host;
            }
            
        }

        public static bool IsDebug()
        {
#if DEBUG
            return true;
#else
        return false;
#endif
        }
    }
}