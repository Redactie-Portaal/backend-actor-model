using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using Microsoft.Extensions.Logging;

public class Program
{
    public static int Main(string[] args)
    {
        return RunMainAsync().Result;
    }

    private static async Task<int> RunMainAsync()
    {
        try
        {
            var host = await StartSilo();
            Console.WriteLine("\n\n Press Enter to terminate...\n\n");
            Console.ReadLine();

            await host.StopAsync();

            return 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return 1;
        }
    }

    private static async Task<ISiloHost> StartSilo()
    {
        // define the cluster configuration
        var builder = new SiloHostBuilder()
            .UseLocalhostClustering()
            .Configure<ClusterOptions>(options =>
            {
                options.ClusterId = "dev";
                options.ServiceId = "OrleansBasics";
            })
            .ConfigureLogging(logging => logging.AddConsole())
            .AddAdoNetGrainStorage("OrleansStorage", options =>
            {
                options.UseJsonFormat = true;
                options.ConnectionString = "Data Source=localhost;Initial Catalog=RedactiePortal;Persist Security Info=True;User ID=SA;Password=yourStrong(!)Password";
            })
            //.AddMemoryGrainStorage("OrleansStorage")
            .UseDashboard();

        var host = builder.Build();
        await host.StartAsync();
        return host;
    }
}