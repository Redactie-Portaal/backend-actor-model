using Orleans;
using RedacteurPortaal.DomainModels.Adress;

namespace RedacteurPortaal.Grains.GrainInterfaces
{
    public interface IAddressGrain : IManageableGrain<AddressModel>
    {
        Task AddAddress(AddressModel address);

        Task UpdateAddress(AddressModel address);
    }
}
