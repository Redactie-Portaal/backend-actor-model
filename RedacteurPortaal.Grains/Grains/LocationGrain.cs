using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.DomainModels.Shared;
using RedacteurPortaal.Grains.GrainInterfaces;

namespace RedacteurPortaal.Grains.Grains;
public class LocationGrain : Grain, ILocationGrain
{
    private readonly IPersistentState<Location> location;

    public Task<bool> HasState()
    {
        return Task.FromResult(this.location.RecordExists);
    }

    public LocationGrain(
#if DEBUG
        // This works in testing.
        [PersistentState("location")]
#else
        // This doesn't work in testing, but I don't know why.
        [PersistentState("location", "OrleansStorage")]
#endif
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