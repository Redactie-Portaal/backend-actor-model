using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.Grains.GrainInterfaces;
using RedacteurPortaal.DomainModels.Shared;

namespace RedacteurPortaal.Grains.Grains;
public class LocationGrain : Grain, ILocationGrain
{
    private readonly IPersistentState<Location> location;

    public LocationGrain(
        [PersistentState("location", "OrleansStorage")]
        IPersistentState<Location> location)
    {
        this.location = location;
    }

    public Task<Location> Get()
    {
        return Task.FromResult(this.location.State);
    }

    public async Task Update(Location location)
    {
        this.location.State = location;
        await this.location.WriteStateAsync();
    }

    public async Task Delete()
    {
        await this.location.ClearStateAsync();
    }
}