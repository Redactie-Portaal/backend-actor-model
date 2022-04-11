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

    public Task<Location> GetLocation(Guid guid)
    {
        return Task.FromResult(this.location.State);
    }
    
    public async Task AddLocation(Location item)
    {
        this.location.State = item;
        await this.location.WriteStateAsync();
    }

    public async Task UpdateLocation(Location item)
    {
        this.location.State = item;
        await this.location.WriteStateAsync();
    }

    public async Task DeleteLocation(Guid guid)
    {
        await this.location.ClearStateAsync();
    }
}