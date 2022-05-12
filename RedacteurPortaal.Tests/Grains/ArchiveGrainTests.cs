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
        await archiveGrain.CreateArchive(toSaveArchive);

        var updatedArchive = await this._cluster.GrainFactory.GetGrain<IArchiveGrain>(guid).Get();

        Assert.Equal(toSaveArchive.ArchivedDate, updatedArchive.ArchivedDate);
        Assert.Equal(toSaveArchive.Title, updatedArchive.Title);
        Assert.Equal(toSaveArchive.Label, updatedArchive.Label);
        Assert.Equal(toSaveArchive.MediaAudioItems, updatedArchive.MediaAudioItems);
        Assert.Equal(toSaveArchive.MediaVideoItems, updatedArchive.MediaVideoItems);
        Assert.Equal(toSaveArchive.MediaPhotoItems, updatedArchive.MediaPhotoItems);
        Assert.Equal(toSaveArchive.Scripts, updatedArchive.Scripts);
    }

    [Fact]
    public async Task CanGetASingularArchiveCorrectly()
    {
        var guid = Guid.NewGuid();

        var toSaveArchive = new ArchiveModel(guid, "Title", "Label", new List<MediaPhotoItem> { }, new List<MediaVideoItem> { }, new List<MediaAudioItem> { }, new List<NewsItemModel> { }, DateTime.UtcNow, new List<string> { });
        var archiveGrain = this._cluster.GrainFactory.GetGrain<IArchiveGrain>(guid);
        await archiveGrain.CreateArchive(toSaveArchive);

        var archiveInGrain = await this._cluster.GrainFactory.GetGrain<IArchiveGrain>(guid).Get();

        Assert.NotNull(archiveInGrain);
    }

    [Fact]
    public async Task UpdateArchiveCorrectly()
    {
        var guid = Guid.NewGuid();

        var toSaveArchive = new ArchiveModel(guid, "Title", "Label", new List<MediaPhotoItem> { }, new List<MediaVideoItem> { }, new List<MediaAudioItem> { }, new List<NewsItemModel> { }, DateTime.UtcNow, new List<string> { });
        var archiveGrain = this._cluster.GrainFactory.GetGrain<IArchiveGrain>(guid);
        await archiveGrain.CreateArchive(toSaveArchive);

        var newerArchive = new ArchiveModel(guid, "Newer Title", "Newer Label", new List<MediaPhotoItem> { }, new List<MediaVideoItem> { }, new List<MediaAudioItem> { }, new List<NewsItemModel> { }, DateTime.UtcNow, new List<string> { });

        await archiveGrain.Update(newerArchive);


        var archiveInGrain = await this._cluster.GrainFactory.GetGrain<IArchiveGrain>(guid).Get();
        Assert.NotEqual(toSaveArchive.Title, archiveInGrain.Title);
        Assert.NotEqual(toSaveArchive.Label, archiveInGrain.Label);

    }

    [Fact]
    public async Task CanDeleteArchiveCorrectly()
    {
        var guid = Guid.NewGuid();

        var toSaveArchive = new ArchiveModel(guid, "Title", "Label", new List<MediaPhotoItem> { }, new List<MediaVideoItem> { }, new List<MediaAudioItem> { }, new List<NewsItemModel> { }, DateTime.UtcNow, new List<string> { });
        var archiveGrain = this._cluster.GrainFactory.GetGrain<IArchiveGrain>(guid);
        await archiveGrain.CreateArchive(toSaveArchive);

        await this._cluster.GrainFactory.GetGrain<IArchiveGrain>(guid).Delete();
        var archiveInGrain = await this._cluster.GrainFactory.GetGrain<IArchiveGrain>(guid).Get();

        Assert.Equal("00000000-0000-0000-0000-000000000000", Convert.ToString(archiveInGrain.Id));
        Assert.Null(archiveInGrain.Title);
        Assert.Null(archiveInGrain.Label);
    }
}

