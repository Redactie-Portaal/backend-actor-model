using Orleans.TestingHost;
using RedacteurPortaal.DomainModels.NewsItem;
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
public class ContactGrainTests
{
    private readonly TestCluster _cluster;

    public ContactGrainTests(ClusterFixture fixture)
    {
        _cluster = fixture.Cluster;
    }

    [Fact]
    public async Task CanAddContactCorrectly()
    {   
        var guid = Guid.NewGuid();

        var toSaveContact = new Contact(guid, "Name", "Email", "00");

        var contactGrain = this._cluster.GrainFactory.GetGrain<IContactGrain>(guid);

        await contactGrain.Update(toSaveContact);

        var contact = await contactGrain.Get();

        //var contact = ClusterFixture.GrainStorage.GetGrainState<Contact>(contactGrain);

        Assert.Equal("Name", contact.Name);
        Assert.Equal(guid, contact.Id);
    }

}

