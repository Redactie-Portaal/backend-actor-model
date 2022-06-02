using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orleans.TestingHost;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.Grains.GrainInterfaces;
using RedacteurPortaal.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.Tests.Grains;

[TestClass]
public class ContactGrainTests
{
    private TestCluster _cluster;

    [TestInitialize]
    public void Initialize()
    {
        this._cluster = new ClusterFixture().Cluster;
    }


    [TestMethod]
    public async Task CanAddContactCorrectly()
    {   
        var guid = Guid.NewGuid();

        var toSaveContact = new Contact(guid, "name", "email@email.com", "0612345678");

        var contactGrain = this._cluster.GrainFactory.GetGrain<IContactGrain>(guid);

        await contactGrain.Update(toSaveContact);

        var contact = await contactGrain.Get();

        Assert.AreEqual("name", contact.Name);
        Assert.AreEqual(guid, contact.Id);
    }

}

