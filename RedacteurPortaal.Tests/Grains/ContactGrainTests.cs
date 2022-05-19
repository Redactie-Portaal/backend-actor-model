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

        var toSaveContact = new Contact(guid, "name", "email@email.com", "0612345678");

        var contactGrain = this._cluster.GrainFactory.GetGrain<IContactGrain>(guid);

        await contactGrain.Update(toSaveContact);

        var contact = await contactGrain.Get();

        Assert.Equal("name", contact.Name);
        Assert.Equal(guid, contact.Id);
    }

}

