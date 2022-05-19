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
public class MediaAudioGrainTests
{
    private readonly TestCluster _cluster;

    public MediaAudioGrainTests(ClusterFixture fixture)
    {
        _cluster = fixture.Cluster;
    }

    [Fact]
    public async Task CanAddMediaAudioItemCorrectly()
    {   
        var guid = Guid.NewGuid();

        var toSaveMediaAudioItem = new MediaAudioItem(guid,
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
                                                      new Uri("https://microsoft.com"),
                                                      TimeSpan.Zero,
                                                      Weather.SUNNY,
                                                      "Firstwords",
                                                      "Programname");

        var mediaAudioGrain = this._cluster.GrainFactory.GetGrain<IMediaAudioGrain>(guid);

        await mediaAudioGrain.Update(toSaveMediaAudioItem);

        var mediaAudioItem = await mediaAudioGrain.Get();

        Assert.Equal("Title", mediaAudioItem.Title);
        Assert.Equal(guid, mediaAudioItem.Id);
    }

}