using Orleans.TestingHost;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.Grains.GrainInterfaces;
using System;
using System.Threading.Tasks;
using Xunit;

namespace RedacteurPortaal.Tests.Grains.Test;

[Collection("Col")]
public class SourceGrainTests
{
    private readonly TestCluster _cluster;

    public SourceGrainTests(ClusterFixture fixture)
    {
        this._cluster = fixture.Cluster;
    }

    [Fact]
    public async Task CanAddSourceCorrectly()
    {   
        var guid = Guid.NewGuid();

        var toSaveSource = new Source(guid, "SourceName", "URI", DateTime.UtcNow);

        var sourceGrain = this._cluster.GrainFactory.GetGrain<ISourceGrain>(guid);

        await sourceGrain.Update(toSaveSource);

        var source = await sourceGrain.Get();

        Assert.Equal("SourceName", source.Name);
        Assert.Equal(guid, source.Id);
    }

}

