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
        for (int i = 0; i < patchArchiveRequest.MediaPhotoItems.Count; i++)
        {
            Assert.AreEqual(patchArchiveRequest.MediaPhotoItems[i].Id, patchResult?.MediaPhotoItems?[i].Id);
            Assert.AreEqual(patchArchiveRequest.MediaPhotoItems[i].Title, patchResult?.MediaPhotoItems?[i].Title);
            Assert.AreEqual(patchArchiveRequest.MediaPhotoItems[i].Format, patchResult?.MediaPhotoItems?[i].Format);
            Assert.AreEqual(patchArchiveRequest.MediaPhotoItems[i].Image, patchResult?.MediaPhotoItems?[i].Image);
            Assert.AreEqual(patchArchiveRequest.MediaPhotoItems[i].Rights, patchResult?.MediaPhotoItems?[i].Rights);
            Assert.AreEqual(patchArchiveRequest.MediaPhotoItems[i].Folder, patchResult?.MediaPhotoItems?[i].Folder);
            Assert.AreEqual(patchArchiveRequest.MediaPhotoItems[i].Presentation, patchResult?.MediaPhotoItems?[i].Presentation);
            Assert.AreEqual(patchArchiveRequest.MediaPhotoItems[i].Camera, patchResult?.MediaPhotoItems?[i].Camera);
            Assert.AreEqual(patchArchiveRequest.MediaPhotoItems[i].LastWords, patchResult?.MediaPhotoItems?[i].LastWords);
            Assert.AreEqual(patchArchiveRequest.MediaPhotoItems[i].RepublishDate, patchResult?.MediaPhotoItems?[i].RepublishDate);
            Assert.AreEqual(patchArchiveRequest.MediaPhotoItems[i].MediaLocation, patchResult?.MediaPhotoItems?[i].MediaLocation);
            Assert.AreEqual(patchArchiveRequest.MediaPhotoItems[i].ProxyFile, patchResult?.MediaPhotoItems?[i].ProxyFile);
            Assert.AreEqual(patchArchiveRequest.MediaPhotoItems[i].Location.Id, patchResult?.MediaPhotoItems?[i].Location.Id);
            Assert.AreEqual(patchArchiveRequest.MediaPhotoItems[i].Location.Longitude, patchResult?.MediaPhotoItems?[i].Location.Longitude);
            Assert.AreEqual(patchArchiveRequest.MediaPhotoItems[i].Location.Latitude, patchResult?.MediaPhotoItems?[i].Location.Latitude);
            Assert.AreEqual(patchArchiveRequest.MediaPhotoItems[i].Location.Province, patchResult?.MediaPhotoItems?[i].Location.Province);
            Assert.AreEqual(patchArchiveRequest.MediaPhotoItems[i].Location.City, patchResult?.MediaPhotoItems?[i].Location.City);
            Assert.AreEqual(patchArchiveRequest.MediaPhotoItems[i].Location.Street, patchResult?.MediaPhotoItems?[i].Location.Street);
            Assert.AreEqual(patchArchiveRequest.MediaPhotoItems[i].Location.Zip, patchResult?.MediaPhotoItems?[i].Location.Zip);
        }
        for (int i = 0; i < patchArchiveRequest.MediaVideoItems.Count; i++)
        {
            Assert.AreEqual(patchArchiveRequest.MediaVideoItems[i].Id, patchResult?.MediaVideoItems?[i].Id);
            Assert.AreEqual(patchArchiveRequest.MediaVideoItems[i].Title, patchResult?.MediaVideoItems?[i].Title);
            Assert.AreEqual(patchArchiveRequest.MediaVideoItems[i].Format, patchResult?.MediaVideoItems?[i].Format);
            Assert.AreEqual(patchArchiveRequest.MediaVideoItems[i].Rights, patchResult?.MediaVideoItems?[i].Rights);
            Assert.AreEqual(patchArchiveRequest.MediaVideoItems[i].Folder, patchResult?.MediaVideoItems?[i].Folder);
            Assert.AreEqual(patchArchiveRequest.MediaVideoItems[i].Presentation, patchResult?.MediaVideoItems?[i].Presentation);
            Assert.AreEqual(patchArchiveRequest.MediaVideoItems[i].Camera, patchResult?.MediaVideoItems?[i].Camera);
            Assert.AreEqual(patchArchiveRequest.MediaVideoItems[i].LastWords, patchResult?.MediaVideoItems?[i].LastWords);
            Assert.AreEqual(patchArchiveRequest.MediaVideoItems[i].RepublishDate, patchResult?.MediaVideoItems?[i].RepublishDate);
            Assert.AreEqual(patchArchiveRequest.MediaVideoItems[i].MediaLocation, patchResult?.MediaVideoItems?[i].MediaLocation);
            Assert.AreEqual(patchArchiveRequest.MediaVideoItems[i].ProxyFile, patchResult?.MediaVideoItems?[i].ProxyFile);
            Assert.AreEqual(patchArchiveRequest.MediaVideoItems[i].Editor, patchResult?.MediaVideoItems?[i].Editor);
            Assert.AreEqual(patchArchiveRequest.MediaVideoItems[i].Sound, patchResult?.MediaVideoItems?[i].Sound);
            Assert.AreEqual(patchArchiveRequest.MediaVideoItems[i].VoiceOver, patchResult?.MediaVideoItems?[i].VoiceOver);
            Assert.AreEqual(patchArchiveRequest.MediaVideoItems[i].Description, patchResult?.MediaVideoItems?[i].Description);
            Assert.AreEqual(patchArchiveRequest.MediaVideoItems[i].EPG, patchResult?.MediaVideoItems?[i].EPG);
            Assert.AreEqual(patchArchiveRequest.MediaVideoItems[i].ArchiveMaterial, patchResult?.MediaVideoItems?[i].ArchiveMaterial);
            //Assert.AreEqual(patchArchiveRequest.MediaVideoItems[i].DurationSeconds, result?.MediaVideoItems?[i].DurationSeconds);
            Assert.AreEqual(patchArchiveRequest.MediaVideoItems[i].FirstPicture, patchResult?.MediaVideoItems?[i].FirstPicture);
            Assert.AreEqual(patchArchiveRequest.MediaVideoItems[i].Director, patchResult?.MediaVideoItems?[i].Director);
            Assert.AreEqual(patchArchiveRequest.MediaVideoItems[i].ProgramDate, patchResult?.MediaVideoItems?[i].ProgramDate);
            Assert.AreEqual(patchArchiveRequest.MediaVideoItems[i].ProgramName, patchResult?.MediaVideoItems?[i].ProgramName);
            Assert.AreEqual(patchArchiveRequest.MediaVideoItems[i].Weather, patchResult?.MediaVideoItems?[i].Weather);
            Assert.AreEqual(patchArchiveRequest.MediaVideoItems[i].Location.Id, patchResult?.MediaVideoItems?[i].Location.Id);
            Assert.AreEqual(patchArchiveRequest.MediaVideoItems[i].Location.Longitude, patchResult?.MediaVideoItems?[i].Location.Longitude);
            Assert.AreEqual(patchArchiveRequest.MediaVideoItems[i].Location.Latitude, patchResult?.MediaVideoItems?[i].Location.Latitude);
            Assert.AreEqual(patchArchiveRequest.MediaVideoItems[i].Location.Province, patchResult?.MediaVideoItems?[i].Location.Province);
            Assert.AreEqual(patchArchiveRequest.MediaVideoItems[i].Location.City, patchResult?.MediaVideoItems?[i].Location.City);
            Assert.AreEqual(patchArchiveRequest.MediaVideoItems[i].Location.Street, patchResult?.MediaVideoItems?[i].Location.Street);
            Assert.AreEqual(patchArchiveRequest.MediaVideoItems[i].Location.Zip, patchResult?.MediaVideoItems?[i].Location.Zip);
        }
        for (int i = 0; i < patchArchiveRequest.MediaAudioItems.Count; i++)
        {
            Assert.AreEqual(patchArchiveRequest.MediaAudioItems[i].Id, patchResult?.MediaAudioItems?[i].Id);
            Assert.AreEqual(patchArchiveRequest.MediaAudioItems[i].Title, patchResult?.MediaAudioItems?[i].Title);
            Assert.AreEqual(patchArchiveRequest.MediaAudioItems[i].Folder, patchResult?.MediaAudioItems?[i].Folder);
            Assert.AreEqual(patchArchiveRequest.MediaAudioItems[i].RepublishDate, patchResult?.MediaAudioItems?[i].RepublishDate);
            Assert.AreEqual(patchArchiveRequest.MediaAudioItems[i].Rights, patchResult?.MediaAudioItems?[i].Rights);
            Assert.AreEqual(patchArchiveRequest.MediaAudioItems[i].Camera, patchResult?.MediaAudioItems?[i].Camera);
            Assert.AreEqual(patchArchiveRequest.MediaAudioItems[i].LastWords, patchResult?.MediaAudioItems?[i].LastWords);
            Assert.AreEqual(patchArchiveRequest.MediaAudioItems[i].ProxyFile, patchResult?.MediaAudioItems?[i].ProxyFile);
            Assert.AreEqual(patchArchiveRequest.MediaAudioItems[i].Presentation, patchResult?.MediaAudioItems?[i].Presentation);
            Assert.AreEqual(patchArchiveRequest.MediaAudioItems[i].Location.Id, patchResult?.MediaAudioItems?[i].Location.Id);
            Assert.AreEqual(patchArchiveRequest.MediaAudioItems[i].Location.Longitude, patchResult?.MediaAudioItems?[i].Location.Longitude);
            Assert.AreEqual(patchArchiveRequest.MediaAudioItems[i].Location.Latitude, patchResult?.MediaAudioItems?[i].Location.Latitude);
            Assert.AreEqual(patchArchiveRequest.MediaAudioItems[i].Location.Province, patchResult?.MediaAudioItems?[i].Location.Province);
            Assert.AreEqual(patchArchiveRequest.MediaAudioItems[i].Location.City, patchResult?.MediaAudioItems?[i].Location.City);
            Assert.AreEqual(patchArchiveRequest.MediaAudioItems[i].Location.Street, patchResult?.MediaAudioItems?[i].Location.Street);
            Assert.AreEqual(patchArchiveRequest.MediaAudioItems[i].Location.Zip, patchResult?.MediaAudioItems?[i].Location.Zip);
            Assert.AreEqual(patchArchiveRequest.MediaAudioItems[i].Format, patchResult?.MediaAudioItems?[i].Format);
            Assert.AreEqual(patchArchiveRequest.MediaAudioItems[i].MediaLocation, patchResult?.MediaAudioItems?[i].MediaLocation);
            //Assert.AreEqual(patchArchiveRequest.MediaAudioItems[i].DurationSeconds, result?.MediaAudioItems?[i].DurationSeconds);
            Assert.AreEqual(patchArchiveRequest.MediaAudioItems[i].Weather, patchResult?.MediaAudioItems?[i].Weather);
            Assert.AreEqual(patchArchiveRequest.MediaAudioItems[i].FirstWords, patchResult?.MediaAudioItems?[i].FirstWords);
            Assert.AreEqual(patchArchiveRequest.MediaAudioItems[i].ProgramName, patchResult?.MediaAudioItems?[i].ProgramName);
        }
        //Assert.AreEqual(patchArchiveRequest.NewsItems, patchResult?.NewsItems);
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
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addArchiveRequest = ArchiveDtoBuilder.BuildAddArchiveRequest();
        var archiveResult = await client.PostAsJsonAsync("/api/Archive", addArchiveRequest);
        var resultString = await archiveResult.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ArchiveDto>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        var newsItemRequest = DtoBuilder.BuildAddNewsItemRequest();
        var addNewsitem = await client.PostAsJsonAsync<NewsItemDto>($"/api/Archive/{result?.Id}/NewsItems", newsItemRequest);
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

        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addArchiveRequest = ArchiveDtoBuilder.BuildAddArchiveRequest();
        var archiveResult = await client.PostAsJsonAsync("/api/Archive", addArchiveRequest);
        var resultString = await archiveResult.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ArchiveDto>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        
        var newsItemRequest = DtoBuilder.BuildAddNewsItemRequest();
        var addNewsitem = await client.PostAsJsonAsync<NewsItemDto>($"/api/Archive/{result?.Id}/NewsItems", newsItemRequest);
        var archiveWithNewsitem = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
        var updatedArchiveResult = await client.GetFromJsonAsync<NewsItemDto>($"/api/Archive/{result?.Id}/NewsItems/{archiveWithNewsitem?.NewsItems?[0].Id}");
        Assert.AreEqual(HttpStatusCode.Created, archiveResult.StatusCode);
        Assert.AreEqual(HttpStatusCode.OK, addNewsitem.StatusCode);
        Assert.IsNotNull(result);
        Assert.IsNotNull(archiveWithNewsitem);
        Assert.IsNotNull(updatedArchiveResult);
        Assert.AreEqual(newsItemRequest.Author, updatedArchiveResult?.Author);
        Assert.AreEqual(newsItemRequest.Title, updatedArchiveResult?.Title);
        Assert.AreEqual(newsItemRequest.Body, updatedArchiveResult?.Body);
        Assert.AreEqual(newsItemRequest.Category, updatedArchiveResult?.Category);
        Assert.AreEqual(newsItemRequest.Region, updatedArchiveResult?.Region);
        Assert.AreEqual(newsItemRequest.EndDate, updatedArchiveResult?.EndDate);
        for (var i = 0; i < updatedArchiveResult?.ContactDetails.Count; i++)
        {
            Assert.AreEqual(newsItemRequest.ContactDetails[i].Email, updatedArchiveResult.ContactDetails[i].Email);
            Assert.AreEqual(newsItemRequest.ContactDetails[i].TelephoneNumber, updatedArchiveResult.ContactDetails[i].TelephoneNumber);
            Assert.AreEqual(newsItemRequest.ContactDetails[i].Name, updatedArchiveResult.ContactDetails[i].Name);
        }
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
        for (int i = 0; i < addArchiveRequest.MediaPhotoItems.Count; i++)
        {
            Assert.AreEqual(addArchiveRequest.MediaPhotoItems[i].Id, result?.MediaPhotoItems?[i].Id);
            Assert.AreEqual(addArchiveRequest.MediaPhotoItems[i].Title, result?.MediaPhotoItems?[i].Title);
            Assert.AreEqual(addArchiveRequest.MediaPhotoItems[i].Format, result?.MediaPhotoItems?[i].Format);
            Assert.AreEqual(addArchiveRequest.MediaPhotoItems[i].Image,result?.MediaPhotoItems?[i].Image);
            Assert.AreEqual(addArchiveRequest.MediaPhotoItems[i].Rights, result?.MediaPhotoItems?[i].Rights);
            Assert.AreEqual(addArchiveRequest.MediaPhotoItems[i].Folder, result?.MediaPhotoItems?[i].Folder);
            Assert.AreEqual(addArchiveRequest.MediaPhotoItems[i].Presentation, result?.MediaPhotoItems?[i].Presentation);
            Assert.AreEqual(addArchiveRequest.MediaPhotoItems[i].Camera, result?.MediaPhotoItems?[i].Camera);
            Assert.AreEqual(addArchiveRequest.MediaPhotoItems[i].LastWords, result?.MediaPhotoItems?[i].LastWords);
            Assert.AreEqual(addArchiveRequest.MediaPhotoItems[i].RepublishDate, result?.MediaPhotoItems?[i].RepublishDate);
            Assert.AreEqual(addArchiveRequest.MediaPhotoItems[i].MediaLocation, result?.MediaPhotoItems?[i].MediaLocation);
            Assert.AreEqual(addArchiveRequest.MediaPhotoItems[i].ProxyFile, result?.MediaPhotoItems?[i].ProxyFile);
            Assert.AreEqual(addArchiveRequest.MediaPhotoItems[i].Location.Id, result?.MediaPhotoItems?[i].Location.Id);
            Assert.AreEqual(addArchiveRequest.MediaPhotoItems[i].Location.Longitude, result?.MediaPhotoItems?[i].Location.Longitude);
            Assert.AreEqual(addArchiveRequest.MediaPhotoItems[i].Location.Latitude, result?.MediaPhotoItems?[i].Location.Latitude);
            Assert.AreEqual(addArchiveRequest.MediaPhotoItems[i].Location.Province, result?.MediaPhotoItems?[i].Location.Province);
            Assert.AreEqual(addArchiveRequest.MediaPhotoItems[i].Location.City, result?.MediaPhotoItems?[i].Location.City);
            Assert.AreEqual(addArchiveRequest.MediaPhotoItems[i].Location.Street, result?.MediaPhotoItems?[i].Location.Street);
            Assert.AreEqual(addArchiveRequest.MediaPhotoItems[i].Location.Zip, result?.MediaPhotoItems?[i].Location.Zip);
        }
        for (int i = 0; i < addArchiveRequest.MediaVideoItems.Count; i++)
        {
            Assert.AreEqual(addArchiveRequest.MediaVideoItems[i].Id, result?.MediaVideoItems?[i].Id);
            Assert.AreEqual(addArchiveRequest.MediaVideoItems[i].Title, result?.MediaVideoItems?[i].Title);
            Assert.AreEqual(addArchiveRequest.MediaVideoItems[i].Format, result?.MediaVideoItems?[i].Format);
            Assert.AreEqual(addArchiveRequest.MediaVideoItems[i].Rights, result?.MediaVideoItems?[i].Rights);
            Assert.AreEqual(addArchiveRequest.MediaVideoItems[i].Folder, result?.MediaVideoItems?[i].Folder);
            Assert.AreEqual(addArchiveRequest.MediaVideoItems[i].Presentation, result?.MediaVideoItems?[i].Presentation);
            Assert.AreEqual(addArchiveRequest.MediaVideoItems[i].Camera, result?.MediaVideoItems?[i].Camera);
            Assert.AreEqual(addArchiveRequest.MediaVideoItems[i].LastWords, result?.MediaVideoItems?[i].LastWords);
            Assert.AreEqual(addArchiveRequest.MediaVideoItems[i].RepublishDate, result?.MediaVideoItems?[i].RepublishDate);
            Assert.AreEqual(addArchiveRequest.MediaVideoItems[i].MediaLocation, result?.MediaVideoItems?[i].MediaLocation);
            Assert.AreEqual(addArchiveRequest.MediaVideoItems[i].ProxyFile, result?.MediaVideoItems?[i].ProxyFile);
            Assert.AreEqual(addArchiveRequest.MediaVideoItems[i].Editor, result?.MediaVideoItems?[i].Editor);
            Assert.AreEqual(addArchiveRequest.MediaVideoItems[i].Sound, result?.MediaVideoItems?[i].Sound);
            Assert.AreEqual(addArchiveRequest.MediaVideoItems[i].VoiceOver, result?.MediaVideoItems?[i].VoiceOver);
            Assert.AreEqual(addArchiveRequest.MediaVideoItems[i].Description, result?.MediaVideoItems?[i].Description);
            Assert.AreEqual(addArchiveRequest.MediaVideoItems[i].EPG, result?.MediaVideoItems?[i].EPG);
            Assert.AreEqual(addArchiveRequest.MediaVideoItems[i].ArchiveMaterial, result?.MediaVideoItems?[i].ArchiveMaterial);
            //Assert.AreEqual(addArchiveRequest.MediaVideoItems[i].DurationSeconds, result?.MediaVideoItems?[i].DurationSeconds);
            Assert.AreEqual(addArchiveRequest.MediaVideoItems[i].FirstPicture, result?.MediaVideoItems?[i].FirstPicture);
            Assert.AreEqual(addArchiveRequest.MediaVideoItems[i].Director, result?.MediaVideoItems?[i].Director);
            Assert.AreEqual(addArchiveRequest.MediaVideoItems[i].ProgramDate, result?.MediaVideoItems?[i].ProgramDate);
            Assert.AreEqual(addArchiveRequest.MediaVideoItems[i].ProgramName, result?.MediaVideoItems?[i].ProgramName);
            Assert.AreEqual(addArchiveRequest.MediaVideoItems[i].Weather, result?.MediaVideoItems?[i].Weather);
            Assert.AreEqual(addArchiveRequest.MediaVideoItems[i].Location.Id, result?.MediaVideoItems?[i].Location.Id);
            Assert.AreEqual(addArchiveRequest.MediaVideoItems[i].Location.Longitude, result?.MediaVideoItems?[i].Location.Longitude);
            Assert.AreEqual(addArchiveRequest.MediaVideoItems[i].Location.Latitude, result?.MediaVideoItems?[i].Location.Latitude);
            Assert.AreEqual(addArchiveRequest.MediaVideoItems[i].Location.Province, result?.MediaVideoItems?[i].Location.Province);
            Assert.AreEqual(addArchiveRequest.MediaVideoItems[i].Location.City, result?.MediaVideoItems?[i].Location.City);
            Assert.AreEqual(addArchiveRequest.MediaVideoItems[i].Location.Street, result?.MediaVideoItems?[i].Location.Street);
            Assert.AreEqual(addArchiveRequest.MediaVideoItems[i].Location.Zip, result?.MediaVideoItems?[i].Location.Zip);
        }
        for (int i = 0; i < addArchiveRequest.MediaAudioItems.Count; i++)
        {
            Assert.AreEqual(addArchiveRequest.MediaAudioItems[i].Id, result?.MediaAudioItems?[i].Id);
            Assert.AreEqual(addArchiveRequest.MediaAudioItems[i].Title, result?.MediaAudioItems?[i].Title);
            Assert.AreEqual(addArchiveRequest.MediaAudioItems[i].Folder, result?.MediaAudioItems?[i].Folder);
            Assert.AreEqual(addArchiveRequest.MediaAudioItems[i].RepublishDate, result?.MediaAudioItems?[i].RepublishDate);
            Assert.AreEqual(addArchiveRequest.MediaAudioItems[i].Rights, result?.MediaAudioItems?[i].Rights);
            Assert.AreEqual(addArchiveRequest.MediaAudioItems[i].Camera, result?.MediaAudioItems?[i].Camera);
            Assert.AreEqual(addArchiveRequest.MediaAudioItems[i].LastWords, result?.MediaAudioItems?[i].LastWords);
            Assert.AreEqual(addArchiveRequest.MediaAudioItems[i].ProxyFile, result?.MediaAudioItems?[i].ProxyFile);
            Assert.AreEqual(addArchiveRequest.MediaAudioItems[i].Presentation, result?.MediaAudioItems?[i].Presentation);
            Assert.AreEqual(addArchiveRequest.MediaAudioItems[i].Location.Id, result?.MediaAudioItems?[i].Location.Id);
            Assert.AreEqual(addArchiveRequest.MediaAudioItems[i].Location.Longitude, result?.MediaAudioItems?[i].Location.Longitude);
            Assert.AreEqual(addArchiveRequest.MediaAudioItems[i].Location.Latitude, result?.MediaAudioItems?[i].Location.Latitude);
            Assert.AreEqual(addArchiveRequest.MediaAudioItems[i].Location.Province, result?.MediaAudioItems?[i].Location.Province);
            Assert.AreEqual(addArchiveRequest.MediaAudioItems[i].Location.City, result?.MediaAudioItems?[i].Location.City);
            Assert.AreEqual(addArchiveRequest.MediaAudioItems[i].Location.Street, result?.MediaAudioItems?[i].Location.Street);
            Assert.AreEqual(addArchiveRequest.MediaAudioItems[i].Location.Zip, result?.MediaAudioItems?[i].Location.Zip);
            Assert.AreEqual(addArchiveRequest.MediaAudioItems[i].Format, result?.MediaAudioItems?[i].Format);
            Assert.AreEqual(addArchiveRequest.MediaAudioItems[i].MediaLocation, result?.MediaAudioItems?[i].MediaLocation);
            //Assert.AreEqual(addArchiveRequest.MediaAudioItems[i].DurationSeconds, result?.MediaAudioItems?[i].DurationSeconds);
            Assert.AreEqual(addArchiveRequest.MediaAudioItems[i].Weather, result?.MediaAudioItems?[i].Weather);
            Assert.AreEqual(addArchiveRequest.MediaAudioItems[i].FirstWords, result?.MediaAudioItems?[i].FirstWords);
            Assert.AreEqual(addArchiveRequest.MediaAudioItems[i].ProgramName, result?.MediaAudioItems?[i].ProgramName);
        }
        CollectionAssert.AreEqual(addArchiveRequest.NewsItems, result?.NewsItems);
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
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addArchiveRequest = ArchiveDtoBuilder.BuildSmallestArchive();
        var archiveResult = await client.PostAsJsonAsync("/api/Archive", addArchiveRequest);
        var resultString = await archiveResult.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ArchiveDto>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        
        var newsItemRequest = DtoBuilder.BuildAddNewsItemRequest();
        var updatedArchiveResult = await client.PostAsJsonAsync<NewsItemDto>($"/api/Archive/{result?.Id}/NewsItems", newsItemRequest);
        var archiveWithNewsitem = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
        Assert.AreEqual(HttpStatusCode.Created, archiveResult.StatusCode);
        Assert.AreEqual(HttpStatusCode.OK, updatedArchiveResult.StatusCode);
        Assert.IsNotNull(archiveWithNewsitem);
        Assert.AreNotEqual(addArchiveRequest?.NewsItems?[0], archiveWithNewsitem?.NewsItems?[0]);
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
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addArchiveRequest = ArchiveDtoBuilder.BuildAddArchiveRequest();
        var archiveResult = await client.PostAsJsonAsync("/api/Archive", addArchiveRequest);
        var resultString = await archiveResult.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ArchiveDto>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });


        var newsItemRequest = DtoBuilder.BuildAddNewsItemRequest();
        var archiveResultsPhotoSent = await client.PostAsJsonAsync<NewsItemDto>($"/api/Archive/{result?.Id}/NewsItems", newsItemRequest);

        var archiveWithNewsItems = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
        var updatedArchiveResult = archiveWithNewsItems?.NewsItems?[0];

        Assert.AreEqual(HttpStatusCode.Created, archiveResult.StatusCode);
        Assert.AreEqual(HttpStatusCode.OK, archiveResultsPhotoSent.StatusCode);
        Assert.IsNotNull(updatedArchiveResult);
        CollectionAssert.AllItemsAreNotNull(archiveWithNewsItems?.NewsItems);

        var deleteNewsItem = await client.DeleteAsync($"/api/Archive/{result?.Id}/NewsItems/{updatedArchiveResult?.Id}");
        var updatedArchiveResultAfterDeleting = await client.GetFromJsonAsync<ArchiveDto>($"/api/Archive/{result?.Id}");
        Assert.AreEqual(HttpStatusCode.OK, deleteNewsItem.StatusCode);
        Assert.AreEqual(0, updatedArchiveResultAfterDeleting?.NewsItems?.Count);
    }
}