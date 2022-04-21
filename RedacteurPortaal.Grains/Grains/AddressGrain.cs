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

        private readonly IPersistentState<AddressModel> address;

        public AddressGrain(ILogger<NewsItemGrain> logger,
#if DEBUG
        // This works in testing.
            [PersistentState("address")]
#else
        // This doesn't work in testing, but I don't know why.
            [PersistentState("address", "OrleansStorage")]
#endif
            IPersistentState<AddressModel> address)
        {
            this.logger = logger;
            this.address = address;
        }

        public Task<bool> HasState()
        {
            return Task.FromResult(this.address.RecordExists);
        }

        public async Task AddAddress(AddressModel address)
        {
            this.address.State = address;
            await this.address.WriteStateAsync();
        }

        public async Task UpdateAddress(AddressModel address)
        {
            // TODO control if address date is not empty
           this.address.State.CompanyName = address.CompanyName;
           this.address.State.PhoneNumber = address.PhoneNumber;
           this.address.State.EmailAddress = address.EmailAddress;
           this.address.State.PostalCode = address.PostalCode;
           this.address.State.Webpage = address.Webpage;
           this.address.State.Address = address.Address;
           var address1 = this.address;
           await address1.WriteStateAsync();
        }

        public async Task Delete()
        {
            await this.address.ClearStateAsync();
        }

        public Task<AddressModel> Get()
        {
            var state = this.address.State;
            state.Id = this.GetGrainIdentity().PrimaryKey;
            this.logger.LogInformation("foobar");
            return Task.FromResult(this.address.State);
        }
    }
}
