
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
            //control if guid is not empty
            //_adress.State.Id = guid;
            var adress = await Task.FromResult(_adress.State);
            _adress.State = adress;
            return adress;
        }

        public async Task AddAdress(AddressModel address)
        {
            // control Adress fields not null
            _adress.State = address;
            await _adress.WriteStateAsync();
        }

        public Task<List<AddressModel>> GetAdresses()
        {
            var adresses = await Task.
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveAdress(Guid guid)
        {
            //Delete Address
            GrainFactory.GetGrain<IAddressGrain>(guid);
            await _adress.ClearStateAsync();

            // Control if address is deleted
            GrainFactory.GetGrain<IAddressGrain>(guid);
            var adress = await Task.FromResult(_adress.State);
            if(adress != null)
            {
                return false;
            }
            return true;
        }

        public async Task UpdateAdress(AddressModel address)
        {
            //control if adress date is not empty
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
