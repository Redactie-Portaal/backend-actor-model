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
            // TODO control if adress date is not empty
           this.adress.State.CompanyName = address.CompanyName;
           this.adress.State.PhoneNumber = address.PhoneNumber;
           this.adress.State.EmailAddress = address.EmailAddress;
           this.adress.State.PostalCode = address.PostalCode;
           this.adress.State.Webpage = address.Webpage;
           this.adress.State.Address = address.Address;
           var adress1 = this.adress;
           await adress1.WriteStateAsync();
        }

        public async Task Delete()
        {
            await this.adress.ClearStateAsync();
        }

        public Task<AddressModel> Get()
        {
            var state = this.adress.State;
            state.Id = this.GetGrainIdentity().PrimaryKey;
            this.logger.LogInformation("foobar");
            return Task.FromResult(this.adress.State);
        }
    }
}
