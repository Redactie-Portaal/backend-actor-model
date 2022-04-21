using Orleans.TestingHost;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.DomainModels.Shared;
using RedacteurPortaal.Grains.GrainInterfaces;
using RedacteurPortaal.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RedacteurPortaal.Tests.Grains.Test;

[Collection("Col")]
public class LocationGrainTests
{
    private readonly TestCluster _cluster;

    public LocationGrainTests(ClusterFixture fixture)
    {
        _cluster = fixture.Cluster;
    }

    [Fact]
    public async Task CanAddLocationCorrectly()
    {   
        var guid = Guid.NewGuid();

        var toSaveLocation = new Location(guid, "Name", "City", "Province", "Street", "1020AB", 0, 0);

        var locationGrain = this._cluster.GrainFactory.GetGrain<ILocationGrain>(guid);

        await locationGrain.Update(toSaveLocation);

        var location = ClusterFixture.GrainStorage.GetGrainState<Location>(locationGrain);

        Assert.Equal("Name", location.Name);
        Assert.Equal(guid, location.Id);
    }

}

