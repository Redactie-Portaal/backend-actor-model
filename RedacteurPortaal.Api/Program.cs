using Orleans;
using Orleans.Clustering.Kubernetes;
using Orleans.Configuration;
using Polly;
using RedacteurPortaal.Api;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton(p => OrleansClient.ClusterClient);
builder.Services.AddControllers();
builder.Services.AddLogging(); 
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();

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
         if (app.Environment.IsDevelopment())
         {
             var builder = new ClientBuilder()
        .Configure<ClusterOptions>(c =>
        {
            c.ClusterId = "Test";
            c.ServiceId = "Test";
        })
        .UseLocalhostClustering()
        .ConfigureLogging(logging => logging.AddConsole());

             var client = builder.Build();
             client.Connect().Wait();
             return client;
         }
         else
         {
             var builder = new ClientBuilder()
        .UseKubeGatewayListProvider()
        .ConfigureLogging(logging => logging.AddConsole());

             var client = builder.Build();
             client.Connect().Wait();
             return client;
         }
        
     });

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
