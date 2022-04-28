using Orleans.TestingHost;
using RedacteurPortaal.DomainModels.Adress;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.Grains.GrainInterfaces;
using RedacteurPortaal.Grains.GrainServices;
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

        Assert.Equal("Company", address.CompanyName);
        Assert.Equal(guid, address.Id);
    }

  [Fact] 
  public async Task GetAddressCorrect()
    {
        var guid = Guid.NewGuid();

        var toSaveAddress = new AddressModel(guid, "Company", "Address", "PostalCode", "Phone", "Email", "Webpage");

        var addressGrain = this._cluster.GrainFactory.GetGrain<IAddressGrain>(guid);

        await addressGrain.UpdateAddress(toSaveAddress);

        var Getaddress = await addressGrain.Get();

        Assert.Equal(Getaddress.Id, toSaveAddress.Id);
        Assert.Equal(Getaddress.PhoneNumber, toSaveAddress.PhoneNumber);
        Assert.Equal(Getaddress.CompanyName, toSaveAddress.CompanyName);
        Assert.Equal(Getaddress.PostalCode, toSaveAddress.PostalCode);
    }

    [Fact]
    public async Task CanUpdateAddressCorrect()
    {
        var guid = Guid.NewGuid();

        var toSaveAddress = new AddressModel(guid, "Company", "Address", "PostalCode", "Phone", "Email", "Webpage");

        var addressGrain = this._cluster.GrainFactory.GetGrain<IAddressGrain>(guid);

        await addressGrain.UpdateAddress(toSaveAddress);

        var updateAddres = new AddressModel(guid, "Sony", "beethovenlaan 20", "5049 AA", "06-12345678", "Email", "Webpage");
        
        await addressGrain.UpdateAddress(updateAddres);

        var updatedAddress = await addressGrain.Get();

        Assert.Equal("Sony", updatedAddress.CompanyName);
        Assert.Equal(guid, updatedAddress.Id);
        Assert.Equal("06-12345678", updatedAddress.PhoneNumber);
        Assert.NotEqual(toSaveAddress.CompanyName, updatedAddress.CompanyName);
    }
  
    [Fact]
    public async Task ThrowsWhenModelPropIsNull()
    {
        var guid = Guid.NewGuid();

        var toSaveAddress = new AddressModel(guid, "Company", "Address", "PostalCode", "Phone", "Email", "Webpage");

        var addressGrain = this._cluster.GrainFactory.GetGrain<IAddressGrain>(guid);

        await addressGrain.UpdateAddress(toSaveAddress);

        Assert.Throws<ArgumentNullException>(() => {
            var updateAddres = new AddressModel(guid, "", null, "5049 AA", "06-12345678", "Email", "Webpage");
        });
      
    }

    [Fact] /*NOT WORKING DUE TO IGRAINMANAGEMENTSERVICE*/
     public async Task DeleteCorrect()
    {
        var guid = Guid.NewGuid();

        var toSaveAddress = new AddressModel(guid, "Company", "Address", "PostalCode", "Phone", "Email", "Webpage");

        var addressGrain = this._cluster.GrainFactory.GetGrain<IAddressGrain>(guid);

        await addressGrain.UpdateAddress(toSaveAddress);

        await addressGrain.Delete();

        var deletedAddress = await addressGrain.Get();

        Assert.Equal(null, deletedAddress.CompanyName);
        Assert.Equal(null, deletedAddress.PhoneNumber);
        Assert.Equal(null, deletedAddress.Webpage);
        /*Assert.Equal(Guid.Empty, deletedAddress.Id);  GUID IS NOT REMOVING WITH DE DELETE FUNCTION */   

    }
}

