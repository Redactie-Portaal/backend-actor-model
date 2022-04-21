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
public class MediaPhotoGrainTests
{
    private readonly TestCluster _cluster;

    public MediaPhotoGrainTests(ClusterFixture fixture)
    {
        _cluster = fixture.Cluster;
    }

    [Fact]
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
                                                      new Location { },
                                                      "Foramt",
                                                      new Uri("https://microsoft.com"),
                                                      "Image");

        var mediaPhotoGrain = this._cluster.GrainFactory.GetGrain<IMediaPhotoGrain>(guid);

        await mediaPhotoGrain.Update(toSaveMediaPhotoItem);

        var mediaPhotoItem = await mediaPhotoGrain.Get();

        //var mediaPhotoItem = ClusterFixture.GrainStorage.GetGrainState<MediaPhotoItem>(mediaPhotoGrain);

        Assert.Equal("Title", mediaPhotoItem.Title);
        Assert.Equal(guid, mediaPhotoItem.Id);
    }

}

