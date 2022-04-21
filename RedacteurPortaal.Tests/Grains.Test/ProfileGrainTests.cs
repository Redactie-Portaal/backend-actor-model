using Orleans.TestingHost;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.DomainModels.Profile;
using RedacteurPortaal.DomainModels.Shared;
using RedacteurPortaal.Grains.GrainInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RedacteurPortaal.Tests.Grains.Test;

[Collection("Col")]
public class ProfileGrainTests
{
    private readonly TestCluster _cluster;

    public ProfileGrainTests(ClusterFixture fixture)
    {
        _cluster = fixture.Cluster;
    }

    [Fact]
    public async Task CanAddProfileCorrectly()
    {
        var guid = Guid.NewGuid();

        var toSaveProfile = new Profile(guid, "Joep Struikrover", new ContactDetails(), "Picture", Role.ADMIN, DateTime.UtcNow);

        var profilegrain = this._cluster.GrainFactory.GetGrain<IProfileGrain>(guid);

        await profilegrain.AddProfile(toSaveProfile);
        
        var profile = ClusterFixture.GrainStorage.GetGrainState<Profile>(profilegrain);

        Assert.Equal("Joep Struikrover", profile.FullName);
        Assert.Equal(Role.ADMIN, profile.Role);
        Assert.Equal(guid, profile.Id);
    }

}
