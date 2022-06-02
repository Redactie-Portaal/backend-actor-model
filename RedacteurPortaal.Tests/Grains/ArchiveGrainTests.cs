using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orleans.TestingHost;
using RedacteurPortaal.DomainModels.Archive;
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

        var toSaveArchive = new ArchiveModel(guid, "Title", "Label", new List<MediaPhotoItem>(), new List<MediaVideoItem>(), new List<MediaAudioItem>(), DateTime.UtcNow, new List<string> { "scripts" });

        var archiveGrain = this._cluster.GrainFactory.GetGrain<IArchiveGrain>(guid);
        await archiveGrain.Update(toSaveArchive);

        var updatedArchive = await this._cluster.GrainFactory.GetGrain<IArchiveGrain>(guid).Get();

        Assert.AreEqual(toSaveArchive.ArchivedDate, updatedArchive.ArchivedDate);
        Assert.AreEqual(toSaveArchive.Title, updatedArchive.Title);
        Assert.AreEqual(toSaveArchive.Label, updatedArchive.Label);
        CollectionAssert.AreEqual(toSaveArchive.MediaAudioItems, updatedArchive.MediaAudioItems);
        CollectionAssert.AreEqual(toSaveArchive.MediaVideoItems, updatedArchive.MediaVideoItems);
        CollectionAssert.AreEqual(toSaveArchive.MediaPhotoItems, updatedArchive.MediaPhotoItems);
        CollectionAssert.AreEqual(toSaveArchive.Scripts, updatedArchive.Scripts);
    }

}