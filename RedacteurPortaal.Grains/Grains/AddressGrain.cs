
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.Grains.GrainInterfaces;
using RedacteurPortaal.DomainModels.Adress;

namespace RedacteurPortaal.Grains.Grains
{
    public class AddressGrain : Grain, IAddressGrain
    {
        private readonly ILogger _logger;


        private readonly IPersistentState<AddressModel> _adress;

        public AddressGrain(ILogger<NewsItemGrain> logger,
           [PersistentState("adress", "OrleansStorage")] IPersistentState<AddressModel> adress)
        {
            _logger = logger;
            _adress = adress;
        }

        public async Task<AddressModel> GetAddress(Guid guid)
        {
            return _adress.State;
        }

        public async Task AddAdress(AddressModel address)
        {
            // control Adress fields not null
            _adress.State = address;
            await _adress.WriteStateAsync();
        }

        public Task<List<AddressModel>> GetAdresses()
        {
            //var adresses = await Task.FromResult();
            throw new NotImplementedException();
        }

        public async Task RemoveAdress(Guid guid)
        {
            //Delete Address
            GrainFactory.GetGrain<IAddressGrain>(guid);
            await this._adress.ClearStateAsync();
        }

        public async Task UpdateAdress(AddressModel address)
        {
            // TODO control if adress date is not empty
           _adress.State.CompanyName = address.CompanyName;
           _adress.State.PhoneNumber = address.CompanyName;
           _adress.State.EmailAddress = address.CompanyName;
           _adress.State.PostalCode = address.CompanyName;
           _adress.State.Webpage = address.CompanyName;
           _adress.State.Address = address.CompanyName;
           await _adress.WriteStateAsync();
        }
    }
}
