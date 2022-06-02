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
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.DomainModels.Profile;
using RedacteurPortaal.Helpers;
using JsonConverter = Newtonsoft.Json.JsonConverter;

namespace RedacteurPortaal.Tests.Api;

[TestClass]
public class NewsItemControllerTests
{

    [TestMethod]
    public async Task DefaultIsEmpty()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();
        var newsItems = await client.GetFromJsonAsync<List<NewsItemDto>>("/api/NewsItem");

        Assert.IsNotNull(newsItems);
        Assert.IsTrue(newsItems?.Count == 0);
    }

    [TestMethod]
    public async Task CanDelete()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addNewsItemRequest = DtoBuilder.BuildAddNewsItemRequest();
        var itemResult = await client.PostAsJsonAsync("/api/NewsItem", addNewsItemRequest);
        var result = JsonSerializer.Deserialize<NewsItemDto>(await itemResult.Content.ReadAsStringAsync(), new JsonSerializerOptions() {PropertyNameCaseInsensitive = true});

        var delete = await client.DeleteAsync($"/api/NewsItem/{result?.Id}");
        Assert.AreEqual(HttpStatusCode.NoContent, delete.StatusCode);

        var newAddress = await client.GetFromJsonAsync<List<NewsItemDto>>("/api/NewsItem");
        CollectionAssert.AllItemsAreNotNull(newAddress);
    }

    [TestMethod]
    public async Task CanAdd()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var newsItemRequest = DtoBuilder.BuildAddNewsItemRequest();
        var newsItem = await client.PostAsJsonAsync("/api/NewsItem", newsItemRequest);
        var resultString = await newsItem.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<NewsItemDto>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        Assert.IsNotNull(result);
        Assert.AreEqual(newsItemRequest.Author, result?.Author);
        Assert.AreEqual(newsItemRequest.Title, result?.Title);
        Assert.AreEqual(newsItemRequest.Body, result?.Body);
        Assert.AreEqual(newsItemRequest.Category, result?.Category);
        Assert.AreEqual(newsItemRequest.Region, result?.Region);
        Assert.AreEqual(newsItemRequest.EndDate, result?.EndDate);
        for (var i = 0; i < result?.ContactDetails.Count; i++)
        {
            Assert.AreEqual(newsItemRequest.ContactDetails[i].Email, result.ContactDetails[i].Email);
            Assert.AreEqual(newsItemRequest.ContactDetails[i].TelephoneNumber, result.ContactDetails[i].TelephoneNumber);
            Assert.AreEqual(newsItemRequest.ContactDetails[i].Name, result.ContactDetails[i].Name);
        }

        var newItems = await client.GetFromJsonAsync<List<NewsItemDto>>("/api/NewsItem");
        Assert.IsNotNull(newItems);
        Assert.IsTrue(newItems?.Count == 1);
    }

    [TestMethod]
    public async Task CanUpdate()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var newsItemRequest = DtoBuilder.BuildAddNewsItemRequest();
        var newsItem = await client.PostAsJsonAsync("/api/NewsItem", newsItemRequest);
        var resultString = await newsItem.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<NewsItemDto>(resultString, new JsonSerializerOptions() {PropertyNameCaseInsensitive = true});


        Assert.IsNotNull(result);
        Assert.AreEqual(newsItemRequest.Title, result?.Title);

        var patchItem = DtoBuilder.BuildUpdateNewsItemRequest();
        var patchContent = new StringContent(JsonSerializer.Serialize(patchItem), Encoding.UTF8, "application/json");
        var newItem = await client.PatchAsync($"/api/NewsItem/{result?.Id}",patchContent );

        var patchResult = JsonSerializer.Deserialize<NewsItemDto>(await newItem.Content.ReadAsStringAsync(), new JsonSerializerOptions() {PropertyNameCaseInsensitive = true});
        Assert.AreEqual(HttpStatusCode.OK, newItem.StatusCode);

        Assert.IsNotNull(patchResult);
        Assert.AreEqual(patchItem.Author, patchResult?.Author);
        Assert.AreEqual(patchItem.Title, patchResult?.Title);
        Assert.AreEqual(patchItem.Body, patchResult?.Body);
        Assert.AreEqual(patchItem.Category, patchResult?.Category);
        Assert.AreEqual(patchItem.Region, patchResult?.Region);
        Assert.AreEqual(patchItem.EndDate, patchResult?.EndDate);
        for (int i = 0; i < patchResult?.ContactDetails.Capacity; i++)
        {
            Assert.AreEqual(patchItem.ContactDetails[i].Email, patchResult.ContactDetails[i].Email);
            Assert.AreEqual(patchItem.ContactDetails[i].TelephoneNumber, patchResult.ContactDetails[i].TelephoneNumber);
            Assert.AreEqual(patchItem.ContactDetails[i].Name, patchResult.ContactDetails[i].Name);
        }
    }

    [TestMethod]
    public async Task CanFilterOnStartDate()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var requests = GetFilterableNewsItems();
        foreach (var item in requests)
        {
            _ = await client.PostAsJsonAsync("/api/NewsItem", item);
        }

        var newItems = await client.GetFromJsonAsync<List<NewsItemDto>>("/api/NewsItem");
        Assert.IsNotNull(newItems);
        Assert.AreEqual(requests.Count, newItems?.Count);

        var filtered = await client.GetFromJsonAsync<List<NewsItemDto>>($"/api/NewsItem?StartDate={(DateTime.MaxValue - TimeSpan.FromMinutes(1)).ToString("s")}");
        Assert.IsNotNull(filtered);
        //Assert.Single(filtered);
        
    }

    [TestMethod]
    public async Task CanFilterOnEndDate()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var requests = GetFilterableNewsItems();
        foreach (var item in requests)
        {
            _ = await client.PostAsJsonAsync("/api/NewsItem", item);
        }

        var newItems = await client.GetFromJsonAsync<List<NewsItemDto>>("/api/NewsItem");
        Assert.IsNotNull(newItems);
        Assert.AreEqual(requests.Count, newItems?.Count);

        var filtered = await client.GetFromJsonAsync<List<NewsItemDto>>($"/api/NewsItem?EndDate={(DateTime.Now + TimeSpan.FromMinutes(1)).ToString("s")}");
        Assert.IsNotNull(filtered);
        //Assert.Single(filtered);
    }

    [TestMethod]
    public async Task CanFilterOnAuthor()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var requests = GetFilterableNewsItems();
        foreach (var item in requests)
        {
            _ = await client.PostAsJsonAsync("/api/NewsItem", item);
        }

        var newItems = await client.GetFromJsonAsync<List<NewsItemDto>>("/api/NewsItem");
        Assert.IsNotNull(newItems);
        Assert.AreEqual(requests.Count, newItems?.Count);

        var filtered = await client.GetFromJsonAsync<List<NewsItemDto>>($"/api/NewsItem?Author={requests[0].Author}");
        Assert.IsNotNull(filtered);
        //Assert.Single(filtered);
    }

    [TestMethod]
    public async Task CanFilterOnStatus()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var requests = GetFilterableNewsItems();
        foreach (var item in requests)
        {
            _ = await client.PostAsJsonAsync("/api/NewsItem", item);
        }

        var newItems = await client.GetFromJsonAsync<List<NewsItemDto>>("/api/NewsItem");
        Assert.IsNotNull(newItems);
        Assert.AreEqual(requests.Count, newItems?.Count);

        var filtered = await client.GetFromJsonAsync<List<NewsItemDto>>($"/api/NewsItem?Status={requests[0].Status}");
        Assert.IsNotNull(filtered);
        //Assert.Single(filtered);
    }

    private static List<NewsItemDto> GetFilterableNewsItems()
    {
        var toReturn = new List<NewsItemDto>();

        var dto1 = DtoBuilder.BuildAddNewsItemRequest();
        dto1.ProductionDate = DateTime.MaxValue;
        dto1.Author = "author1";
        dto1.Status = Status.DONE;
        dto1.EndDate = DateTime.Today;
        toReturn.Add(dto1);

        var dto2 = DtoBuilder.BuildAddNewsItemRequest();
        dto2.ProductionDate = DateTime.Now;
        dto2.Author = "author2";
        dto2.Status = Status.DELETED;
        dto2.EndDate = DateTime.MaxValue;
        toReturn.Add(dto2);

        var dto3 = DtoBuilder.BuildAddNewsItemRequest();
        dto3.ProductionDate = DateTime.Now - TimeSpan.FromDays(1);
        dto3.Author = "author3";
        dto3.Status = Status.INPRODUCTION;
        dto3.EndDate = DateTime.MaxValue;
        toReturn.Add(dto3);

        return toReturn;
    }

}