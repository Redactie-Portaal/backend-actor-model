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
            var builder = new SiloHostBuilder()
                .UseKubeMembership()
                .Configure<ClusterOptions>(c =>
                {
                    c.ClusterId = "Test";
                    c.ServiceId = "TestService";
                })
                .Configure<EndpointOptions>(c =>
                {
                    c.AdvertisedIPAddress = System.Net.IPAddress.Loopback;
                });

            var host = builder.Build();
            await host.StartAsync();
            return host;
        }
    }
}
