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
using RedacteurPortaal.Api.Models;
using RedacteurPortaal.Api.Models.Profile;
using RedacteurPortaal.Api.Models.Request;
using RedacteurPortaal.DomainModels.Profile;
using RedacteurPortaal.Helpers;
using JsonConverter = Newtonsoft.Json.JsonConverter;

namespace RedacteurPortaal.Tests.Api;

[TestClass]
public class ArchiveControllerTests
{
    [TestMethod]
    public async Task DefaultIsEmpty()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();
        var archives = await client.GetFromJsonAsync<List<ArchiveDto>>("/api/Archive");

        Assert.IsNotNull(archives);
        Assert.IsTrue(archives?.Count == 0);
    }

    [TestMethod]
    public async Task CanUpdate()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addArchiveRequest = ArchiveDtoBuilder.BuildAddArchiveRequest();
        var archive = await client.PostAsJsonAsync("/api/Archive", addArchiveRequest);
        var resultString = await archive.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<ArchiveDto>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        Assert.IsNotNull(result);

        var patchArchiveRequest = ArchiveDtoBuilder.BuildUpdateArchiveRequest();
        var patchContent = new StringContent(JsonSerializer.Serialize(patchArchiveRequest), Encoding.UTF8, "application/json");
        var newArchive = await client.PatchAsync($"/api/Archive/{result?.Id}", patchContent);

        var patchResult = JsonSerializer.Deserialize<ArchiveDto>(await newArchive.Content.ReadAsStringAsync(), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        Assert.AreEqual(HttpStatusCode.OK, newArchive.StatusCode);

        Assert.AreEqual(patchArchiveRequest.Title, patchResult?.Title);
        Assert.AreEqual(patchArchiveRequest.Label, patchResult?.Label);
        Assert.AreEqual(patchArchiveRequest.MediaPhotoItems, patchResult?.MediaPhotoItems);
        Assert.AreEqual(patchArchiveRequest.MediaVideoItems, patchResult?.MediaVideoItems);
        Assert.AreEqual(patchArchiveRequest.MediaAudioItems, patchResult?.MediaAudioItems);
        Assert.AreEqual(patchArchiveRequest.NewsItems, patchResult?.NewsItems);
    }

    [TestMethod]
    public async Task CanGetAllArchives()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var newArchive = await client.GetFromJsonAsync<List<ArchiveDto>>("/api/Archive");

        Assert.IsNotNull(newArchive);
        Assert.IsTrue(newArchive?.Count == 0);
    }

    [TestMethod]
    public async Task CanGetArchiveById()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addArchiveRequest = ArchiveDtoBuilder.BuildSmallestArchive();
        var archiveResult = await client.PostAsJsonAsync("/api/Archive", addArchiveRequest);
        var resultString = await archiveResult.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ArchiveDto>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        var newArchive = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");

        Assert.IsNotNull(newArchive);
        Assert.IsTrue(newArchive?.Id == result?.Id);
        Assert.IsTrue(newArchive?.Title == addArchiveRequest?.Title);
        Assert.IsTrue(newArchive?.Label == addArchiveRequest?.Label);
        CollectionAssert.AreEqual(addArchiveRequest?.Scripts, newArchive?.Scripts);
    }

    [TestMethod]
    public async Task CanGetVideoItems()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addArchiveRequest = ArchiveDtoBuilder.BuildSmallestArchive();
        var archiveResult = await client.PostAsJsonAsync("/api/Archive", addArchiveRequest);
        var resultString = await archiveResult.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ArchiveDto>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        var videoItem = ArchiveDtoBuilder.CreateMediaVideoItemRequest();
        var updatedArchiveResult = await client.PostAsJsonAsync<MediaVideoItemDto>($"/api/Archive/{result?.Id}/VideoItems", videoItem);

        var archiveWithVideos = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
        Assert.AreEqual(HttpStatusCode.Created, archiveResult.StatusCode);
        Assert.AreEqual(HttpStatusCode.OK, updatedArchiveResult.StatusCode);
        Assert.IsNotNull(archiveWithVideos);
        Assert.IsNotNull(archiveWithVideos?.MediaVideoItems);
        Assert.AreEqual(1, archiveWithVideos?.MediaVideoItems?.Count);
        Assert.IsNotNull(archiveWithVideos?.MediaVideoItems?[0]);
    }

    [TestMethod]
    public async Task CanGetAudioItems()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addArchiveRequest = ArchiveDtoBuilder.BuildSmallestArchive();
        var archiveResult = await client.PostAsJsonAsync("/api/Archive", addArchiveRequest);
        var resultString = await archiveResult.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ArchiveDto>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        var audioItem = ArchiveDtoBuilder.CreateMediaAudioItemRequest();
        var updatedArchiveResult = await client.PostAsJsonAsync<MediaAudioItemDto>($"/api/Archive/{result?.Id}/AudioItems", audioItem);

        var archiveWithAudios = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
        Assert.AreEqual(HttpStatusCode.Created, archiveResult.StatusCode);
        Assert.AreEqual(HttpStatusCode.OK, updatedArchiveResult.StatusCode);
        Assert.IsNotNull(archiveWithAudios);
        Assert.IsNotNull(archiveWithAudios?.MediaAudioItems);
        Assert.AreEqual(1, archiveWithAudios?.MediaAudioItems?.Count);
        Assert.IsNotNull(archiveWithAudios?.MediaAudioItems?[0]);
    }

    [TestMethod]
    public async Task CanGetPhotoItems()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addArchiveRequest = ArchiveDtoBuilder.BuildSmallestArchive();
        var archiveResult = await client.PostAsJsonAsync("/api/Archive", addArchiveRequest);
        var resultString = await archiveResult.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ArchiveDto>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        var photoItem = ArchiveDtoBuilder.CreateMediaPhotoItemRequest();
        var updatedArchiveResult = await client.PostAsJsonAsync<MediaPhotoItemDto>($"/api/Archive/{result?.Id}/PhotoItems", photoItem);

        var archiveWithPhotos = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
        Assert.AreEqual(HttpStatusCode.Created, archiveResult.StatusCode);
        Assert.AreEqual(HttpStatusCode.OK, updatedArchiveResult.StatusCode);
        Assert.IsNotNull(archiveWithPhotos);
        Assert.IsNotNull(archiveWithPhotos?.MediaPhotoItems);
        Assert.AreEqual(1, archiveWithPhotos?.MediaPhotoItems?.Count);
        Assert.IsNotNull(archiveWithPhotos?.MediaPhotoItems?[0]);
    }

    [TestMethod]
    public async Task CanGetNewsItems()
    {
    }

    [TestMethod]
    public async Task CanGetVideoItemById()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addArchiveRequest = ArchiveDtoBuilder.BuildSmallestArchive();
        var archiveResult = await client.PostAsJsonAsync("/api/Archive", addArchiveRequest);
        var resultString = await archiveResult.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ArchiveDto>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        var videoItem = ArchiveDtoBuilder.CreateMediaVideoItemRequest();
        var archiveResultVideoSent = await client.PostAsJsonAsync<MediaVideoItemDto>($"/api/Archive/{result?.Id}/VideoItems", videoItem);
        var archiveWithVideos = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
        var updatedArchiveResult = await client.GetFromJsonAsync<MediaVideoItemDto>($"/api/Archive/{result?.Id}/VideoItems/{archiveWithVideos?.MediaVideoItems?[0].Id}");

        Assert.AreEqual(HttpStatusCode.Created, archiveResult.StatusCode);
        Assert.AreEqual(HttpStatusCode.OK, archiveResultVideoSent.StatusCode);
        Assert.IsNotNull(updatedArchiveResult);
        Assert.AreEqual(updatedArchiveResult?.Camera, archiveWithVideos?.MediaVideoItems?[0].Camera);
        Assert.AreEqual(updatedArchiveResult?.Description, archiveWithVideos?.MediaVideoItems?[0].Description);
        Assert.AreEqual(updatedArchiveResult?.EPG, archiveWithVideos?.MediaVideoItems?[0].EPG);
        Assert.AreEqual(updatedArchiveResult?.ItemName, archiveWithVideos?.MediaVideoItems?[0].ItemName);
        Assert.AreEqual(updatedArchiveResult?.Editor, archiveWithVideos?.MediaVideoItems?[0].Editor);
        Assert.AreEqual(updatedArchiveResult?.Director, archiveWithVideos?.MediaVideoItems?[0].Director);
        Assert.AreEqual(updatedArchiveResult?.DurationSeconds, archiveWithVideos?.MediaVideoItems?[0].DurationSeconds);
        CollectionAssert.AreEqual(updatedArchiveResult?.Guests, archiveWithVideos?.MediaVideoItems?[0].Guests);
        CollectionAssert.AreEqual(updatedArchiveResult?.Keywords, archiveWithVideos?.MediaVideoItems?[0].Keywords);
        Assert.AreEqual(updatedArchiveResult?.FirstPicture, archiveWithVideos?.MediaVideoItems?[0].FirstPicture);
        Assert.AreEqual(updatedArchiveResult?.FirstWords, archiveWithVideos?.MediaVideoItems?[0].FirstWords);
        Assert.AreEqual(updatedArchiveResult?.Format, archiveWithVideos?.MediaVideoItems?[0].Format);
        Assert.AreEqual(updatedArchiveResult?.Reporter, archiveWithVideos?.MediaVideoItems?[0].Reporter);
    }

    [TestMethod]
    public async Task CanGetAudioItemById()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addArchiveRequest = ArchiveDtoBuilder.BuildAddArchiveRequest();
        var archiveResult = await client.PostAsJsonAsync("/api/Archive", addArchiveRequest);
        var resultString = await archiveResult.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ArchiveDto>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        var audioItem = ArchiveDtoBuilder.CreateMediaAudioItemRequest();
        var archiveResultAudioSent = await client.PostAsJsonAsync<MediaAudioItemDto>($"/api/Archive/{result?.Id}/AudioItems", audioItem);
        var archiveWithAudios = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
        var updatedArchiveResult = await client.GetFromJsonAsync<MediaAudioItemDto>($"/api/Archive/{result?.Id}/AudioItems/{archiveWithAudios?.MediaAudioItems?[0].Id}");

        Assert.AreEqual(HttpStatusCode.Created, archiveResult.StatusCode);
        Assert.AreEqual(HttpStatusCode.OK, archiveResultAudioSent.StatusCode);
        Assert.IsNotNull(updatedArchiveResult);
        Assert.AreEqual(updatedArchiveResult?.DurationSeconds, archiveWithAudios?.MediaAudioItems?[0].DurationSeconds);
        Assert.AreEqual(updatedArchiveResult?.FirstWords, archiveWithAudios?.MediaAudioItems?[0].FirstWords);
        Assert.AreEqual(updatedArchiveResult?.ProgramName, archiveWithAudios?.MediaAudioItems?[0].ProgramName);
        Assert.AreEqual(updatedArchiveResult?.Weather, archiveWithAudios?.MediaAudioItems?[0].Weather);
    }

    [TestMethod]
    public async Task CanGetPhotoItemById()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addArchiveRequest = ArchiveDtoBuilder.BuildAddArchiveRequest();
        var archiveResult = await client.PostAsJsonAsync("/api/Archive", addArchiveRequest);
        var resultString = await archiveResult.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ArchiveDto>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        var photoItem = ArchiveDtoBuilder.CreateMediaPhotoItemRequest();
        var archiveResulsPhotoSent = await client.PostAsJsonAsync<MediaPhotoItemDto>($"/api/Archive/{result?.Id}/PhotoItems", photoItem);
        var archiveWithPhotos = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
        var updatedArchiveResult = await client.GetFromJsonAsync<MediaPhotoItemDto>($"/api/Archive/{result?.Id}/PhotoItems/{archiveWithPhotos?.MediaPhotoItems?[0].Id}");

        Assert.AreEqual(HttpStatusCode.Created, archiveResult.StatusCode);
        Assert.AreEqual(HttpStatusCode.OK, archiveResulsPhotoSent.StatusCode);
        Assert.IsNotNull(updatedArchiveResult);
        Assert.AreEqual(updatedArchiveResult?.Presentation, archiveWithPhotos?.MediaPhotoItems?[0].Presentation);
        Assert.AreEqual(updatedArchiveResult?.Camera, archiveWithPhotos?.MediaPhotoItems?[0].Presentation);
        Assert.AreEqual(updatedArchiveResult?.RepublishDate, archiveWithPhotos?.MediaPhotoItems?[0].RepublishDate);
        Assert.AreEqual(updatedArchiveResult?.Format, archiveWithPhotos?.MediaPhotoItems?[0].Format);
        Assert.AreEqual(updatedArchiveResult?.Location, archiveWithPhotos?.MediaPhotoItems?[0].Location);
        Assert.AreEqual(updatedArchiveResult?.MediaLocation, archiveWithPhotos?.MediaPhotoItems?[0].MediaLocation);
        Assert.AreEqual(updatedArchiveResult?.Folder, archiveWithPhotos?.MediaPhotoItems?[0].Folder);
        Assert.AreEqual(updatedArchiveResult?.Image, archiveWithPhotos?.MediaPhotoItems?[0].Image);
        Assert.AreEqual(updatedArchiveResult?.ProxyFile, archiveWithPhotos?.MediaPhotoItems?[0].ProxyFile);
        Assert.AreEqual(updatedArchiveResult?.Title, archiveWithPhotos?.MediaPhotoItems?[0].Title);
        Assert.AreEqual(updatedArchiveResult?.Rights, archiveWithPhotos?.MediaPhotoItems?[0].Rights);
        Assert.AreEqual(updatedArchiveResult?.LastWords, archiveWithPhotos?.MediaPhotoItems?[0].LastWords);
    }

    [TestMethod]
    public async Task CanGetNewsItemById()
    {

    }

    [TestMethod]
    public async Task CanCreateArchive()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addArchiveRequest = ArchiveDtoBuilder.BuildAddArchiveRequest();
        var archiveResult = await client.PostAsJsonAsync("/api/Archive", addArchiveRequest);
        var resultString = await archiveResult.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ArchiveDto>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        Assert.IsNotNull(result);
        Assert.AreEqual(addArchiveRequest.Title, result?.Title);
        Assert.AreEqual(addArchiveRequest.Label, result?.Label);
        Assert.AreEqual(addArchiveRequest.MediaPhotoItems, result?.MediaPhotoItems);
        Assert.AreEqual(addArchiveRequest.MediaVideoItems, result?.MediaVideoItems);
        Assert.AreEqual(addArchiveRequest.MediaAudioItems, result?.MediaAudioItems);
        Assert.AreEqual(addArchiveRequest.NewsItems, result?.NewsItems);
        Assert.AreEqual(addArchiveRequest.ArchivedDate, result?.ArchivedDate);
        Assert.AreEqual(addArchiveRequest.Scripts, result?.Scripts);

        var newArchive = await client.GetFromJsonAsync<List<ArchiveDto>>("/api/Archive");

        Assert.IsNotNull(newArchive);
        Assert.IsTrue(newArchive?.Count == 1);
    }

    [TestMethod]
    public async Task CanAddVideoItemById()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addArchiveRequest = ArchiveDtoBuilder.BuildSmallestArchive();
        var archiveResult = await client.PostAsJsonAsync("/api/Archive", addArchiveRequest);
        var resultString = await archiveResult.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ArchiveDto>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        var videoItem = ArchiveDtoBuilder.CreateMediaVideoItemRequest();
        var updatedArchiveResult = await client.PostAsJsonAsync<MediaVideoItemDto>($"/api/Archive/{result?.Id}/VideoItems", videoItem);
        var archiveWithVideo = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
        
        Assert.AreEqual(HttpStatusCode.Created, archiveResult.StatusCode);
        Assert.AreEqual(HttpStatusCode.OK, updatedArchiveResult.StatusCode);
        Assert.IsNotNull(archiveWithVideo);
        Assert.AreNotEqual(addArchiveRequest?.MediaVideoItems, archiveWithVideo?.MediaVideoItems);
    }

    [TestMethod]
    public async Task CanAddAudioItemById()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addArchiveRequest = ArchiveDtoBuilder.BuildSmallestArchive();
        var archiveResult = await client.PostAsJsonAsync("/api/Archive", addArchiveRequest);
        var resultString = await archiveResult.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ArchiveDto>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        var audioItem = ArchiveDtoBuilder.CreateMediaAudioItemRequest();
        var updatedArchiveResult = await client.PostAsJsonAsync<MediaAudioItemDto>($"/api/Archive/{result?.Id}/AudioItems", audioItem);
        var archiveWithAudio = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
        Assert.AreEqual(HttpStatusCode.Created, archiveResult.StatusCode);
        Assert.AreEqual(HttpStatusCode.OK, updatedArchiveResult.StatusCode);
        Assert.IsNotNull(archiveWithAudio);
        Assert.AreNotEqual(addArchiveRequest?.MediaAudioItems, archiveWithAudio?.MediaAudioItems);
    }

    [TestMethod]
    public async Task CanAddPhotoItemById()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addArchiveRequest = ArchiveDtoBuilder.BuildSmallestArchive();
        var archiveResult = await client.PostAsJsonAsync("/api/Archive", addArchiveRequest);
        var resultString = await archiveResult.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ArchiveDto>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        var photoItem = ArchiveDtoBuilder.CreateMediaPhotoItemRequest();
        var updatedArchiveResult = await client.PostAsJsonAsync<MediaPhotoItemDto>($"/api/Archive/{result?.Id}/PhotoItems", photoItem);
        var archiveWithPhoto = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
        Assert.AreEqual(HttpStatusCode.Created, archiveResult.StatusCode);
        Assert.AreEqual(HttpStatusCode.OK, updatedArchiveResult.StatusCode);
        Assert.IsNotNull(archiveWithPhoto);
        Assert.AreNotEqual(addArchiveRequest?.MediaPhotoItems, archiveWithPhoto?.MediaPhotoItems);
    }

    [TestMethod]
    public async Task CanAddNewsItemById()
    {
    }

    [TestMethod]
    public async Task CanDeleteArchive()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addArchiveRequest = ArchiveDtoBuilder.BuildAddArchiveRequest();
        var archive = await client.PostAsJsonAsync("/api/Archive", addArchiveRequest);
        var result = JsonSerializer.Deserialize<ArchiveDto>(await archive.Content.ReadAsStringAsync(), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        var delete = await client.DeleteAsync($"/api/Archive/{result?.Id}");
        Assert.AreEqual(HttpStatusCode.OK, delete.StatusCode);

        var emptyArchive = await client.GetFromJsonAsync<List<ArchiveDto>>("/api/Archive");
        Assert.IsTrue(emptyArchive?.Count == 0);
    }

    [TestMethod]
    public async Task CanDeleteVideoItemFromArchive()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addArchiveRequest = ArchiveDtoBuilder.BuildSmallestArchive();
        var archiveResult = await client.PostAsJsonAsync("/api/Archive", addArchiveRequest);
        var resultString = await archiveResult.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ArchiveDto>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        
        var videoItem = ArchiveDtoBuilder.CreateMediaVideoItemRequest();
        var archiveResultsVideoSent = await client.PostAsJsonAsync<MediaVideoItemDto>($"/api/Archive/{result?.Id}/VideoItems", videoItem);

        var archiveWithVideos = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
        var updatedArchiveResult = archiveWithVideos?.MediaVideoItems?[0];
        
        Assert.AreEqual(HttpStatusCode.Created, archiveResult.StatusCode);
        Assert.AreEqual(HttpStatusCode.OK, archiveResultsVideoSent.StatusCode);
        Assert.IsNotNull(updatedArchiveResult);
        CollectionAssert.AllItemsAreNotNull(archiveWithVideos?.MediaVideoItems);
        
        var deleteVideo = await client.DeleteAsync($"/api/Archive/{result?.Id}/VideoItems/{updatedArchiveResult?.Id}");
        var updatedArchiveResultAfterDeleting = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
        Assert.AreEqual(HttpStatusCode.OK, deleteVideo.StatusCode);
        Assert.AreEqual(0, updatedArchiveResultAfterDeleting?.MediaVideoItems?.Count);
    }

    [TestMethod]
    public async Task CanDeleteAudioItemFromArchive()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addArchiveRequest = ArchiveDtoBuilder.BuildAddArchiveRequest();
        var archiveResult = await client.PostAsJsonAsync("/api/Archive", addArchiveRequest);
        var resultString = await archiveResult.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ArchiveDto>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        
        
        var audioItem = ArchiveDtoBuilder.CreateMediaAudioItemRequest();
        var archiveResultsAudioSent = await client.PostAsJsonAsync<MediaAudioItemDto>($"/api/Archive/{result?.Id}/AudioItems", audioItem);
        
        var archiveWithAudios = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
        var updatedArchiveResult = archiveWithAudios?.MediaAudioItems?[0];
        
        Assert.AreEqual(HttpStatusCode.Created, archiveResult.StatusCode);
        Assert.AreEqual(HttpStatusCode.OK, archiveResultsAudioSent.StatusCode);
        Assert.IsNotNull(updatedArchiveResult);
        CollectionAssert.AllItemsAreNotNull(archiveWithAudios?.MediaAudioItems);

        var deleteAudio = await client.DeleteAsync($"/api/Archive/{result?.Id}/AudioItems/{updatedArchiveResult?.Id}");
        var updatedArchiveResultAfterDeleting = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
        Assert.AreEqual(HttpStatusCode.OK, deleteAudio.StatusCode);
        Assert.AreEqual(1, updatedArchiveResultAfterDeleting?.MediaAudioItems?.Count);
    }

    [TestMethod]
    public async Task CanDeletePhotoItemFromArchive()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addArchiveRequest = ArchiveDtoBuilder.BuildAddArchiveRequest();
        var archiveResult = await client.PostAsJsonAsync("/api/Archive", addArchiveRequest);
        var resultString = await archiveResult.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ArchiveDto>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        
        var photoItem = ArchiveDtoBuilder.CreateMediaPhotoItemRequest();
        var archiveResultsPhotoSent = await client.PostAsJsonAsync<MediaPhotoItemDto>($"/api/Archive/{result?.Id}/PhotoItems", photoItem);
        
        var archiveWithPhotos = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
        var updatedArchiveResult = archiveWithPhotos?.MediaPhotoItems?[0];
        
        Assert.AreEqual(HttpStatusCode.Created, archiveResult.StatusCode);
        Assert.AreEqual(HttpStatusCode.OK, archiveResultsPhotoSent.StatusCode);
        Assert.IsNotNull(updatedArchiveResult);
        CollectionAssert.AllItemsAreNotNull(archiveWithPhotos?.MediaPhotoItems);
        
        var deletePhoto = await client.DeleteAsync($"/api/Archive/{result?.Id}/PhotoItems/{updatedArchiveResult?.Id}");
        var updatedArchiveResultAfterDeleting = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
        Assert.AreEqual(HttpStatusCode.OK, deletePhoto.StatusCode);
        Assert.AreEqual(1, updatedArchiveResultAfterDeleting?.MediaPhotoItems?.Count);
    }

    [TestMethod]
    public async Task CanDeleteNewsItemFromArchive()
    {

    }
}