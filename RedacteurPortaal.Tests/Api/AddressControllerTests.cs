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
using RedacteurPortaal.Api.DTOs;
using RedacteurPortaal.Api.Models.Profile;
using RedacteurPortaal.Api.Models.Request;
using RedacteurPortaal.DomainModels.Profile;
using RedacteurPortaal.Helpers;
using Xunit;
using JsonConverter = Newtonsoft.Json.JsonConverter;

namespace RedacteurPortaal.Tests.Api;

public class AddressControllerTests
{

    [Fact]
    public async Task DefaultIsEmpty()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();
        var addresses = await client.GetFromJsonAsync<List<AddressDTO>>("/api/Address");

        Assert.NotNull(addresses);
        Assert.True(addresses?.Count == 0);
    }

    [Fact]
    public async Task CanAdd()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addAddressRequest = DtoBuilder.BuildAddAddressRequest();
        var addressResult = await client.PostAsJsonAsync("/api/Address", addAddressRequest);
        var resultString = await addressResult.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<AddressDTO>(resultString, new JsonSerializerOptions() {PropertyNameCaseInsensitive = true});

        Assert.NotNull(result);
        Assert.Equal(addAddressRequest.Address, result?.Address);
        Assert.Equal(addAddressRequest.Webpage, result?.Webpage);
        Assert.Equal(addAddressRequest.CompanyName, result?.CompanyName);
        Assert.Equal(addAddressRequest.EmailAddress, result?.EmailAddress);
        Assert.Equal(addAddressRequest.PhoneNumber, result?.PhoneNumber);
        Assert.Equal(addAddressRequest.PostalCode, result?.PostalCode);

        var newAddress = await client.GetFromJsonAsync<List<AddressDTO>>("/api/Address");

        Assert.NotNull(newAddress);
        Assert.True(newAddress?.Count == 1);
    }

    [Fact]
    public async Task CanUpdate()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addAddressRequest = DtoBuilder.BuildAddAddressRequest();
        var addressResult = await client.PostAsJsonAsync("/api/Address", addAddressRequest);
        var resultString = await addressResult.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<AddressDTO>(resultString, new JsonSerializerOptions() {PropertyNameCaseInsensitive = true});

        Assert.NotNull(result);
        Assert.Equal(addAddressRequest.CompanyName, result?.CompanyName);

        var patchAddressRequest = DtoBuilder.BuildPatchAddressRequest();
        var patchContent = new StringContent(JsonSerializer.Serialize(patchAddressRequest), Encoding.UTF8, "application/json");
        var newAddress = await client.PatchAsync($"/api/Address/{result?.Id}", patchContent );

        var patchResult = JsonSerializer.Deserialize<AddressDTO>(await newAddress.Content.ReadAsStringAsync(), new JsonSerializerOptions() {PropertyNameCaseInsensitive = true});
        Assert.Equal(HttpStatusCode.OK, newAddress.StatusCode);

        Assert.Equal(patchAddressRequest.Address, patchResult?.Address);
        Assert.Equal(patchAddressRequest.Webpage, patchResult?.Webpage);
        Assert.Equal(patchAddressRequest.CompanyName, patchResult?.CompanyName);
        Assert.Equal(patchAddressRequest.EmailAddress, patchResult?.EmailAddress);
        Assert.Equal(patchAddressRequest.PostalCode, patchResult?.PostalCode);
        Assert.Equal(patchAddressRequest.PhoneNumber, patchResult?.PhoneNumber);
    }

    [Fact]
    public async Task CanDelete()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addAddressRequest = DtoBuilder.BuildAddAddressRequest();
        var addressResult = await client.PostAsJsonAsync("/api/Address", addAddressRequest);
        var result = JsonSerializer.Deserialize<AddressDTO>(await addressResult.Content.ReadAsStringAsync(), new JsonSerializerOptions() {PropertyNameCaseInsensitive = true});

        var delete = await client.DeleteAsync($"/api/Address/{result?.Id}");
        Assert.Equal(HttpStatusCode.NoContent, delete.StatusCode);

        var newAddress = await client.GetFromJsonAsync<List<AddressDTO>>("/api/Address");
        Assert.Empty(newAddress);
    }

    [Fact]
    public async Task CanGetAddressByGuid()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addAddressRequest = DtoBuilder.BuildAddAddressRequest();
        var addressResult = await client.PostAsJsonAsync("/api/Address", addAddressRequest);
        var addResult = JsonSerializer.Deserialize<AddressDTO>(await addressResult.Content.ReadAsStringAsync(), new JsonSerializerOptions() {PropertyNameCaseInsensitive = true});

        var get = await client.GetAsync($"/api/Address/{addResult?.Id}");
        var getResult = JsonSerializer.Deserialize<AddressDTO>(await addressResult.Content.ReadAsStringAsync(), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        Assert.Equal(HttpStatusCode.OK, get.StatusCode);
        Assert.Equal(addResult?.Id, getResult?.Id);
        Assert.Equal(addResult?.PostalCode, getResult?.PostalCode);
        Assert.Equal(addResult?.PhoneNumber, getResult?.PhoneNumber);
        Assert.Equal(addResult?.CompanyName, getResult?.CompanyName);
        Assert.Equal(addResult?.EmailAddress, getResult?.EmailAddress);
    }

    [Fact]
    public async Task CanGetAll()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addAddressRequest = DtoBuilder.BuildAddAddressRequest();
        var addressResult = await client.PostAsJsonAsync("/api/Address", addAddressRequest);
        var addResult = JsonSerializer.Deserialize<AddressDTO>(await addressResult.Content.ReadAsStringAsync(), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        var addListRessresult = await client.GetAsync($"/api/Address");
        var listResult = JsonSerializer.Deserialize<List<AddressDTO>>(await addListRessresult.Content.ReadAsStringAsync(), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        Assert.Equal(HttpStatusCode.OK, addListRessresult.StatusCode);
        Assert.NotEmpty(listResult);
    }
}