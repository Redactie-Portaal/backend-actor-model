using Orleans.TestingHost;
using RedacteurPortaal.DomainModels.NewsItem;
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
public class NewsItemDescriptionGrainTests
{
    private readonly TestCluster _cluster;

    public NewsItemDescriptionGrainTests(ClusterFixture fixture)
    {
        _cluster = fixture.Cluster;
    }

    [Fact]
    public async Task CanAddSourceCorrectly()
    {   
        var guid = Guid.NewGuid();

        var toSaveSource = new Source(guid, "SourceName", "URI", DateTime.UtcNow);

        var sourceGrain = this._cluster.GrainFactory.GetGrain<ISourceGrain>(guid);

        await sourceGrain.AddSource(toSaveSource);

        var source = ClusterFixture.GrainStorage.GetGrainState<Source>(sourceGrain);

        Assert.Equal("SourceName", source.Name);
        Assert.Equal(guid, source.Id);
    }

}

