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
public class MediaVideoGrainTests
{
    private TestCluster _cluster;

    [TestInitialize]
    public void Initialize()
    {
        this._cluster = new ClusterFixture().Cluster;
    }


    [TestMethod]
    public async Task CanAddVideoItemCorrectly()
    {
        var guid = Guid.NewGuid();

        var toSaveMediaVideoItem = new MediaVideoItem(guid,
                                                      "Title",
                                                      "Folder",
                                                      DateTime.UtcNow,
                                                      "Rights",
                                                      "Camera",
                                                      "Lastwords",
                                                      "Proxyfile",
                                                      "Presentation",
                                                      new Location(guid, "Name", "City", "Province", "Street", "1000AB", 0, 90),
                                                      "Format",
                                                      "Reporter",
                                                      "Sound",
                                                      "Editor",
                                                      "Lastpicture",
                                                      new List<string> { "keyword" },
                                                      "Voiceover",
                                                      "Description",
                                                      DateTime.UtcNow,
                                                      "Itemname",
                                                      "Epg",
                                                      TimeSpan.FromSeconds(10),
                                                      "Archivematerial",
                                                      Weather.SUNNY,
                                                      "Producer",
                                                      "Director",
                                                      new List<string> { "guest" },
                                                      "Firstpicture",
                                                      "Programname",
                                                      "Firstwords",
                                                      new Uri("https://microsoft.com"));

        var mediaVideoGrain = this._cluster.GrainFactory.GetGrain<IMediaVideoGrain>(guid);

        await mediaVideoGrain.Update(toSaveMediaVideoItem);

        var mediaVideoItem = await mediaVideoGrain.Get();

        Assert.AreEqual("Title", mediaVideoItem.Title);
        Assert.AreEqual(guid, mediaVideoItem.Id);
    }

}