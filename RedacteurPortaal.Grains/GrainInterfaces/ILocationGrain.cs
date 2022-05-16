using RedacteurPortaal.DomainModels.Shared;

namespace RedacteurPortaal.Grains.GrainInterfaces;

public interface ILocationGrain : IManageableGrain<Location>
{
    Task<Location> Update(Location location);
}
