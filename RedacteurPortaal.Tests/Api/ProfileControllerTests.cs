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

public class ProfileControllerTests
{

    [Fact]
    public async Task DefaultIsEmpty()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();
        var profiles = await client.GetFromJsonAsync<List<ProfileDto>>("/api/Profile");

        Assert.NotNull(profiles);
        Assert.True(profiles?.Count == 0);
    }

    [Fact]
    public async Task CanAdd()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var profile = DtoBuilder.BuildAddProfileRequest();
        var profiles = await client.PostAsJsonAsync("/api/Profile", profile);
        var resultString = await profiles.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ProfileDto>(resultString, new JsonSerializerOptions() {PropertyNameCaseInsensitive = true});

        Assert.NotNull(result);
        Assert.Equal(profile.FullName, result?.FullName);
        Assert.Equal(profile.Role, result?.Role);
        Assert.Equal(profile.ContactDetails.Address, result?.ContactDetails.Address);
        Assert.Equal(profile.ContactDetails.City, result?.ContactDetails.City);
        Assert.Equal(profile.ContactDetails.Email, result?.ContactDetails.Email);
        Assert.Equal(profile.ContactDetails.PhoneNumber, result?.ContactDetails.PhoneNumber);
        Assert.Equal(profile.ContactDetails.Province, result?.ContactDetails.Province);
        Assert.Equal(profile.ContactDetails.PostalCode, result?.ContactDetails.PostalCode);
        Assert.Equal(profile.LastOnline, result?.LastOnline);
        Assert.Equal(profile.ProfilePicture, result?.ProfilePicture);

        var newProfiles = await client.GetFromJsonAsync<List<ProfileDto>>("/api/Profile");

        Assert.NotNull(newProfiles);
        Assert.True(newProfiles?.Count == 1);
    }

    [Fact]
    public async Task CanUpdate()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var profile = DtoBuilder.BuildAddProfileRequest();
        var profiles = await client.PostAsJsonAsync("/api/Profile", profile);
        var resultString = await profiles.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<ProfileDto>(resultString, new JsonSerializerOptions() {PropertyNameCaseInsensitive = true});

        Assert.NotNull(result);
        Assert.Equal(profile.FullName, result?.FullName);

        var patchProfile = DtoBuilder.BuildPatchProfileRequest();
        var patchContent = new StringContent(JsonSerializer.Serialize(patchProfile), Encoding.UTF8, "application/json");
        var newProfile = await client.PatchAsync($"/api/Profile/{result?.Id}",patchContent );

        var patchResult = JsonSerializer.Deserialize<ProfileDto>(await newProfile.Content.ReadAsStringAsync(), new JsonSerializerOptions() {PropertyNameCaseInsensitive = true});
        Assert.Equal(HttpStatusCode.OK, newProfile.StatusCode);

        Assert.Equal(patchProfile.Name, patchResult?.FullName);
        Assert.Equal(patchProfile.ProfilePicture, patchResult?.ProfilePicture);
        Assert.Equal(patchProfile.ContactDetails.Address, patchResult?.ContactDetails.Address);
        Assert.Equal(patchProfile.ContactDetails.City, patchResult?.ContactDetails.City);
        Assert.Equal(patchProfile.ContactDetails.Email, patchResult?.ContactDetails.Email);
        Assert.Equal(patchProfile.ContactDetails.PhoneNumber, patchResult?.ContactDetails.PhoneNumber);
        Assert.Equal(patchProfile.ContactDetails.Province, patchResult?.ContactDetails.Province);
        Assert.Equal(patchProfile.ContactDetails.PostalCode, patchResult?.ContactDetails.PostalCode);
    }

    [Fact]
    public async Task CantRemove()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var delete = await client.DeleteAsync($"/api/Profile/{Guid.NewGuid()}");
        Assert.Equal(HttpStatusCode.MethodNotAllowed, delete.StatusCode);
    }
}