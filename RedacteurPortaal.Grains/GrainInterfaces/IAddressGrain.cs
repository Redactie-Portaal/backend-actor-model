using RedacteurPortaal.DomainModels.Adress;

namespace RedacteurPortaal.Grains.GrainInterfaces;

public interface IAddressGrain : IManageableGrain<AddressModel>
{
    Task<AddressModel> UpdateAddress(AddressModel address);
}
