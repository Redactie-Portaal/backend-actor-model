using Orleans.TestingHost;
using RedacteurPortaal.DomainModels.Adress;
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
public class AddressGrainTests
{
    private readonly TestCluster _cluster;

    public AddressGrainTests(ClusterFixture fixture)
    {
        _cluster = fixture.Cluster;
    }

    [Fact]
    public async Task CanAddAddressCorrectly()
    {   
        var guid = Guid.NewGuid();

        var toSaveAddress = new AddressModel(guid, "Company", "Address", "PostalCode", "Phone", "Email", "Webpage");

        var addressGrain = this._cluster.GrainFactory.GetGrain<IAddressGrain>(guid);

        await addressGrain.UpdateAddress(toSaveAddress);

        var address = await addressGrain.Get();

        //var address = ClusterFixture.GrainStorage.GetGrainState<AddressModel>(addressGrain);

        Assert.Equal("Company", address.CompanyName);
        Assert.Equal(guid, address.Id);
    }
}

