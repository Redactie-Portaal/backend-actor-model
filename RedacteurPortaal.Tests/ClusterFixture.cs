using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orleans;
using Orleans.Hosting;
using Orleans.Storage;
using Orleans.TestingHost;
using RedacteurPortaal.Api;
using RedacteurPortaal.Data.Context;
using RedacteurPortaal.DomainModels.Adress;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.DomainModels.Profile;
using RedacteurPortaal.DomainModels.Shared;
using RedacteurPortaal.Grains.GrainInterfaces;
using RedacteurPortaal.Grains.GrainServices;
using RedacteurPortaal.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.Tests;

public class ClusterFixture 
{
    public ClusterFixture()
    {
        var builder = new TestClusterBuilder();
        builder.AddSiloBuilderConfigurator<SiloConfigurator>();
        
        this.Cluster = builder.Build();
        this.Cluster.Deploy();
    }

    public void Dispose()
    {
        this.Cluster.StopAllSilos();
    }

    public TestCluster Cluster { get; private set; }

    public class SiloConfigurator : ISiloConfigurator
    {
        public void Configure(ISiloBuilder siloBuilder)
        {

            siloBuilder.AddMemoryGrainStorage("OrleansStorage");
            siloBuilder.ConfigureServices(services => {
                services.AddDbContext<DataContext>(options =>
                {
                    options.UseInMemoryDatabase("bababoey");
                });

                services.AddSingleton<FileSystemProvider>();
                services.AddScoped<IExportPluginService, ExportPluginService>();
                services.AddScoped<IGrainManagementService<INewsItemGrain>, GrainManagementService<INewsItemGrain, NewsItemModel>>();
                services.AddScoped<IGrainManagementService<IProfileGrain>, GrainManagementService<IProfileGrain, Profile>>();
                services.AddScoped<IGrainManagementService<IAddressGrain>, GrainManagementService<IAddressGrain, AddressModel>>();
                services.AddScoped<IGrainManagementService<IContactGrain>, GrainManagementService<IContactGrain, Contact>>();
                services.AddScoped<IGrainManagementService<IMediaAudioGrain>, GrainManagementService<IMediaAudioGrain, MediaAudioItem>>();
                services.AddScoped<IGrainManagementService<IMediaVideoGrain>, GrainManagementService<IMediaVideoGrain, MediaVideoItem>>();
                services.AddScoped<IGrainManagementService<IMediaPhotoGrain>, GrainManagementService<IMediaPhotoGrain, MediaPhotoItem>>();
                services.AddScoped<IGrainManagementService<ILocationGrain>, GrainManagementService<ILocationGrain, Location>>();
            });
        }
    }
}
