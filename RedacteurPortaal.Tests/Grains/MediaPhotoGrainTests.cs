using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orleans.TestingHost;
using RedacteurPortaal.DomainModels.Media;
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
public class MediaPhotoGrainTests
{
    private TestCluster _cluster;

    [TestInitialize]
    public void Initialize()
    {
        this._cluster = new ClusterFixture().Cluster;
    }


    [TestMethod]
    public async Task CanAddMediaPhotoItemCorrectly()
    {   
        var guid = Guid.NewGuid();

        var toSaveMediaPhotoItem = new MediaPhotoItem(guid,
                                                      "Title",
                                                      "Folder",
                                                      DateTime.UtcNow,
                                                      "Rights",
                                                      "Camera",
                                                      "Lastwords",
                                                      "Proxyfile",
                                                      "Presentation",
                                                      new Location(guid, "Name", "City", "Province", "Street", "1000AB", 0, 90),
                                                      "Foramt",
                                                      new Uri("https://microsoft.com"),
                                                      "Image");

        var mediaPhotoGrain = this._cluster.GrainFactory.GetGrain<IMediaPhotoGrain>(guid);

        await mediaPhotoGrain.Update(toSaveMediaPhotoItem);

        var mediaPhotoItem = await mediaPhotoGrain.Get();

        Assert.AreEqual("Title", mediaPhotoItem.Title);
        Assert.AreEqual(guid, mediaPhotoItem.Id);
    }

}