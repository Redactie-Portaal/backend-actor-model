using Microsoft.VisualStudio.TestTools.UnitTesting;
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

namespace RedacteurPortaal.Tests.Grains;

[TestClass]
public class LocationGrainTests
{
    private TestCluster _cluster;

    [TestInitialize]
    public void Initialize()
    {
        this._cluster = new ClusterFixture().Cluster;
    }


    [TestMethod]
    public async Task CanAddLocationCorrectly()
    {   
        var guid = Guid.NewGuid();

        var toSaveLocation = new Location(guid, "Name", "City", "Province", "Street", "1000AB", 0, 90);

        var locationGrain = this._cluster.GrainFactory.GetGrain<ILocationGrain>(guid);

        await locationGrain.Update(toSaveLocation);

        var location = await locationGrain.Get();

        Assert.AreEqual("Name", location.Name);
        Assert.AreEqual(guid, location.Id);
    }

}

