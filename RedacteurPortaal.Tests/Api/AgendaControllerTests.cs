using System.Net.Http.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RedacteurPortaal.Api.DTOs;
using Xunit;

namespace RedacteurPortaal.Tests.Api;

public class AgendaControllerTests
{

    [Fact]
    public async Task DefaultIsEmpty()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();
        var addresses = await client.GetFromJsonAsync<List<AgendaDto>>("/api/Agenda");

        Assert.NotNull(addresses);
        Assert.True(addresses?.Count == 0);
    }

    [Fact]
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
        
        Assert.NotNull(result);
        Assert.Equal(agendaItem.Title, result?.Title);
        Assert.Equal(agendaItem.StartDate, result?.StartDate);
        Assert.Equal(agendaItem.EndDate, result?.EndDate);
        Assert.Equal(agendaItem.Description, result?.Description);

        var agenda = await client.GetFromJsonAsync<List<AgendaDto>>("api/Agenda");
        
        Assert.NotNull(agenda);
        Assert.True(agenda?.Count == 1);
    }

    [Fact]
    public async Task CanUpdate()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addAgendaRequest = DtoBuilder.BuildAgendaRequest();
        var agendaResult = await client.PostAsJsonAsync($"/api/Agenda", addAgendaRequest);
        var resultString = await agendaResult.Content.ReadAsStreamAsync();
        var result = JsonSerializer.Deserialize<AgendaDto>(resultString, new JsonSerializerOptions() {
            PropertyNameCaseInsensitive = true
        });
        
        Assert.NotNull(result);
        Assert.Equal(addAgendaRequest.Title, result?.Title);

        var patchAgendaRequest = DtoBuilder.BuildPatchAgendaRequest();
        var patchContent = new StringContent(JsonSerializer.Serialize(patchAgendaRequest), Encoding.UTF8, "application/json");
        var newAgenda = await client.PatchAsync($"/api/Agenda/{result?.Id}", patchContent);

        var patchResult = JsonSerializer.Deserialize<AgendaDto>(await newAgenda.Content.ReadAsStringAsync(),
            new JsonSerializerOptions() {
                PropertyNameCaseInsensitive = true
            });
        
        Assert.Equal(HttpStatusCode.OK, newAgenda.StatusCode);
        Assert.Equal(patchAgendaRequest.Title, patchResult?.Title);
        Assert.Equal(patchAgendaRequest.StartDate, patchResult?.StartDate);
        Assert.Equal(patchAgendaRequest.EndDate, patchResult?.EndDate);
        Assert.Equal(patchAgendaRequest.Description, patchResult?.Description);
    }

    [Fact]
    public async Task CanDelete()
    {
        var application = new RedacteurPortaalApplication();
        var client = application.CreateClient();

        var addAgendaRequest = DtoBuilder.BuildAgendaRequest();
        var agendaResult = await client.PostAsJsonAsync("/api/Agenda", addAgendaRequest);
        var result = JsonSerializer.Deserialize<AddressDTO>(await agendaResult.Content.ReadAsStringAsync(), new JsonSerializerOptions() {PropertyNameCaseInsensitive = true});

        var delete = await client.DeleteAsync($"/api/Agenda/{result?.Id}");
        Assert.Equal(HttpStatusCode.NoContent, delete.StatusCode);

        var newAgenda = await client.GetFromJsonAsync<List<AgendaDto>>("/api/Agenda");
        Assert.Empty(newAgenda);
    }
}