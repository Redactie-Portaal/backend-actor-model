using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.DomainModels.Agenda;
using RedacteurPortaal.Grains.GrainInterfaces;

namespace RedacteurPortaal.Grains.Grains
{
    public class AgendaGrain : Grain, IAgendaGrain
    {
        private readonly IPersistentState<AgendaModel> agenda;

        public AgendaGrain([PersistentState("agenda", "OrleansStorage")] IPersistentState<AgendaModel> agenda)
        {
            this.agenda = agenda;
        }

        public async Task Delete()
        {
            await this.agenda.ClearStateAsync();
        }

        public Task<AgendaModel> Get()
        {
            return Task.FromResult(this.agenda.State);
        }

        public Task<bool> HasState()
        {
            return Task.FromResult(this.agenda.RecordExists);
        }

        public async Task<AgendaModel> UpdateAgenda(AgendaModel agenda)
        {
            this.agenda.State = agenda;
            await this.agenda.WriteStateAsync();
            return this.agenda.State;
        }
    }
}