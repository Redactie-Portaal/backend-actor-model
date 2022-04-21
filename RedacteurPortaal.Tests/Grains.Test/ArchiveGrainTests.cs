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
    public void CanAddArchiveCorrectly()
    {
        var guid = Guid.NewGuid();

        var toSaveArchive = new ArchiveModel(guid, "Title", "Label", new List<MediaPhotoItem> { }, new List<MediaVideoItem> { }, new List<MediaAudioItem> { }, DateTime.UtcNow, new List<string> { });

        var archiveGrain = this._cluster.GrainFactory.GetGrain<IArchiveGrain>(guid);

        //await archiveGrain

        //var source = ClusterFixture.GrainStorage.GetGrainState<Source>(sourceGrain);

        //Assert.Equal("SourceName", source.Name);
        //Assert.Equal(guid, source.Id);
    }

}

