using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.Grains.GrainInterfaces;
using RedacteurPortaal.DomainModels.Adress;

namespace RedacteurPortaal.Grains.Grains
{
    public class AddressGrain : Grain, IAddressGrain
    {
        private readonly ILogger logger;

        private readonly IPersistentState<AddressModel> adress;

        public AddressGrain(ILogger<NewsItemGrain> logger,
           [PersistentState("adress", "OrleansStorage")] IPersistentState<AddressModel> adress)
        {
            this.logger = logger;
            this.adress = adress;
        }

        public Task<bool> HasState()
        {
            return Task.FromResult(this.adress.RecordExists);
        }

        public async Task UpdateAdress(AddressModel address)
        {
           this.adress.State= address;
           await this.adress.WriteStateAsync();
        }

        public async Task Delete()
        {
            await this.adress.ClearStateAsync();
        }

        public Task<AddressModel> Get()
        {
            var state = this.adress.State;
            state.Id = this.GetGrainIdentity().PrimaryKey;
            return Task.FromResult(this.adress.State);
        }
    }
}
