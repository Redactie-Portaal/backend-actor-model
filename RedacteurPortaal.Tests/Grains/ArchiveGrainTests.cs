using Orleans.TestingHost;
using RedacteurPortaal.DomainModels.Archive;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.DomainModels.Media;
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
public class ArchiveGrainTests
{
    private readonly TestCluster _cluster;

    public ArchiveGrainTests(ClusterFixture fixture)
    {
        _cluster = fixture.Cluster;
    }

    [Fact]
    public async Task CanAddArchiveCorrectly()
    {
        var guid = Guid.NewGuid();

        var toSaveArchive = new ArchiveModel(guid, "Title", "Label", new List<MediaPhotoItem> { }, new List<MediaVideoItem> { }, new List<MediaAudioItem> { }, new List<NewsItemModel> { }, DateTime.UtcNow, new List<string> { });

        var archiveGrain = this._cluster.GrainFactory.GetGrain<IArchiveGrain>(guid);
        await archiveGrain.Update(toSaveArchive);

        var updatedArchive = await this._cluster.GrainFactory.GetGrain<IArchiveGrain>(guid).Get();

        Assert.Equal(toSaveArchive.ArchivedDate, updatedArchive.ArchivedDate);
        Assert.Equal(toSaveArchive.Title, updatedArchive.Title);
        Assert.Equal(toSaveArchive.Label, updatedArchive.Label);
        Assert.Equal(toSaveArchive.MediaAudioItems, updatedArchive.MediaAudioItems);
        Assert.Equal(toSaveArchive.MediaVideoItems, updatedArchive.MediaVideoItems);
        Assert.Equal(toSaveArchive.MediaPhotoItems, updatedArchive.MediaPhotoItems);
        Assert.Equal(toSaveArchive.Scripts, updatedArchive.Scripts);
    }
}

