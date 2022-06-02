using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedacteurPortaal.Api.DTOs;
using RedacteurPortaal.Api.Models.Profile;
using RedacteurPortaal.Api.Models.Request;
using RedacteurPortaal.DomainModels.Profile;
using RedacteurPortaal.Helpers;
using JsonConverter = Newtonsoft.Json.JsonConverter;

namespace RedacteurPortaal.Tests.Api;

[TestClass]
public class AddressControllerTests
{

    [TestMethod]
    public async Task DefaultIsEmpty()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();
        var addresses = await client.GetFromJsonAsync<List<AddressDTO>>("/api/Address");

        Assert.IsNotNull(addresses);
        Assert.IsTrue(addresses?.Count == 0);
    }

    [TestMethod]
    public async Task CanAdd()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addAddressRequest = DtoBuilder.BuildAddAddressRequest();
        var addressResult = await client.PostAsJsonAsync("/api/Address", addAddressRequest);
        var resultString = await addressResult.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<AddressDTO>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        Assert.IsNotNull(result);
        Assert.AreEqual(addAddressRequest.Address, result?.Address);
        Assert.AreEqual(addAddressRequest.Webpage, result?.Webpage);
        Assert.AreEqual(addAddressRequest.CompanyName, result?.CompanyName);
        Assert.AreEqual(addAddressRequest.EmailAddress, result?.EmailAddress);
        Assert.AreEqual(addAddressRequest.PhoneNumber, result?.PhoneNumber);
        Assert.AreEqual(addAddressRequest.PostalCode, result?.PostalCode);

        var newAddress = await client.GetFromJsonAsync<List<AddressDTO>>("/api/Address");

        Assert.IsNotNull(newAddress);
        Assert.IsTrue(newAddress?.Count == 1);
    }

    [TestMethod]
    public async Task CanUpdate()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addAddressRequest = DtoBuilder.BuildAddAddressRequest();
        var addressResult = await client.PostAsJsonAsync("/api/Address", addAddressRequest);
        var resultString = await addressResult.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<AddressDTO>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        Assert.IsNotNull(result);
        Assert.AreEqual(addAddressRequest.CompanyName, result?.CompanyName);

        var patchAddressRequest = DtoBuilder.BuildPatchAddressRequest();
        var patchContent = new StringContent(JsonSerializer.Serialize(patchAddressRequest), Encoding.UTF8, "application/json");
        var newAddress = await client.PatchAsync($"/api/Address/{result?.Id}", patchContent);

        var patchResult = JsonSerializer.Deserialize<AddressDTO>(await newAddress.Content.ReadAsStringAsync(), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        Assert.AreEqual(HttpStatusCode.OK, newAddress.StatusCode);

        Assert.AreEqual(patchAddressRequest.Address, patchResult?.Address);
        Assert.AreEqual(patchAddressRequest.Webpage, patchResult?.Webpage);
        Assert.AreEqual(patchAddressRequest.CompanyName, patchResult?.CompanyName);
        Assert.AreEqual(patchAddressRequest.EmailAddress, patchResult?.EmailAddress);
        Assert.AreEqual(patchAddressRequest.PostalCode, patchResult?.PostalCode);
        Assert.AreEqual(patchAddressRequest.PhoneNumber, patchResult?.PhoneNumber);
    }

    [TestMethod]
    public async Task CanDelete()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addAddressRequest = DtoBuilder.BuildAddAddressRequest();
        var addressResult = await client.PostAsJsonAsync("/api/Address", addAddressRequest);
        var result = JsonSerializer.Deserialize<AddressDTO>(await addressResult.Content.ReadAsStringAsync(), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        var delete = await client.DeleteAsync($"/api/Address/{result?.Id}");
        Assert.AreEqual(HttpStatusCode.NoContent, delete.StatusCode);

        var newAddress = await client.GetFromJsonAsync<List<AddressDTO>>("/api/Address");
        CollectionAssert.AllItemsAreNotNull(newAddress);
    }

    [TestMethod]
    public async Task CanGetAddressByGuid()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addAddressRequest = DtoBuilder.BuildAddAddressRequest();
        var addressResult = await client.PostAsJsonAsync("/api/Address", addAddressRequest);
        var addResult = JsonSerializer.Deserialize<AddressDTO>(await addressResult.Content.ReadAsStringAsync(), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        var get = await client.GetAsync($"/api/Address/{addResult?.Id}");
        var getResult = JsonSerializer.Deserialize<AddressDTO>(await addressResult.Content.ReadAsStringAsync(), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        Assert.AreEqual(HttpStatusCode.OK, get.StatusCode);
        Assert.AreEqual(addResult?.Id, getResult?.Id);
        Assert.AreEqual(addResult?.PostalCode, getResult?.PostalCode);
        Assert.AreEqual(addResult?.PhoneNumber, getResult?.PhoneNumber);
        Assert.AreEqual(addResult?.CompanyName, getResult?.CompanyName);
        Assert.AreEqual(addResult?.EmailAddress, getResult?.EmailAddress);
    }

    [TestMethod]
    public async Task CanGetAll()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addAddressRequest = DtoBuilder.BuildAddAddressRequest();
        var addressResult = await client.PostAsJsonAsync("/api/Address", addAddressRequest);
        var addResult = JsonSerializer.Deserialize<AddressDTO>(await addressResult.Content.ReadAsStringAsync(), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        var addListRessresult = await client.GetAsync($"/api/Address");
        var listResult = JsonSerializer.Deserialize<List<AddressDTO>>(await addListRessresult.Content.ReadAsStringAsync(), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        Assert.AreEqual(HttpStatusCode.OK, addListRessresult.StatusCode);
          CollectionAssert.AllItemsAreNotNull(listResult);
    }
}