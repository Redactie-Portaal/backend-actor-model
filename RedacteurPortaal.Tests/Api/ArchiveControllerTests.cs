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
        for (int i = 0; i < patchArchiveRequest.MediaPhotoItems?.Count; i++)
        {
            Assert.AreEqual(patchArchiveRequest.MediaPhotoItems[i], patchResult?.MediaPhotoItems?[i]);
        }
        for (int i = 0; i < patchArchiveRequest.MediaVideoItems?.Count; i++)
        {
            Assert.AreEqual(patchArchiveRequest.MediaVideoItems[i], patchResult?.MediaVideoItems?[i]);
        }
        for (int i = 0; i < patchArchiveRequest.MediaAudioItems?.Count; i++)
        {
            Assert.AreEqual(patchArchiveRequest.MediaAudioItems[i], patchResult?.MediaAudioItems?[i]);
        }
        for (int i = 0; i < patchArchiveRequest.NewsItems?.Count; i++)
        {
            Assert.AreEqual(patchArchiveRequest.NewsItems[i], patchResult?.NewsItems?[i]);
        }
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
        var videoItemId = Guid.NewGuid();
        var updatedArchiveResult = await client.PostAsJsonAsync<Guid>($"/api/Archive/{result?.Id}/VideoItems", videoItemId);

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
        var audioItemId = Guid.NewGuid();
        var updatedArchiveResult = await client.PostAsJsonAsync<Guid>($"/api/Archive/{result?.Id}/AudioItems", audioItemId);

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
        var photoItemId = Guid.NewGuid();
        var updatedArchiveResult = await client.PostAsJsonAsync<Guid>($"/api/Archive/{result?.Id}/PhotoItems", photoItemId);

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
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addArchiveRequest = ArchiveDtoBuilder.BuildAddArchiveRequest();
        var archiveResult = await client.PostAsJsonAsync("/api/Archive", addArchiveRequest);
        var resultString = await archiveResult.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ArchiveDto>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        var newsItemId = Guid.NewGuid();
        var addNewsitem = await client.PostAsJsonAsync<Guid>($"/api/Archive/{result?.Id}/NewsItems", newsItemId);
        var archiveWithNewsitem = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
        Assert.AreEqual(HttpStatusCode.Created, archiveResult.StatusCode);
        Assert.AreEqual(HttpStatusCode.OK, addNewsitem.StatusCode);
        Assert.IsNotNull(result);
        Assert.IsNotNull(archiveWithNewsitem);
        Assert.IsNotNull(archiveWithNewsitem);
        Assert.IsNotNull(archiveWithNewsitem?.NewsItems);
        Assert.AreEqual(1, archiveWithNewsitem?.NewsItems?.Count);
        Assert.IsNotNull(archiveWithNewsitem?.NewsItems?[0]);
    }

    //[TestMethod]
    //public async Task CanGetVideoItemById()
    //{
    //    var application = new RedacteurPortaalApplication();
    //    var client = application.CreateClient();

    //    var addArchiveRequest = ArchiveDtoBuilder.BuildSmallestArchive();
    //    var archiveResult = await client.PostAsJsonAsync("/api/Archive", addArchiveRequest);
    //    var resultString = await archiveResult.Content.ReadAsStringAsync();

    //    var result = JsonSerializer.Deserialize<ArchiveDto>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
    //    var videoItemId = Guid.NewGuid();
    //    var archiveResultVideoSent = await client.PostAsJsonAsync<Guid>($"/api/Archive/{result?.Id}/VideoItems", videoItemId);
    //    var archiveWithVideos = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
    //    var updatedArchiveResult = await client.GetFromJsonAsync<MediaVideoItemDto>($"/api/Archive/{result?.Id}/VideoItems/{archiveWithVideos?.MediaVideoItems?[0]}");

    //    Assert.AreEqual(HttpStatusCode.Created, archiveResult.StatusCode);
    //    Assert.AreEqual(HttpStatusCode.OK, archiveResultVideoSent.StatusCode);
    //    Assert.IsNotNull(updatedArchiveResult);
    //    Assert.IsNotNull(updatedArchiveResult?.Camera);
    //    Assert.IsNotNull(updatedArchiveResult?.Description);
    //    Assert.IsNotNull(updatedArchiveResult?.EPG);
    //    Assert.IsNotNull(updatedArchiveResult?.ItemName);
    //    Assert.IsNotNull(updatedArchiveResult?.Editor);
    //    Assert.IsNotNull(updatedArchiveResult?.Director);
    //    Assert.IsNotNull(updatedArchiveResult?.DurationSeconds);
    //    CollectionAssert.AllItemsAreNotNull(updatedArchiveResult?.Guests);
    //    CollectionAssert.AllItemsAreNotNull(updatedArchiveResult?.Keywords);
    //    Assert.IsNotNull(updatedArchiveResult?.FirstPicture);
    //    Assert.IsNotNull(updatedArchiveResult?.FirstWords);
    //    Assert.IsNotNull(updatedArchiveResult?.Format);
    //    Assert.IsNotNull(updatedArchiveResult?.Reporter);
    //}

    //[TestMethod]
    //public async Task CanGetAudioItemById()
    //{
    //    var application = new RedacteurPortaalApplication();
    //    var client = application.CreateClient();

    //    var addArchiveRequest = ArchiveDtoBuilder.BuildAddArchiveRequest();
    //    var archiveResult = await client.PostAsJsonAsync("/api/Archive", addArchiveRequest);
    //    var resultString = await archiveResult.Content.ReadAsStringAsync();

    //    var result = JsonSerializer.Deserialize<ArchiveDto>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
    //    var audioItemId = Guid.NewGuid();
    //    var archiveResultAudioSent = await client.PostAsJsonAsync<Guid>($"/api/Archive/{result?.Id}/AudioItems", audioItemId);
    //    var archiveWithAudios = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
    //    var updatedArchiveResult = await client.GetFromJsonAsync<MediaAudioItemDto>($"/api/Archive/{result?.Id}/AudioItems/{archiveWithAudios?.MediaAudioItems?[0]}");

    //    Assert.AreEqual(HttpStatusCode.Created, archiveResult.StatusCode);
    //    Assert.AreEqual(HttpStatusCode.OK, archiveResultAudioSent.StatusCode);
    //    Assert.IsNotNull(updatedArchiveResult);
    //    Assert.IsNotNull(updatedArchiveResult?.DurationSeconds);
    //    Assert.IsNotNull(updatedArchiveResult?.FirstWords);
    //    Assert.IsNotNull(updatedArchiveResult?.ProgramName);
    //    Assert.IsNotNull(updatedArchiveResult?.Weather);
    //}

    //[TestMethod]
    //public async Task CanGetPhotoItemById()
    //{
    //    var application = new RedacteurPortaalApplication();
    //    var client = application.CreateClient();

    //    var addArchiveRequest = ArchiveDtoBuilder.BuildSmallestArchive();
    //    var archiveResult = await client.PostAsJsonAsync("/api/Archive", addArchiveRequest);
    //    var resultString = await archiveResult.Content.ReadAsStringAsync();

    //    var result = JsonSerializer.Deserialize<ArchiveDto>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
    //    var photoItemId = Guid.NewGuid();
    //    var archiveResultPhotoSent = await client.PostAsJsonAsync<Guid>($"/api/Archive/{result?.Id}/PhotoItems", photoItemId);
    //    var archiveWithPhotos = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
    //    var updatedArchiveResult = await client.GetFromJsonAsync<MediaPhotoItemDto>($"/api/Archive/{result?.Id}/PhotoItems/{archiveWithPhotos?.MediaPhotoItems?[0]}");

    //    Assert.AreEqual(HttpStatusCode.Created, archiveResult.StatusCode);
    //    Assert.AreEqual(HttpStatusCode.OK, archiveResultPhotoSent.StatusCode);
    //    Assert.IsNotNull(updatedArchiveResult);
    //    Assert.IsNotNull(updatedArchiveResult?.Presentation);
    //    Assert.IsNotNull(updatedArchiveResult?.Camera);
    //    Assert.IsNotNull(updatedArchiveResult?.RepublishDate);
    //    Assert.IsNotNull(updatedArchiveResult?.Format);
    //    Assert.IsNotNull(updatedArchiveResult?.Location);
    //    Assert.IsNotNull(updatedArchiveResult?.MediaLocation);
    //    Assert.IsNotNull(updatedArchiveResult?.Folder);
    //    Assert.IsNotNull(updatedArchiveResult?.Image);
    //    Assert.IsNotNull(updatedArchiveResult?.ProxyFile);
    //    Assert.IsNotNull(updatedArchiveResult?.Title);
    //    Assert.IsNotNull(updatedArchiveResult?.Rights);
    //    Assert.IsNotNull(updatedArchiveResult?.LastWords);
    //}

    //[TestMethod]
    //public async Task CanGetNewsItemById()
    //{

    //    var application = new RedacteurPortaalApplication();
    //    var client = application.CreateClient();

    //    var addArchiveRequest = ArchiveDtoBuilder.BuildAddArchiveRequest();
    //    var archiveResult = await client.PostAsJsonAsync("/api/Archive", addArchiveRequest);
    //    var resultString = await archiveResult.Content.ReadAsStringAsync();

    //    var result = JsonSerializer.Deserialize<ArchiveDto>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });


    //    var newsItemId = Guid.NewGuid();
    //    var archiveResultPhotoSent = await client.PostAsJsonAsync<Guid>($"/api/Archive/{result?.Id}/NewsItems", newsItemId);
    //    var archiveWithNewsitem = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
    //    var updatedArchiveResult = await client.GetFromJsonAsync<NewsItemDto>($"/api/Archive/{result?.Id}/NewsItems/{archiveWithNewsitem?.NewsItems?[0]}");
    //    Assert.AreEqual(HttpStatusCode.Created, archiveResult.StatusCode);
    //    Assert.AreEqual(HttpStatusCode.OK, archiveResultPhotoSent.StatusCode);
    //    Assert.IsNotNull(result);
    //    Assert.IsNotNull(archiveWithNewsitem);
    //    Assert.IsNotNull(updatedArchiveResult);
    //    Assert.IsNotNull(updatedArchiveResult?.Author);
    //    Assert.IsNotNull(updatedArchiveResult?.Title);
    //    Assert.IsNotNull(updatedArchiveResult?.Body);
    //    Assert.IsNotNull(updatedArchiveResult?.Category);
    //    Assert.IsNotNull(updatedArchiveResult?.Region);
    //    Assert.IsNotNull(updatedArchiveResult?.EndDate);
    //    for (var i = 0; i < updatedArchiveResult?.ContactDetails.Count; i++)
    //    {
    //        Assert.IsNotNull(updatedArchiveResult.ContactDetails[i].Email);
    //        Assert.IsNotNull(updatedArchiveResult.ContactDetails[i].TelephoneNumber);
    //        Assert.IsNotNull(updatedArchiveResult.ContactDetails[i].Name);
    //    }
    //}

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
        for (int i = 0; i < addArchiveRequest.MediaPhotoItems.Count; i++)
        {
            Assert.AreEqual(addArchiveRequest.MediaPhotoItems[i], result?.MediaPhotoItems?[i]);
        }
        for (int i = 0; i < addArchiveRequest.MediaVideoItems.Count; i++)
        {
            Assert.AreEqual(addArchiveRequest.MediaVideoItems[i], result?.MediaVideoItems?[i]);
        }
        for (int i = 0; i < addArchiveRequest.MediaAudioItems.Count; i++)
        {
            Assert.AreEqual(addArchiveRequest.MediaAudioItems[i], result?.MediaAudioItems?[i]);
        }
        for (int i = 0; i < addArchiveRequest.NewsItems.Count; i++)
        {
            Assert.AreEqual(addArchiveRequest.NewsItems[i], result?.NewsItems?[i]);
        }
        //Assert.AreEqual(addArchiveRequest.ArchivedDate, result?.ArchivedDate);
        CollectionAssert.AreEqual(addArchiveRequest.Scripts, result?.Scripts);

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
        var videoItemId = Guid.NewGuid();
        var updatedArchiveResult = await client.PostAsJsonAsync<Guid>($"/api/Archive/{result?.Id}/VideoItems", videoItemId);
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
        var audioItemId = Guid.NewGuid();
        var updatedArchiveResult = await client.PostAsJsonAsync<Guid>($"/api/Archive/{result?.Id}/AudioItems", audioItemId);
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
        var photoItemId = Guid.NewGuid();
        var updatedArchiveResult = await client.PostAsJsonAsync<Guid>($"/api/Archive/{result?.Id}/PhotoItems", photoItemId);
        var archiveWithPhoto = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
        
        Assert.AreEqual(HttpStatusCode.Created, archiveResult.StatusCode);
        Assert.AreEqual(HttpStatusCode.OK, updatedArchiveResult.StatusCode);
        Assert.IsNotNull(archiveWithPhoto);
        Assert.AreNotEqual(addArchiveRequest?.MediaPhotoItems, archiveWithPhoto?.MediaPhotoItems);
    }

    [TestMethod]
    public async Task CanAddNewsItemById()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addArchiveRequest = ArchiveDtoBuilder.BuildSmallestArchive();
        var archiveResult = await client.PostAsJsonAsync("/api/Archive", addArchiveRequest);
        var resultString = await archiveResult.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ArchiveDto>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        var newsItemId = Guid.NewGuid();
        var updatedArchiveResult = await client.PostAsJsonAsync<Guid>($"/api/Archive/{result?.Id}/NewsItems", newsItemId);
        var archiveWithNewsitem = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
        Assert.AreEqual(HttpStatusCode.Created, archiveResult.StatusCode);
        Assert.AreEqual(HttpStatusCode.OK, updatedArchiveResult.StatusCode);
        Assert.IsNotNull(archiveWithNewsitem);
        //Assert.AreNotEqual(newsItemRequest, archiveWithNewsitem?.NewsItems?[0]);
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
        Assert.AreEqual(HttpStatusCode.NoContent, delete.StatusCode);

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

        
        var videoItemId = Guid.NewGuid();
        var archiveResultsVideoSent = await client.PostAsJsonAsync<Guid>($"/api/Archive/{result?.Id}/VideoItems", videoItemId);

        var archiveWithVideos = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
        var updatedArchiveResult = archiveWithVideos?.MediaVideoItems?[0];
        
        Assert.AreEqual(HttpStatusCode.Created, archiveResult.StatusCode);
        Assert.AreEqual(HttpStatusCode.OK, archiveResultsVideoSent.StatusCode);
        Assert.IsNotNull(updatedArchiveResult);
        CollectionAssert.AllItemsAreNotNull(archiveWithVideos?.MediaVideoItems);
        
        var deleteVideo = await client.DeleteAsync($"/api/Archive/{result?.Id}/VideoItems/{updatedArchiveResult}");
        var updatedArchiveResultAfterDeleting = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
        Assert.AreEqual(HttpStatusCode.NoContent, deleteVideo.StatusCode);
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

        var audioItemId = Guid.NewGuid();
        var archiveResultsAudioSent = await client.PostAsJsonAsync<Guid>($"/api/Archive/{result?.Id}/AudioItems", audioItemId);

        var archiveWithAudios = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
        var updatedArchiveResult = archiveWithAudios?.MediaAudioItems?[0];
        
        Assert.AreEqual(HttpStatusCode.Created, archiveResult.StatusCode);
        Assert.AreEqual(HttpStatusCode.OK, archiveResultsAudioSent.StatusCode);
        Assert.IsNotNull(updatedArchiveResult);
        CollectionAssert.AllItemsAreNotNull(archiveWithAudios?.MediaAudioItems);

        var deleteAudio = await client.DeleteAsync($"/api/Archive/{result?.Id}/AudioItems/{updatedArchiveResult}");
        var updatedArchiveResultAfterDeleting = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
        Assert.AreEqual(HttpStatusCode.NoContent, deleteAudio.StatusCode);
        Assert.AreEqual(0, updatedArchiveResultAfterDeleting?.MediaAudioItems?.Count);
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

        var photoItemId = Guid.NewGuid();
        var archiveResultsPhotoSent = await client.PostAsJsonAsync<Guid>($"/api/Archive/{result?.Id}/PhotoItems", photoItemId);

        var archiveWithPhotos = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
        var updatedArchiveResult = archiveWithPhotos?.MediaPhotoItems?[0];
        
        Assert.AreEqual(HttpStatusCode.Created, archiveResult.StatusCode);
        Assert.AreEqual(HttpStatusCode.OK, archiveResultsPhotoSent.StatusCode);
        Assert.IsNotNull(updatedArchiveResult);
        CollectionAssert.AllItemsAreNotNull(archiveWithPhotos?.MediaPhotoItems);
        
        var deletePhoto = await client.DeleteAsync($"/api/Archive/{result?.Id}/PhotoItems/{updatedArchiveResult}");
        var updatedArchiveResultAfterDeleting = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
        Assert.AreEqual(HttpStatusCode.NoContent, deletePhoto.StatusCode);
        Assert.AreEqual(0, updatedArchiveResultAfterDeleting?.MediaPhotoItems?.Count);
    }

    [TestMethod]
    public async Task CanDeleteNewsItemFromArchive()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addArchiveRequest = ArchiveDtoBuilder.BuildAddArchiveRequest();
        var archiveResult = await client.PostAsJsonAsync("/api/Archive", addArchiveRequest);
        var resultString = await archiveResult.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ArchiveDto>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        var newsItemId = Guid.NewGuid();
        var archiveResultsNewsItemSent = await client.PostAsJsonAsync<Guid>($"/api/Archive/{result?.Id}/NewsItems", newsItemId);

        var archiveWithNewsItems = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
        var updatedArchiveResult = archiveWithNewsItems?.NewsItems?[0];

        Assert.AreEqual(HttpStatusCode.Created, archiveResult.StatusCode);
        Assert.AreEqual(HttpStatusCode.OK, archiveResultsNewsItemSent.StatusCode);
        Assert.IsNotNull(updatedArchiveResult);
        CollectionAssert.AllItemsAreNotNull(archiveWithNewsItems?.NewsItems);

        var deleteNewsItem = await client.DeleteAsync($"/api/Archive/{result?.Id}/NewsItems/{updatedArchiveResult}");
        var updatedArchiveResultAfterDeleting = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
        Assert.AreEqual(HttpStatusCode.NoContent, deleteNewsItem.StatusCode);
        Assert.AreEqual(0, updatedArchiveResultAfterDeleting?.NewsItems?.Count);
    }
}