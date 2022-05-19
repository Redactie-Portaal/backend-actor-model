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
using RedacteurPortaal.Api.Models;
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
    public async Task CanUpdate()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addArchiveRequest = DtoBuilder.BuildAddArchiveRequest();
        var archive = await client.PostAsJsonAsync("/api/Archive", addArchiveRequest);
        var resultString = await archive.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<ArchiveDto>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        Assert.NotNull(result);

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
    public async Task CanGetAllArchives()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var newArchive = await client.GetFromJsonAsync<List<ArchiveDto>>("/api/Archive");

        Assert.NotNull(newArchive);
        Assert.True(newArchive?.Count == 0);
    }

    [Fact]
    public async Task CanGetArchiveById()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addArchiveRequest = DtoBuilder.BuildAddArchiveRequest();
        var archiveResult = await client.PostAsJsonAsync("/api/Archive", addArchiveRequest);
        var resultString = await archiveResult.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ArchiveDto>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        var newArchive = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result.Id}");

        Assert.NotNull(newArchive);
        Assert.True(newArchive?.Id == result?.Id);
        Assert.True(newArchive?.Title == addArchiveRequest?.Title);
        Assert.True(newArchive?.Label == addArchiveRequest?.Label);
        Assert.Equal(newArchive?.MediaPhotoItems, addArchiveRequest?.MediaPhotoItems);
        Assert.Equal(newArchive?.MediaVideoItems, addArchiveRequest?.MediaVideoItems);
        Assert.Equal(newArchive?.MediaAudioItems, addArchiveRequest?.MediaAudioItems);
        Assert.Equal(newArchive?.NewsItems, addArchiveRequest?.NewsItems);
        Assert.True(newArchive?.ArchivedDate == addArchiveRequest?.ArchivedDate);
        Assert.Equal(newArchive?.Scripts, addArchiveRequest?.Scripts);
    }
    
    [Fact]
    public async Task CanGetVideoItems()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addArchiveRequest = DtoBuilder.BuildAddArchiveRequest();
        var archiveResult = await client.PostAsJsonAsync("/api/Archive", addArchiveRequest);
        var resultString = await archiveResult.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ArchiveDto>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        var videoItem = DtoBuilder.CreateMediaVideoItemRequest();
        var updatedArchiveResult = await client.PostAsJsonAsync<MediaVideoItemDto>($"/api/Archive/{result?.Id}/VideoItems", videoItem);
        
        var archiveWithVideos = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
        Assert.Equal(HttpStatusCode.Created, archiveResult.StatusCode);
        Assert.Equal(HttpStatusCode.OK, updatedArchiveResult.StatusCode);
        Assert.NotNull(archiveWithVideos);
        Assert.NotNull(archiveWithVideos?.MediaVideoItems);
        Assert.True(archiveWithVideos?.MediaVideoItems?.Count == 1);
        Assert.NotNull(archiveWithVideos?.MediaVideoItems?[0]);
    }

    [Fact]
    public async Task CanGetAudioItems()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addArchiveRequest = DtoBuilder.BuildAddArchiveRequest();
        var archiveResult = await client.PostAsJsonAsync("/api/Archive", addArchiveRequest);
        var resultString = await archiveResult.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ArchiveDto>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        var audioItem = DtoBuilder.CreateMediaAudioItemRequest();
        var updatedArchiveResult = await client.PostAsJsonAsync<MediaAudioItemDto>($"/api/Archive/{result?.Id}/AudioItems", audioItem);

        var archiveWithAudios = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
        Assert.Equal(HttpStatusCode.Created, archiveResult.StatusCode);
        Assert.Equal(HttpStatusCode.OK, updatedArchiveResult.StatusCode);
        Assert.NotNull(archiveWithAudios);
        Assert.NotNull(archiveWithAudios?.MediaAudioItems);
        Assert.True(archiveWithAudios?.MediaAudioItems?.Count == 1);
        Assert.NotNull(archiveWithAudios?.MediaAudioItems?[0]);
    }

    [Fact]
    public async Task CanGetPhotoItems()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addArchiveRequest = DtoBuilder.BuildAddArchiveRequest();
        var archiveResult = await client.PostAsJsonAsync("/api/Archive", addArchiveRequest);
        var resultString = await archiveResult.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ArchiveDto>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        var photoItem = DtoBuilder.CreateMediaPhotoItemRequest();
        var updatedArchiveResult = await client.PostAsJsonAsync<MediaPhotoItemDto>($"/api/Archive/{result?.Id}/PhotoItems", photoItem);

        var archiveWithPhotos = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
        Assert.Equal(HttpStatusCode.Created, archiveResult.StatusCode);
        Assert.Equal(HttpStatusCode.OK, updatedArchiveResult.StatusCode);
        Assert.NotNull(archiveWithPhotos);
        Assert.NotNull(archiveWithPhotos?.MediaPhotoItems);
        Assert.True(archiveWithPhotos?.MediaPhotoItems?.Count == 1);
        Assert.NotNull(archiveWithPhotos?.MediaPhotoItems?[0]);
    }

    [Fact]
    public async Task CanGetNewsItems()
    {
    }

    [Fact]
    public async Task CanGetVideoItemById()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addArchiveRequest = DtoBuilder.BuildAddArchiveRequest();
        var archiveResult = await client.PostAsJsonAsync("/api/Archive", addArchiveRequest);
        var resultString = await archiveResult.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ArchiveDto>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        var videoItem = DtoBuilder.CreateMediaVideoItemRequest();
        var archiveResultVideoSent = await client.PostAsJsonAsync<MediaVideoItemDto>($"/api/Archive/{result?.Id}/VideoItems", videoItem);
        var archiveWithVideos = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
        var updatedArchiveResult = await client.GetFromJsonAsync<MediaVideoItemDto>($"/api/Archive/{result?.Id}/VideoItems/{archiveWithVideos?.MediaVideoItems?[0].Id}");

        Assert.Equal(HttpStatusCode.Created, archiveResult.StatusCode);
        Assert.Equal(HttpStatusCode.OK, archiveResultVideoSent.StatusCode);
        Assert.NotNull(updatedArchiveResult);
        Assert.Equal(updatedArchiveResult?.Camera,archiveWithVideos?.MediaVideoItems?[0].Camera);
        Assert.Equal(updatedArchiveResult?.Description, archiveWithVideos?.MediaVideoItems?[0].Description);
        Assert.Equal(updatedArchiveResult?.EPG, archiveWithVideos?.MediaVideoItems?[0].EPG);
        Assert.Equal(updatedArchiveResult?.ItemName, archiveWithVideos?.MediaVideoItems?[0].ItemName);
        Assert.Equal(updatedArchiveResult?.Editor, archiveWithVideos?.MediaVideoItems?[0].Editor);
        Assert.Equal(updatedArchiveResult?.Director, archiveWithVideos?.MediaVideoItems?[0].Director);
        Assert.Equal(updatedArchiveResult?.DurationSeconds, archiveWithVideos?.MediaVideoItems?[0].DurationSeconds);
        Assert.Equal(updatedArchiveResult?.Guests, archiveWithVideos?.MediaVideoItems?[0].Guests);
        Assert.Equal(updatedArchiveResult?.Keywords, archiveWithVideos?.MediaVideoItems?[0].Keywords);
        Assert.Equal(updatedArchiveResult?.FirstPicture, archiveWithVideos?.MediaVideoItems?[0].FirstPicture);
        Assert.Equal(updatedArchiveResult?.FirstWords, archiveWithVideos?.MediaVideoItems?[0].FirstWords);
        Assert.Equal(updatedArchiveResult?.Format, archiveWithVideos?.MediaVideoItems?[0].Format);
        Assert.Equal(updatedArchiveResult?.Reporter, archiveWithVideos?.MediaVideoItems?[0].Reporter);
    }

    [Fact]
    public async Task CanGetAudioItemById()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addArchiveRequest = DtoBuilder.BuildAddArchiveRequest();
        var archiveResult = await client.PostAsJsonAsync("/api/Archive", addArchiveRequest);
        var resultString = await archiveResult.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ArchiveDto>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        var audioItem = DtoBuilder.CreateMediaAudioItemRequest();
        var archiveResultAudioSent = await client.PostAsJsonAsync<MediaAudioItemDto>($"/api/Archive/{result?.Id}/AudioItems", audioItem);
        var archiveWithAudios = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
        var updatedArchiveResult = await client.GetFromJsonAsync<MediaAudioItemDto>($"/api/Archive/{result?.Id}/AudioItems/{archiveWithAudios?.MediaAudioItems?[0].Id}");

        Assert.Equal(HttpStatusCode.Created, archiveResult.StatusCode);
        Assert.Equal(HttpStatusCode.OK, archiveResultAudioSent.StatusCode);
        Assert.NotNull(updatedArchiveResult);
        Assert.Equal(updatedArchiveResult?.DurationSeconds, archiveWithAudios?.MediaAudioItems?[0].DurationSeconds);
        Assert.Equal(updatedArchiveResult?.FirstWords, archiveWithAudios?.MediaAudioItems?[0].FirstWords);
        Assert.Equal(updatedArchiveResult?.ProgramName, archiveWithAudios?.MediaAudioItems?[0].ProgramName);
        Assert.Equal(updatedArchiveResult?.Weather, archiveWithAudios?.MediaAudioItems?[0].Weather);
    }

    [Fact]
    public async Task CanGetPhotoItemById()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addArchiveRequest = DtoBuilder.BuildAddArchiveRequest();
        var archiveResult = await client.PostAsJsonAsync("/api/Archive", addArchiveRequest);
        var resultString = await archiveResult.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ArchiveDto>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        var photoItem = DtoBuilder.CreateMediaPhotoItemRequest();
        var archiveResulsPhotoSent = await client.PostAsJsonAsync<MediaPhotoItemDto>($"/api/Archive/{result?.Id}/PhotoItems", photoItem);
        var archiveWithPhotos = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
        var updatedArchiveResult = await client.GetFromJsonAsync<MediaPhotoItemDto>($"/api/Archive/{result?.Id}/PhotoItems/{archiveWithPhotos?.MediaPhotoItems?[0].Id}");

        Assert.Equal(HttpStatusCode.Created, archiveResult.StatusCode);
        Assert.Equal(HttpStatusCode.OK, archiveResulsPhotoSent.StatusCode);
        Assert.NotNull(updatedArchiveResult);
        Assert.Equal(updatedArchiveResult?.Presentation, archiveWithPhotos?.MediaPhotoItems?[0].Presentation);
        Assert.Equal(updatedArchiveResult?.Camera, archiveWithPhotos?.MediaPhotoItems?[0].Presentation);
        Assert.Equal(updatedArchiveResult?.RepublishDate, archiveWithPhotos?.MediaPhotoItems?[0].RepublishDate);
        Assert.Equal(updatedArchiveResult?.Format, archiveWithPhotos?.MediaPhotoItems?[0].Format);
        Assert.Equal(updatedArchiveResult?.Location, archiveWithPhotos?.MediaPhotoItems?[0].Location);
        Assert.Equal(updatedArchiveResult?.MediaLocation, archiveWithPhotos?.MediaPhotoItems?[0].MediaLocation);
        Assert.Equal(updatedArchiveResult?.Folder, archiveWithPhotos?.MediaPhotoItems?[0].Folder);
        Assert.Equal(updatedArchiveResult?.Image, archiveWithPhotos?.MediaPhotoItems?[0].Image);
        Assert.Equal(updatedArchiveResult?.ProxyFile, archiveWithPhotos?.MediaPhotoItems?[0].ProxyFile);
        Assert.Equal(updatedArchiveResult?.Title, archiveWithPhotos?.MediaPhotoItems?[0].Title);
        Assert.Equal(updatedArchiveResult?.Rights, archiveWithPhotos?.MediaPhotoItems?[0].Rights);
        Assert.Equal(updatedArchiveResult?.LastWords, archiveWithPhotos?.MediaPhotoItems?[0].LastWords);
    }

    [Fact]
    public async Task CanGetNewsItemById()
    {

    }
    
    [Fact]
    public async Task CanCreateArchive()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addArchiveRequest = DtoBuilder.BuildAddArchiveRequest();
        var archiveResult = await client.PostAsJsonAsync("/api/Archive", addArchiveRequest);
        var resultString = await archiveResult.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ArchiveDto>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        Assert.NotNull(result);
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
    public async Task CanAddVideoItemById()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addArchiveRequest = DtoBuilder.BuildAddArchiveRequest();
        var archiveResult = await client.PostAsJsonAsync("/api/Archive", addArchiveRequest);
        var resultString = await archiveResult.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ArchiveDto>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        var videoItem = DtoBuilder.CreateMediaVideoItemRequest();
        var updatedArchiveResult = await client.PostAsJsonAsync<MediaVideoItemDto>($"/api/Archive/{result.Id}/VideoItems", videoItem);
        var archiveWithVideo = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result.Id}");
        Assert.Equal(HttpStatusCode.Created, archiveResult.StatusCode);
        Assert.Equal(HttpStatusCode.OK, updatedArchiveResult.StatusCode);
        Assert.NotNull(archiveWithVideo);
        Assert.NotEqual(addArchiveRequest?.MediaVideoItems, archiveWithVideo?.MediaVideoItems);
    }
    
    [Fact]
    public async Task CanAddAudioItemById()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addArchiveRequest = DtoBuilder.BuildAddArchiveRequest();
        var archiveResult = await client.PostAsJsonAsync("/api/Archive", addArchiveRequest);
        var resultString = await archiveResult.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ArchiveDto>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        var audioItem = DtoBuilder.CreateMediaAudioItemRequest();
        var updatedArchiveResult = await client.PostAsJsonAsync<MediaAudioItemDto>($"/api/Archive/{result?.Id}/AudioItems", audioItem);
        var archiveWithAudio = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
        Assert.Equal(HttpStatusCode.Created, archiveResult.StatusCode);
        Assert.Equal(HttpStatusCode.OK, updatedArchiveResult.StatusCode);
        Assert.NotNull(archiveWithAudio);
        Assert.NotEqual(addArchiveRequest?.MediaAudioItems, archiveWithAudio?.MediaAudioItems);
    }

    [Fact]
    public async Task CanAddPhotoItemById()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addArchiveRequest = DtoBuilder.BuildAddArchiveRequest();
        var archiveResult = await client.PostAsJsonAsync("/api/Archive", addArchiveRequest);
        var resultString = await archiveResult.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ArchiveDto>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        var photoItem = DtoBuilder.CreateMediaPhotoItemRequest();
        var updatedArchiveResult = await client.PostAsJsonAsync<MediaPhotoItemDto>($"/api/Archive/{result?.Id}/PhotoItems", photoItem);
        var archiveWithPhoto = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
        Assert.Equal(HttpStatusCode.Created, archiveResult.StatusCode);
        Assert.Equal(HttpStatusCode.OK, updatedArchiveResult.StatusCode);
        Assert.NotNull(archiveWithPhoto);
        Assert.NotEqual(addArchiveRequest?.MediaPhotoItems, archiveWithPhoto?.MediaPhotoItems);
    }

    [Fact]
    public async Task CanAddNewsItemById()
    {
    }

    [Fact]
    public async Task CanDeleteArchive()
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
    
    [Fact]
    public async Task CanDeleteVideoItemFromArchive()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addArchiveRequest = DtoBuilder.BuildAddArchiveRequest();
        var archiveResult = await client.PostAsJsonAsync("/api/Archive", addArchiveRequest);
        var resultString = await archiveResult.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ArchiveDto>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        var videoItem = DtoBuilder.CreateMediaVideoItemRequest();
        var archiveResultsVideoSent = await client.PostAsJsonAsync<MediaVideoItemDto>($"/api/Archive/{result?.Id}/VideoItems", videoItem);
        var archiveWithVideos = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
        var updatedArchiveResult = await client.GetFromJsonAsync<MediaVideoItemDto>($"/api/Archive/{result?.Id}/VideoItems/{archiveWithVideos?.MediaVideoItems?[0].Id}");
        Assert.Equal(HttpStatusCode.Created, archiveResult.StatusCode);
        Assert.Equal(HttpStatusCode.OK, archiveResultsVideoSent.StatusCode);
        Assert.NotNull(updatedArchiveResult);
        Assert.NotEmpty(archiveWithVideos?.MediaVideoItems);
        var deleteVideo = await client.DeleteAsync($"/api/Archive/{result?.Id}/VideoItems/{updatedArchiveResult?.Id}");
        var updatedArchiveResultAfterDeleting = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
        Assert.Equal(HttpStatusCode.OK, deleteVideo.StatusCode);
        Assert.Empty(updatedArchiveResultAfterDeleting?.MediaVideoItems);
    }

    [Fact]
    public async Task CanDeleteAudioItemFromArchive()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addArchiveRequest = DtoBuilder.BuildAddArchiveRequest();
        var archiveResult = await client.PostAsJsonAsync("/api/Archive", addArchiveRequest);
        var resultString = await archiveResult.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ArchiveDto>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        var audioItem = DtoBuilder.CreateMediaAudioItemRequest();
        var archiveResultsAudioSent = await client.PostAsJsonAsync<MediaAudioItemDto>($"/api/Archive/{result?.Id}/AudioItems", audioItem);
        var archiveWithAudios = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
        var updatedArchiveResult = await client.GetFromJsonAsync<MediaAudioItemDto>($"/api/Archive/{result?.Id}/AudioItems/{archiveWithAudios?.MediaAudioItems?[0].Id}");
        Assert.Equal(HttpStatusCode.Created, archiveResult.StatusCode);
        Assert.Equal(HttpStatusCode.OK, archiveResultsAudioSent.StatusCode);
        Assert.NotNull(updatedArchiveResult);
        Assert.NotEmpty(archiveWithAudios?.MediaAudioItems);
        var deleteAudio = await client.DeleteAsync($"/api/Archive/{result?.Id}/AudioItems/{updatedArchiveResult?.Id}");
        var updatedArchiveResultAfterDeleting = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
        Assert.Equal(HttpStatusCode.OK, deleteAudio.StatusCode);
        Assert.Empty(updatedArchiveResultAfterDeleting?.MediaAudioItems);
    }

    [Fact]
    public async Task CanDeletePhotoItemFromArchive()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addArchiveRequest = DtoBuilder.BuildAddArchiveRequest();
        var archiveResult = await client.PostAsJsonAsync("/api/Archive", addArchiveRequest);
        var resultString = await archiveResult.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ArchiveDto>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        var photoItem = DtoBuilder.CreateMediaPhotoItemRequest();
        var archiveResultsPhotoSent = await client.PostAsJsonAsync<MediaPhotoItemDto>($"/api/Archive/{result?.Id}/PhotoItems", photoItem);
        var archiveWithPhotos = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
        var updatedArchiveResult = await client.GetFromJsonAsync<MediaPhotoItemDto>($"/api/Archive/{result?.Id}/PhotoItems/{archiveWithPhotos?.MediaPhotoItems?[0].Id}");
        Assert.Equal(HttpStatusCode.Created, archiveResult.StatusCode);
        Assert.Equal(HttpStatusCode.OK, archiveResultsPhotoSent.StatusCode);
        Assert.NotNull(updatedArchiveResult);
        Assert.NotEmpty(archiveWithPhotos?.MediaPhotoItems);
        var deletePhoto = await client.DeleteAsync($"/api/Archive/{result?.Id}/PhotoItems/{updatedArchiveResult?.Id}");
        var updatedArchiveResultAfterDeleting = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
        Assert.Equal(HttpStatusCode.OK, deletePhoto.StatusCode);
        Assert.Empty(updatedArchiveResultAfterDeleting?.MediaPhotoItems);
    }

    [Fact]
    public async Task CanDeleteNewsItemFromArchive()
    {

    }
}