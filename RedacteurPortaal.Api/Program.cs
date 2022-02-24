using Orleans;
using Orleans.Clustering.Kubernetes;
using Orleans.Configuration;
using Polly;
using RedacteurPortaal.Grains;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


OrleansClient.ClusterClient = Policy<IClusterClient>.Handle<Exception>()
     .WaitAndRetry(new[]
     {
         TimeSpan.FromSeconds(1),
         TimeSpan.FromSeconds(2),
         TimeSpan.FromSeconds(3)
     })
     .Execute(() =>
     {
         var builder = new ClientBuilder()
         .Configure<ClusterOptions>(c =>
         {
             c.ClusterId = "Test";
             c.ServiceId = "TestService";
         })
         .UseKubeGatewayListProvider()
         .ConfigureLogging(logging => logging.AddConsole());

         var client = builder.Build();
         client.Connect().Wait();
         return client;
     });

builder.Services.AddSingleton(p => OrleansClient.ClusterClient);

app.MapGet("/", () => "Hello World!");

app.Run();
