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

public class ArchiveControllerTests
{

    [Fact]
    public async Task DefaultIsEmpty()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();
        var archives = await client.GetFromJsonAsync<List<ArchiveDto>>("/api/Archive");

        Assert.NotNull(archives);
        Assert.True(archives?.Count == 0);
    }

    [Fact]
    public async Task CanAdd()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addArchiveRequest = DtoBuilder.BuildAddArchiveRequest();
        var archiveResult = await client.PostAsJsonAsync("/api/Archive", addArchiveRequest);
        var resultString = await archiveResult.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ArchiveDto>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        Assert.NotNull(result);
        Assert.NotEqual(addArchiveRequest.Id, result?.Id);
        Assert.Equal(addArchiveRequest.Title, result?.Title);
        Assert.Equal(addArchiveRequest.Label, result?.Label);
        Assert.Equal(addArchiveRequest.MediaPhotoItems, result?.MediaPhotoItems);
        Assert.Equal(addArchiveRequest.MediaVideoItems, result?.MediaVideoItems);
        Assert.Equal(addArchiveRequest.MediaAudioItems, result?.MediaAudioItems);
        Assert.Equal(addArchiveRequest.NewsItems, result?.NewsItems);
        Assert.Equal(addArchiveRequest.ArchivedDate, result?.ArchivedDate);
        Assert.Equal(addArchiveRequest.Scripts, result?.Scripts);

        var newArchive = await client.GetFromJsonAsync<List<ArchiveDto>>("/api/Archive");

        Assert.NotNull(newArchive);
        Assert.True(newArchive?.Count == 1);
    }

    [Fact]
    public async Task CanUpdate()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addArchiveRequest = DtoBuilder.BuildAddArchiveRequest();
        var archive = await client.PostAsJsonAsync("/api/Archive", addArchiveRequest);
        var resultString = await archive.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<ArchiveDto>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        Assert.NotNull(result);
        Assert.NotEqual(addArchiveRequest.Id, result?.Id);

        var patchArchiveRequest = DtoBuilder.BuildUpdateArchiveRequest();
        var patchContent = new StringContent(JsonSerializer.Serialize(patchArchiveRequest), Encoding.UTF8, "application/json");
        var newArchive = await client.PatchAsync($"/api/Archive/{result?.Id}", patchContent);

        var patchResult = JsonSerializer.Deserialize<ArchiveDto>(await newArchive.Content.ReadAsStringAsync(), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        Assert.Equal(HttpStatusCode.OK, newArchive.StatusCode);
        
        Assert.Equal(patchArchiveRequest.Title, patchResult?.Title);
        Assert.Equal(patchArchiveRequest.Label, patchResult?.Label);
        Assert.Equal(patchArchiveRequest.MediaPhotoItems, patchResult?.MediaPhotoItems);
        Assert.Equal(patchArchiveRequest.MediaVideoItems, patchResult?.MediaVideoItems);
        Assert.Equal(patchArchiveRequest.MediaAudioItems, patchResult?.MediaAudioItems);
        Assert.Equal(patchArchiveRequest.NewsItems, patchResult?.NewsItems);
    }

    [Fact]
    public async Task CanDelete()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addArchiveRequest = DtoBuilder.BuildAddArchiveRequest();
        var archive = await client.PostAsJsonAsync("/api/Archive", addArchiveRequest);
        var result = JsonSerializer.Deserialize<ArchiveDto>(await archive.Content.ReadAsStringAsync(), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        var delete = await client.DeleteAsync($"/api/Archive/{result?.Id}");
        Assert.Equal(HttpStatusCode.OK, delete.StatusCode);

        var emptyArchive = await client.GetFromJsonAsync<List<AddressDTO>>("/api/Archive");
        Assert.Empty(emptyArchive);
    }
}