using Microsoft.VisualStudio.TestTools.UnitTesting;
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

namespace RedacteurPortaal.Tests.Grains;

[TestClass]
public class ProfileGrainTests
{
    private TestCluster _cluster;

    [TestInitialize]
    public void Initialize()
    {
        this._cluster = new ClusterFixture().Cluster;
    }


    [TestMethod]
    public async Task CanAddProfileCorrectly()
    {
        var guid = Guid.NewGuid();

        var toSaveProfile = new Profile(guid, "Joep Struikrover", new ContactDetails("email@email.com", "0612345678", "address", "province", "city", "1000AB"), "Picture", Role.ADMIN, DateTime.UtcNow);

        var profilegrain = this._cluster.GrainFactory.GetGrain<IProfileGrain>(guid);

        await profilegrain.Update(toSaveProfile);

        var profile = await profilegrain.Get();

        Assert.AreEqual("Joep Struikrover", profile.FullName);
        Assert.AreEqual(Role.ADMIN, profile.Role);
        Assert.AreEqual(guid, profile.Id);
    }

}
