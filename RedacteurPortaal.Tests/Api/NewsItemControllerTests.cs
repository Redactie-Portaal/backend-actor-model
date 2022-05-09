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

public class NewsItemControllerTests
{

    [Fact]
    public async Task DefaultIsEmpty()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();
        var newsItems = await client.GetFromJsonAsync<List<NewsItemDto>>("/api/NewsItem");

        Assert.NotNull(newsItems);
        Assert.True(newsItems?.Count == 0);
    }

    [Fact]
    public async Task CanDelete()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addNewsItemRequest = DtoBuilder.BuildAddNewsItemRequest();
        var itemResult = await client.PostAsJsonAsync("/api/NewsItem", addNewsItemRequest);
        var result = JsonSerializer.Deserialize<NewsItemDto>(await itemResult.Content.ReadAsStringAsync(), new JsonSerializerOptions() {PropertyNameCaseInsensitive = true});

        var delete = await client.DeleteAsync($"/api/NewsItem/{result?.Id}");
        Assert.Equal(HttpStatusCode.NoContent, delete.StatusCode);

        var newAddress = await client.GetFromJsonAsync<List<NewsItemDto>>("/api/NewsItem");
        Assert.Empty(newAddress);
    }

    [Fact]
    public async Task CanAdd()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var newsItemRequest = DtoBuilder.BuildAddNewsItemRequest();
        var newsItem = await client.PostAsJsonAsync("/api/NewsItem", newsItemRequest);
        var resultString = await newsItem.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<NewsItemDto>(resultString, new JsonSerializerOptions() {PropertyNameCaseInsensitive = true});

        Assert.NotNull(result);
        Assert.Equal(newsItemRequest.Author, result?.Author);
        Assert.Equal(newsItemRequest.Title, result?.Title);
        Assert.Equal(newsItemRequest.Body, result?.Body);
        Assert.Equal(newsItemRequest.Category, result?.Category);
        Assert.Equal(newsItemRequest.Region, result?.Region);
        Assert.Equal(newsItemRequest.Audio, result?.Audio);
        Assert.Equal(newsItemRequest.Photos, result?.Photos);
        Assert.Equal(newsItemRequest.Videos, result?.Videos);
        Assert.Equal(newsItemRequest.ContactDetails, result?.ContactDetails);
        Assert.Equal(newsItemRequest.EndDate, result?.EndDate);
        for (var i = 0; i < result?.ContactDetails.Capacity; i++)
        {
            Assert.Equal(newsItemRequest.ContactDetails[i].Email, result.ContactDetails[i].Email);
            Assert.Equal(newsItemRequest.ContactDetails[i].TelephoneNumber, result.ContactDetails[i].TelephoneNumber);
            Assert.Equal(newsItemRequest.ContactDetails[i].Name, result.ContactDetails[i].Name);
        }

        var newItems = await client.GetFromJsonAsync<List<NewsItemDto>>("/api/NewsItem");
        Assert.NotNull(newItems);
        Assert.True(newItems?.Count == 1);
    }

    [Fact]
    public async Task CanUpdate()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var newsItemRequest = DtoBuilder.BuildAddNewsItemRequest();
        var newsItem = await client.PostAsJsonAsync("/api/NewsItem", newsItemRequest);
        var resultString = await newsItem.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<NewsItemDto>(resultString, new JsonSerializerOptions() {PropertyNameCaseInsensitive = true});


        Assert.NotNull(result);
        Assert.Equal(newsItemRequest.Title, result?.Title);

        var patchItem = DtoBuilder.BuildUpdateNewsItemRequest();
        var patchContent = new StringContent(JsonSerializer.Serialize(patchItem), Encoding.UTF8, "application/json");
        var newItem = await client.PatchAsync($"/api/NewsItem/{result?.Id}",patchContent );

        var patchResult = JsonSerializer.Deserialize<NewsItemDto>(await newItem.Content.ReadAsStringAsync(), new JsonSerializerOptions() {PropertyNameCaseInsensitive = true});
        Assert.Equal(HttpStatusCode.OK, newItem.StatusCode);

        Assert.NotNull(patchResult);
        Assert.Equal(patchItem.Author, patchResult?.Author);
        Assert.Equal(patchItem.Title, patchResult?.Title);
        Assert.Equal(patchItem.Body, patchResult?.Body);
        Assert.Equal(patchItem.Category, patchResult?.Category);
        Assert.Equal(patchItem.Region, patchResult?.Region);
        Assert.Equal(patchItem.Audio, patchResult?.Audio);
        Assert.Equal(patchItem.Photos, patchResult?.Photos);
        Assert.Equal(patchItem.Videos, patchResult?.Videos);
        Assert.Equal(patchItem.ContactDetails, patchResult?.ContactDetails);
        Assert.Equal(patchItem.EndDate, patchResult?.EndDate);
        for (int i = 0; i < patchResult?.ContactDetails.Capacity; i++)
        {
            Assert.Equal(patchItem.ContactDetails[i].Email, patchResult.ContactDetails[i].Email);
            Assert.Equal(patchItem.ContactDetails[i].TelephoneNumber, patchResult.ContactDetails[i].TelephoneNumber);
            Assert.Equal(patchItem.ContactDetails[i].Name, patchResult.ContactDetails[i].Name);
        }
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