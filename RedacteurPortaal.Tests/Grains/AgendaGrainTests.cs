using System;
using System.Threading.Tasks;
using Orleans.TestingHost;
using RedacteurPortaal.DomainModels.Agenda;
using RedacteurPortaal.Grains.GrainInterfaces;
using Xunit;

namespace RedacteurPortaal.Tests.Grains;

[Collection("Col")]
public class AgendaGrainTests
{
    private readonly TestCluster _cluster;

    public AgendaGrainTests(ClusterFixture fixture)
    {
        this._cluster = fixture.Cluster;
    }

    [Fact]
    public async Task CanAddAgendaItemCorrectly()
    {
        var guid = Guid.NewGuid();

        var toSaveAgendaItem = new AgendaModel {
            Id = guid,
            StartDate = DateTime.Now,
            EndDate = new DateTime(2022, 05, 12, 15, 00, 00),
            Title = "foo1",
            Description = "boo1",
            UserId = "string"
        };

        var agendaGrain = this._cluster.GrainFactory.GetGrain<IAgendaGrain>(guid);

        await agendaGrain.UpdateAgenda(toSaveAgendaItem);

        var agenda = await agendaGrain.Get();

        Assert.Equal("foo1", agenda.Title);
        Assert.Equal(guid, agenda.Id);
    }
}