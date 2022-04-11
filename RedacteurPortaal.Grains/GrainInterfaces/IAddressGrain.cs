using Orleans;
using RedacteurPortaal.DomainModels.Adress;

namespace RedacteurPortaal.Grains.GrainInterfaces
{
    public interface IAddressGrain : IGrainWithGuidKey
    {
        Task AddAdress(AddressModel address);

        Task<AddressModel> GetAddress(Guid guid);

        Task UpdateAdress(AddressModel address);

        Task RemoveAdress(Guid guid);

        Task<List<AddressModel>> GetAdresses();
    }
}
