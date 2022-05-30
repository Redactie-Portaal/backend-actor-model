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
public class ProfileControllerTests
{

    [TestMethod]
    public async Task DefaultIsEmpty()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();
        var profiles = await client.GetFromJsonAsync<List<ProfileDto>>("/api/Profile");

        Assert.IsNotNull(profiles);
        Assert.IsTrue(profiles?.Count == 0);
    }

    [TestMethod]
    public async Task CanAdd()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var profile = DtoBuilder.BuildAddProfileRequest();
        var profiles = await client.PostAsJsonAsync("/api/Profile", profile);
        var resultString = await profiles.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ProfileDto>(resultString, new JsonSerializerOptions() {PropertyNameCaseInsensitive = true});

        Assert.IsNotNull(result);
        Assert.AreEqual(profile.FullName, result?.FullName);
        Assert.AreEqual(profile.Role, result?.Role);
        Assert.AreEqual(profile.ContactDetails.Address, result?.ContactDetails.Address);
        Assert.AreEqual(profile.ContactDetails.City, result?.ContactDetails.City);
        Assert.AreEqual(profile.ContactDetails.Email, result?.ContactDetails.Email);
        Assert.AreEqual(profile.ContactDetails.PhoneNumber, result?.ContactDetails.PhoneNumber);
        Assert.AreEqual(profile.ContactDetails.Province, result?.ContactDetails.Province);
        Assert.AreEqual(profile.ContactDetails.PostalCode, result?.ContactDetails.PostalCode);
        Assert.AreEqual(profile.LastOnline, result?.LastOnline);
        Assert.AreEqual(profile.ProfilePicture, result?.ProfilePicture);

        var newProfiles = await client.GetFromJsonAsync<List<ProfileDto>>("/api/Profile");

        Assert.IsNotNull(newProfiles);
        Assert.IsTrue(newProfiles?.Count == 1);
    }

    [TestMethod]
    public async Task CanUpdate()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var profile = DtoBuilder.BuildAddProfileRequest();
        var profiles = await client.PostAsJsonAsync("/api/Profile", profile);
        var resultString = await profiles.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<ProfileDto>(resultString, new JsonSerializerOptions() {PropertyNameCaseInsensitive = true});

        Assert.IsNotNull(result);
        Assert.AreEqual(profile.FullName, result?.FullName);

        var patchProfile = DtoBuilder.BuildPatchProfileRequest();
        var patchContent = new StringContent(JsonSerializer.Serialize(patchProfile), Encoding.UTF8, "application/json");
        var newProfile = await client.PatchAsync($"/api/Profile/{result?.Id}",patchContent );

        var patchResult = JsonSerializer.Deserialize<ProfileDto>(await newProfile.Content.ReadAsStringAsync(), new JsonSerializerOptions() {PropertyNameCaseInsensitive = true});
        Assert.AreEqual(HttpStatusCode.OK, newProfile.StatusCode);

        Assert.AreEqual(patchProfile.Name, patchResult?.FullName);
        Assert.AreEqual(patchProfile.ProfilePicture, patchResult?.ProfilePicture);
        Assert.AreEqual(patchProfile.ContactDetails.Address, patchResult?.ContactDetails.Address);
        Assert.AreEqual(patchProfile.ContactDetails.City, patchResult?.ContactDetails.City);
        Assert.AreEqual(patchProfile.ContactDetails.Email, patchResult?.ContactDetails.Email);
        Assert.AreEqual(patchProfile.ContactDetails.PhoneNumber, patchResult?.ContactDetails.PhoneNumber);
        Assert.AreEqual(patchProfile.ContactDetails.Province, patchResult?.ContactDetails.Province);
        Assert.AreEqual(patchProfile.ContactDetails.PostalCode, patchResult?.ContactDetails.PostalCode);
    }

    [TestMethod]
    public async Task CantRemove()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var delete = await client.DeleteAsync($"/api/Profile/{Guid.NewGuid()}");
        Assert.AreEqual(HttpStatusCode.MethodNotAllowed, delete.StatusCode);
    }
}