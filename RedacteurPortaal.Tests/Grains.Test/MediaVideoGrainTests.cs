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
using Xunit;

namespace RedacteurPortaal.Tests.Grains.Test;

[Collection("Col")]
public class MediaVideoGrainTests
{
    private readonly TestCluster _cluster;

    public MediaVideoGrainTests(ClusterFixture fixture)
    {
        _cluster = fixture.Cluster;
    }

    [Fact]
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
                                                      new Location { },
                                                      "Format",
                                                      "Reporter",
                                                      "Sound",
                                                      "Editor",
                                                      "Lastpicture",
                                                      new List<string> { },
                                                      "Voiceover",
                                                      "Description",
                                                      DateTime.UtcNow,
                                                      "Itemname",
                                                      "Epg",
                                                      TimeSpan.Zero,
                                                      "Archivematerial",
                                                      Weather.Sunny,
                                                      "Producer",
                                                      "Director",
                                                      new List<string> { },
                                                      "Firstpicture",
                                                      "Programname",
                                                      "Firstwords",
                                                      new Uri("https://microsoft.com"));

        var mediaVideoGrain = this._cluster.GrainFactory.GetGrain<IMediaVideoGrain>(guid);

        await mediaVideoGrain.Update(toSaveMediaVideoItem);

        var mediaVideoItem = ClusterFixture.GrainStorage.GetGrainState<MediaVideoItem>(mediaVideoGrain);

        Assert.Equal("Title", mediaVideoItem.Title);
        Assert.Equal(guid, mediaVideoItem.Id);
    }

}

