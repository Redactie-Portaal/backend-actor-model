using System;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orleans.TestingHost;
using RedacteurPortaal.DomainModels.Adress;
using RedacteurPortaal.Grains.GrainInterfaces;

namespace RedacteurPortaal.Tests.Grains;

[TestClass]
public class AddressGrainTests
{
    private TestCluster _cluster;

    [TestInitialize]
    public void Initialize()
    {
        this._cluster = new ClusterFixture().Cluster;
    }


    [TestMethod]
    public async Task CanAddAddressCorrectly()
    {
        var guid = Guid.NewGuid();

        var toSaveAddress = new AddressModel(guid, "Company", "Address", "5050BB", "0612345678", "hans@gmail.com", "Webpage");

        var addressGrain = this._cluster.GrainFactory.GetGrain<IAddressGrain>(guid);

        await addressGrain.UpdateAddress(toSaveAddress);

        var address = await addressGrain.Get();

        Assert.AreEqual(guid, address.Id);
        Assert.AreEqual("Company", address.CompanyName);
        Assert.AreEqual("Address", address.Address);
        Assert.AreEqual("5050BB", address.PostalCode);
        Assert.AreEqual("0612345678", address.PhoneNumber);
        Assert.AreEqual("hans@gmail.com", address.EmailAddress);
        Assert.AreEqual("Webpage", address.Webpage);
    }

  [TestMethod] 
  public async Task GetAddressCorrect()
    {
        var guid = Guid.NewGuid();

        var toSaveAddress = new AddressModel(guid, "Company", "Address", "5050AA", "0612345678", "hans@gmail.com", "Webpage");

        var addressGrain = this._cluster.GrainFactory.GetGrain<IAddressGrain>(guid);

        await addressGrain.UpdateAddress(toSaveAddress);

        var Getaddress = await addressGrain.Get();

        Assert.AreEqual(Getaddress.Id, toSaveAddress.Id);
        Assert.AreEqual(Getaddress.PhoneNumber, toSaveAddress.PhoneNumber);
        Assert.AreEqual(Getaddress.CompanyName, toSaveAddress.CompanyName);
        Assert.AreEqual(Getaddress.PostalCode, toSaveAddress.PostalCode);
    }

    [TestMethod]
    public async Task CanUpdateAddressCorrect()
    {
        var guid = Guid.NewGuid();

        var toSaveAddress = new AddressModel(guid, "Company", "Address", "5050AA", "0612345678", "hans@gmail.com", "Webpage");

        var addressGrain = this._cluster.GrainFactory.GetGrain<IAddressGrain>(guid);

        await addressGrain.UpdateAddress(toSaveAddress);

        var updateAddres = new AddressModel(guid, "Sony", "beethovenlaan 20", "5050AA", "0612345678", "hans@gmail.com", "Webpage");
        
        await addressGrain.UpdateAddress(updateAddres);

        var updatedAddress = await addressGrain.Get();

        Assert.AreEqual("Sony", updatedAddress.CompanyName);
        Assert.AreEqual(guid, updatedAddress.Id);
        Assert.AreEqual("0612345678", updatedAddress.PhoneNumber);
        Assert.AreNotEqual(toSaveAddress.CompanyName, updatedAddress.CompanyName);
    }
  
    [TestMethod]
    public void ThrowsWhenModelPropIsNull()
    {
        var guid = Guid.NewGuid();

        Assert.ThrowsException<ValidationException>(() => {
            var updateAddres = new AddressModel(guid, "", "", "5050AA", "0612345678", "hans@gmail.com", "Webpage");
        });
    }

    [TestMethod] 
     public async Task DeleteCorrect()
    {
        var guid = Guid.NewGuid();

        var toSaveAddress = new AddressModel(guid, "Company", "Address", "5050AA", "0612345678", "hans@gmail.com", "Webpage");

        var addressGrain = this._cluster.GrainFactory.GetGrain<IAddressGrain>(guid);

        await addressGrain.UpdateAddress(toSaveAddress);

        await addressGrain.Delete();

        var deletedAddress = await addressGrain.Get();

        Assert.IsNull(deletedAddress.CompanyName);
        Assert.IsNull(deletedAddress.PhoneNumber);
        Assert.IsNull(deletedAddress.Webpage);
    }
}