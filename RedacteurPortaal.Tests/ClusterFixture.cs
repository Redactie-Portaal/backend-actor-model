using Microsoft.Extensions.DependencyInjection;
using Orleans.Hosting;
using Orleans.Storage;
using Orleans.TestingHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.Tests
{
    public class ClusterFixture : IDisposable
    {
        public ClusterFixture()
        {
            var builder = new TestClusterBuilder();
            builder.AddSiloBuilderConfigurator<TestSiloConfiguration>();
            this.Cluster = builder.Build();
            this.Cluster.Deploy();
        }

        public void Dispose()
        {
            this.Cluster.StopAllSilos();
        }

        public TestCluster Cluster { get; private set; }

        public static FakeGrainStorage GrainStorage { get; } = new();
        class TestSiloConfiguration : ISiloBuilderConfigurator
        {
            public void Configure(ISiloHostBuilder siloBuilder)
            {
                siloBuilder
                    .ConfigureServices(services => {
                             services
                             .AddSingleton<IGrainStorage>(GrainStorage);
                         })
                    .UseLocalhostClustering()
                    .AddMemoryGrainStorage("OrleansStorage");  
            }
        }
    }
}
