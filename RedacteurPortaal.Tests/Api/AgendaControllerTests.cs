using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedacteurPortaal.Api.DTOs;

namespace RedacteurPortaal.Tests.Api;

[TestClass]
public class AgendaControllerTests
{
    [TestMethod]
    public async Task DefaultIsEmpty()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();
        var agenda = await client.GetFromJsonAsync<List<AgendaDto>>("/api/Agenda");

        Assert.IsNotNull(agenda);
        Assert.IsTrue(agenda?.Count == 0);
    }

    [TestMethod]
    public async Task CanAdd()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var agendaItem = DtoBuilder.BuildAgendaRequest();
        var agendaResult = await client.PostAsJsonAsync("/api/Agenda", agendaItem);
        var resultString = await agendaResult.Content.ReadAsStreamAsync();

        var result = JsonSerializer.Deserialize<AgendaDto>(resultString, new JsonSerializerOptions() {
            PropertyNameCaseInsensitive = true
        });


        Assert.IsNotNull(result);
        Assert.AreEqual(agendaItem.Title, result?.Title);
        Assert.AreEqual(agendaItem.StartDate, result?.StartDate);
        Assert.AreEqual(agendaItem.EndDate, result?.EndDate);
        Assert.AreEqual(agendaItem.Description, result?.Description);

        var agenda = await client.GetFromJsonAsync<List<AgendaDto>>("api/Agenda");

        Assert.IsNotNull(agenda);
        Assert.IsTrue(agenda?.Count == 1);
    }

    [TestMethod]
    public async Task CanGetAll()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addAgendaRequest = DtoBuilder.BuildAgendaRequest();
        await client.PostAsJsonAsync("/api/Agenda", addAgendaRequest);

        var result = await client.GetAsync("/api/Agenda");
        var listResult = JsonSerializer.Deserialize<List<AgendaDto>>(await result.Content.ReadAsStringAsync(),
            new JsonSerializerOptions() {
                PropertyNameCaseInsensitive = true
            });

        Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        CollectionAssert.AllItemsAreNotNull(listResult);
    }

    [TestMethod]
    public async Task CanGetByGuid()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addAgendaRequest = DtoBuilder.ReadAgendaRequest();
        var agendaResult = await client.PostAsJsonAsync("/api/Agenda", addAgendaRequest);
        var addResult = JsonSerializer.Deserialize<AgendaReadDto>(await agendaResult.Content.ReadAsStringAsync(),
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        var getByIdResult = await client.GetAsync($"/api/Agenda/{addResult?.Id}");
        var getResult = JsonSerializer.Deserialize<AgendaReadDto>(await getByIdResult.Content.ReadAsStringAsync(),
            new JsonSerializerOptions() {
                PropertyNameCaseInsensitive = true
            });

        Assert.AreEqual(HttpStatusCode.OK, getByIdResult.StatusCode);
        Assert.AreEqual(addResult?.Id, getResult?.Id);
        Assert.AreEqual(addResult?.Title, getResult?.Title);
        Assert.AreEqual(addResult?.Description, getResult?.Description);
        Assert.AreEqual(addResult?.StartDate, getResult?.StartDate);
        Assert.AreEqual(addResult?.EndDate, getResult?.EndDate);
    }

    [TestMethod]
    public async Task CanFilterOnDateWithResult()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addAgendaRequest = DtoBuilder.BuildAgendaRequest();
        await client.PostAsJsonAsync("/api/Agenda", addAgendaRequest);


        var sortByDate = await client.GetAsync($"/api/Agenda/s?StartDate=2022-05-12T00%3A00&EndDate=2022-05-12T23%3A00");
        var getResult = JsonSerializer.Deserialize<List<AgendaDto>>(await sortByDate.Content.ReadAsStringAsync(),
            new JsonSerializerOptions() {
                PropertyNameCaseInsensitive = true
            });

        Assert.AreEqual(HttpStatusCode.OK, sortByDate.StatusCode);
        CollectionAssert.AllItemsAreNotNull(getResult);
    }

    [TestMethod]
    public async Task CanFilterOnDateWithoutResult()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addAgendaRequest = DtoBuilder.BuildAgendaRequest();
        await client.PostAsJsonAsync("/api/Agenda", addAgendaRequest);


        var sortByDate = await client.GetAsync($"/api/Agenda/s?StartDate=2022-05-13T00%3A00&EndDate=2022-05-15T23%3A00");
        var getResult = JsonSerializer.Deserialize<List<AgendaDto>>(await sortByDate.Content.ReadAsStringAsync(),
            new JsonSerializerOptions() {
                PropertyNameCaseInsensitive = true
            });

        Assert.AreEqual(HttpStatusCode.OK, sortByDate.StatusCode);
        CollectionAssert.AllItemsAreNotNull(getResult);
    }

    [TestMethod]
    public async Task CanUpdate()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addAgendaRequest = DtoBuilder.ReadAgendaRequest();
        var agendaResult = await client.PostAsJsonAsync($"/api/Agenda", addAgendaRequest);
        var resultString = await agendaResult.Content.ReadAsStreamAsync();
        var result = JsonSerializer.Deserialize<AgendaReadDto>(resultString, new JsonSerializerOptions() {
            PropertyNameCaseInsensitive = true
        });

        Assert.IsNotNull(result);
        Assert.AreEqual(addAgendaRequest.Title, result?.Title);

        var patchAgendaRequest = DtoBuilder.ReadAgendaRequest();
        var patchContent = new StringContent(JsonSerializer.Serialize(patchAgendaRequest), Encoding.UTF8,
            "application/json");
        var newAgenda = await client.PatchAsync($"/api/Agenda/{result?.Id}", patchContent);

        var patchResult = JsonSerializer.Deserialize<AgendaReadDto>(await newAgenda.Content.ReadAsStringAsync(),
            new JsonSerializerOptions() {
                PropertyNameCaseInsensitive = true
            });

        Assert.AreEqual(HttpStatusCode.OK, newAgenda.StatusCode);
        Assert.AreEqual(patchAgendaRequest.Title, patchResult?.Title);
        Assert.AreEqual(patchAgendaRequest.StartDate, patchResult?.StartDate);
        Assert.AreEqual(patchAgendaRequest.EndDate, patchResult?.EndDate);
        Assert.AreEqual(patchAgendaRequest.Description, patchResult?.Description);
    }

    [TestMethod]
    public async Task CanDelete()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addAgendaRequest = DtoBuilder.BuildAgendaRequest();
        var agendaResult = await client.PostAsJsonAsync("/api/Agenda", addAgendaRequest);
        var result = JsonSerializer.Deserialize<AddressDTO>(await agendaResult.Content.ReadAsStringAsync(),
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        var delete = await client.DeleteAsync($"/api/Agenda/{result?.Id}");
        Assert.AreEqual(HttpStatusCode.NoContent, delete.StatusCode);

        var newAgenda = await client.GetFromJsonAsync<List<AgendaDto>>("/api/Agenda");
        CollectionAssert.AllItemsAreNotNull(newAgenda);
    }
}