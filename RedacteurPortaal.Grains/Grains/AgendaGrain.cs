using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.DomainModels.Agenda;
using RedacteurPortaal.Grains.GrainInterfaces;

namespace RedacteurPortaal.Grains.Grains
{
    public class AgendaGrain : Grain, IAgendaGrain
    {
        private readonly IPersistentState<AgendaModel> Agenda;

        public AgendaGrain([PersistentState("agenda", "OrleansStorage")] IPersistentState<AgendaModel> agenda)
        {
            this.Agenda = agenda;
        }

        public async Task Delete()
        {
            await this.Agenda.ClearStateAsync();
        }

        public Task<AgendaModel> Get()
        {
            return Task.FromResult(this.Agenda.State);
        }

        public Task<bool> HasState()
        {
            return Task.FromResult(this.Agenda.RecordExists);
        }

        public async Task<AgendaModel> UpdateAgenda(AgendaModel agenda)
        {
            this.Agenda.State = agenda;
            await this.Agenda.WriteStateAsync();
            return this.Agenda.State;
        }
    }
}