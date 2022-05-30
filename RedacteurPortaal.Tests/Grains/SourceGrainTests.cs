using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orleans.TestingHost;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.Grains.GrainInterfaces;
using System;
using System.Threading.Tasks;

namespace RedacteurPortaal.Tests.Grains;

[TestClass]
public class SourceGrainTests
{
    private TestCluster _cluster;

    [TestInitialize]
    public void Initialize()
    {
        this._cluster = new ClusterFixture().Cluster;
    }


    [TestMethod]
    public async Task CanAddSourceCorrectly()
    {   
        var guid = Guid.NewGuid();

        var toSaveSource = new Source(guid, "SourceName", "URI", DateTime.UtcNow);

        var sourceGrain = this._cluster.GrainFactory.GetGrain<ISourceGrain>(guid);

        await sourceGrain.Update(toSaveSource);

        var source = await sourceGrain.Get();

        Assert.AreEqual("SourceName", source.Name);
        Assert.AreEqual(guid, source.Id);
    }

}

