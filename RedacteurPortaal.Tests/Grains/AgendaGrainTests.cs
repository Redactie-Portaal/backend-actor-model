using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orleans.TestingHost;
using RedacteurPortaal.DomainModels.Agenda;
using RedacteurPortaal.Grains.GrainInterfaces;

namespace RedacteurPortaal.Tests.Grains;

[TestClass]
public class AgendaGrainTests
{
    private TestCluster _cluster;

    [TestInitialize]
    public void Initialize()
    {
        this._cluster = new ClusterFixture().Cluster;
    }


    [TestMethod]
    public async Task CanAddAgendaItemCorrectly()
    {
        var guid = Guid.NewGuid();

        var toSaveAgendaItem = new AgendaModel {
            Id = guid,
            StartDate = new DateTime(2022, 05, 12, 15, 00, 00),
            EndDate = new DateTime(2022, 05, 12, 18, 00, 00),
            Title = "foo1",
            Description = "boo1",
            UserId = "string"
        };

        var agendaGrain = this._cluster.GrainFactory.GetGrain<IAgendaGrain>(guid);

        await agendaGrain.UpdateAgenda(toSaveAgendaItem);

        var agenda = await agendaGrain.Get();

        Assert.AreEqual(guid, agenda.Id);
        Assert.AreEqual("foo1", agenda.Title);
        Assert.AreEqual("boo1", agenda.Description);
        Assert.AreEqual(new DateTime(2022, 05, 12, 15, 00, 00), agenda.StartDate);
        Assert.AreEqual(new DateTime(2022, 05, 12, 18, 00, 00), agenda.EndDate);
        Assert.AreEqual("string", agenda.UserId);
    }
}