using Orleans;
using Orleans.Configuration;
using Polly;
using RedacteurPortaal.Grains;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton(p => OrleansClient.ClusterClient);
var app = builder.Build();
var env = Environment.GetEnvironmentVariable("HostIP");
IPAddress.TryParse(env.Trim(), out var ip);

// Setup orleans client to use.
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
             c.ServiceId = "Test";
         })
         .UseStaticClustering(new IPEndPoint(ip, 30000))
         .ConfigureLogging(logging => logging.AddConsole());

         var client = builder.Build();
         client.Connect().Wait();
         return client;
     });

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Hello World!");

app.Run();
