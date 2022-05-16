using System;
using System.Threading.Tasks;
using Orleans.TestingHost;
using RedacteurPortaal.DomainModels.Adress;
using RedacteurPortaal.Grains.GrainInterfaces;
using Xunit;

namespace RedacteurPortaal.Tests.Grains.Test;

[Collection("Col")]
public class AddressGrainTests
{
    private readonly TestCluster _cluster;

    public AddressGrainTests(ClusterFixture fixture)
    {
        this._cluster = fixture.Cluster;
    }

    [Fact]
    public async Task CanAddAddressCorrectly()
    {
        var guid = Guid.NewGuid();

        var toSaveAddress = new AddressModel(guid, "Company", "Address", "PostalCode", "Phone", "Email", "Webpage");

        var addressGrain = this._cluster.GrainFactory.GetGrain<IAddressGrain>(guid);

        await addressGrain.UpdateAddress(toSaveAddress);

        var address = await addressGrain.Get();

        Assert.Equal("Company", address.CompanyName);
        Assert.Equal(guid, address.Id);
    }
}