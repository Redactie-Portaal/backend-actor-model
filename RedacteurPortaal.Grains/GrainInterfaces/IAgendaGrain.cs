using RedacteurPortaal.DomainModels.Agenda;

namespace RedacteurPortaal.Grains.GrainInterfaces
{
    public interface IAgendaGrain : IManageableGrain<AgendaModel>
    {
        Task<AgendaModel> UpdateAgenda(AgendaModel agenda);
    }
}