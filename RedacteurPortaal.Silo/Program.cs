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

        private static async Task<ISiloHost> StartSilo()
        {
            var clusterId = "test";
            var serviceId = "test";
            if (IsDebug())
            {
                var builder = new SiloHostBuilder()
                   .UseLocalhostClustering()
                   .Configure<ClusterOptions>(options =>
                   {
                       options.ClusterId = clusterId;
                       options.ServiceId = serviceId;
                   })
                   .ConfigureLogging(logging => logging.AddConsole())
                   .ConfigureLogging(logging => logging.SetMinimumLevel(LogLevel.Warning));

                var host = builder.Build();
                await host.StartAsync();
                return host;
            }
            else
            {
                var builder = new SiloHostBuilder()
                            .Configure<ClusterOptions>(options =>
                            {
                                options.ClusterId = clusterId;
                                options.ServiceId = serviceId;
                            })
                            .ConfigureEndpoints(new Random(1).Next(30001, 30100), new Random(1).Next(20001, 20100), listenOnAnyHostAddress: true)
                            .UseKubeMembership()
                            .AddMemoryGrainStorageAsDefault()
                            .ConfigureLogging(logging => logging.AddConsole())
                            .ConfigureLogging(logging => logging.SetMinimumLevel(LogLevel.Warning));

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
