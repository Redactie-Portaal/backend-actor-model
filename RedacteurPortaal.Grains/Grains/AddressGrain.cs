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

     /*   public async Task AddAdress(AddressModel address)
        {
            // control Adress fields not null
            this.adress.State = address;
            await this.adress.WriteStateAsync();
        }*/

        public async Task UpdateAdress(AddressModel address)
        {
            // TODO control if adress date is not empty
           this.adress.State.CompanyName = address.CompanyName;
           this.adress.State.PhoneNumber = address.CompanyName;
           this.adress.State.EmailAddress = address.CompanyName;
           this.adress.State.PostalCode = address.CompanyName;
           this.adress.State.Webpage = address.CompanyName;
           this.adress.State.Address = address.CompanyName;
           var adress1 = this.adress;
           await adress1.WriteStateAsync();
        }

        public async Task Delete()
        {
            await this.adress.ClearStateAsync();
        }

        public Task<AddressModel> Get()
        {
            return Task.FromResult(this.adress.State);
        }
    }
}
