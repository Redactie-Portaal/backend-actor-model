using Orleans;
using RedacteurPortaal.DomainModels.Adress;

namespace RedacteurPortaal.Grains.GrainInterfaces
{
    public interface IAddressGrain : IManageableGrain<AddressModel>
    {
        Task UpdateAddress(AddressModel address);
    }
}
