using Microsoft.VisualStudio.TestTools.UnitTesting;
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

namespace RedacteurPortaal.Tests.Grains;

[TestClass]
public class ArchiveGrainTests
{
    private TestCluster _cluster;

    [TestInitialize]
    public void Initialize()
    {
        this._cluster = new ClusterFixture().Cluster;
    }

    [TestMethod]
    public async Task CanAddArchiveCorrectly()
    {
        var guid = Guid.NewGuid();

        var toSaveArchive = new ArchiveModel(guid, "Title", "Label", new List<Guid> { }, new List<Guid> { }, new List<Guid> { }, new List<Guid> { }, DateTime.UtcNow, new List<string> { "scripts" });

        var archiveGrain = this._cluster.GrainFactory.GetGrain<IArchiveGrain>(guid);
        await archiveGrain.CreateArchive(toSaveArchive);

        var updatedArchive = await this._cluster.GrainFactory.GetGrain<IArchiveGrain>(guid).Get();

        Assert.AreNotEqual("00000000-0000-0000-0000-000000000000", Convert.ToString(updatedArchive.Id));
        Assert.AreEqual(toSaveArchive.ArchivedDate, updatedArchive.ArchivedDate);
        Assert.AreEqual(toSaveArchive.Title, updatedArchive.Title);
        Assert.AreEqual(toSaveArchive.Label, updatedArchive.Label);
        CollectionAssert.AreEqual(toSaveArchive.MediaAudioItems, updatedArchive.MediaAudioItems);
        CollectionAssert.AreEqual(toSaveArchive.MediaVideoItems, updatedArchive.MediaVideoItems);
        CollectionAssert.AreEqual(toSaveArchive.MediaPhotoItems, updatedArchive.MediaPhotoItems);
        CollectionAssert.AreEqual(toSaveArchive.Scripts, updatedArchive.Scripts);
    }

    [TestMethod]
    public async Task CanGetASingularArchiveCorrectly()
    {
        var guid = Guid.NewGuid();

        var toSaveArchive = new ArchiveModel(guid, "Title", "Label", new List<Guid> { }, new List<Guid> { }, new List<Guid> { }, new List<Guid> { }, DateTime.UtcNow, new List<string> { "scripts" });
        var archiveGrain = this._cluster.GrainFactory.GetGrain<IArchiveGrain>(guid);
        await archiveGrain.CreateArchive(toSaveArchive);

        var archiveInGrain = await this._cluster.GrainFactory.GetGrain<IArchiveGrain>(guid).Get();

        Assert.AreNotEqual("00000000-0000-0000-0000-000000000000", Convert.ToString(archiveInGrain.Id));
        Assert.IsNotNull(archiveInGrain.Title);
        Assert.IsNotNull(archiveInGrain.Label);
        Assert.IsNotNull(archiveInGrain.NewsItems);
        Assert.IsNotNull(archiveInGrain.Scripts);
        Assert.IsNotNull(archiveInGrain.MediaAudioItems);
        Assert.IsNotNull(archiveInGrain.MediaVideoItems);
        Assert.IsNotNull(archiveInGrain.MediaPhotoItems);
    }

    [TestMethod]
    public async Task UpdateArchiveCorrectly()
    {
        var guid = Guid.NewGuid();

        var toSaveArchive = new ArchiveModel(guid, "Title", "Label", new List<Guid> { }, new List<Guid> { }, new List<Guid> { }, new List<Guid> { }, DateTime.UtcNow, new List<string> { "scripts" });
        var archiveGrain = this._cluster.GrainFactory.GetGrain<IArchiveGrain>(guid);
        await archiveGrain.CreateArchive(toSaveArchive);

        var newerArchive = new ArchiveModel(guid, "Newer Title", "Newer Label", new List<Guid> { }, new List<Guid> { }, new List<Guid> { }, new List<Guid> { }, DateTime.UtcNow, new List<string> { "scripts" });

        await archiveGrain.Update(newerArchive);

        var archiveInGrain = await this._cluster.GrainFactory.GetGrain<IArchiveGrain>(guid).Get();
        Assert.AreNotEqual(toSaveArchive.Title, archiveInGrain.Title);
        Assert.AreNotEqual(toSaveArchive.Label, archiveInGrain.Label);
        Assert.AreNotEqual(toSaveArchive.NewsItems, archiveInGrain.NewsItems);
        Assert.AreNotEqual(toSaveArchive.Scripts, archiveInGrain.Scripts);
        Assert.AreNotEqual(toSaveArchive.ArchivedDate, archiveInGrain.ArchivedDate);
        Assert.AreNotEqual(toSaveArchive.MediaAudioItems, archiveInGrain.MediaAudioItems);
        Assert.AreNotEqual(toSaveArchive.MediaVideoItems, archiveInGrain.MediaVideoItems);
        Assert.AreNotEqual(toSaveArchive.MediaPhotoItems, archiveInGrain.MediaPhotoItems);
    }

    [TestMethod]
    public async Task CanDeleteArchiveCorrectly()
    {
        var guid = Guid.NewGuid();

        var toSaveArchive = new ArchiveModel(guid, "Title", "Label", new List<Guid> { }, new List<Guid> { }, new List<Guid> { }, new List<Guid> { }, DateTime.UtcNow, new List<string> { "scripts" });
        var archiveGrain = this._cluster.GrainFactory.GetGrain<IArchiveGrain>(guid);
        await archiveGrain.CreateArchive(toSaveArchive);

        await archiveGrain.Delete();

        Assert.IsFalse(await archiveGrain.HasState());
    }
}