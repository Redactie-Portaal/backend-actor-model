using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.DomainModels;
using RedacteurPortaal.DomainModels.Shared;
using RedacteurPortaal.Grains.GrainInterfaces;

namespace RedacteurPortaal.Grains.Grains;
public class LocationGrain : Grain, ILocationGrain
{
    private readonly IPersistentState<Location> location;

    public LocationGrain(
    [PersistentState("location","OrleansStorage")]
    IPersistentState<Location> location)
    {
        this.location = location;
    }

    public Task<bool> HasState()
    {
        return Task.FromResult(this.location.RecordExists);
    }

    public async Task<Location> Get()
    {
        await this.location.ReadStateAsync();
        var state = this.location.State;
        state.Id = this.GetGrainIdentity().PrimaryKey;
        return state;
    }

    public async Task<Location> Update(Location location)
    {
        this.location.State = location;
        await this.location.WriteStateAsync();
        return await this.Get();
    }

    public async Task Delete()
    {
        await this.location.ClearStateAsync();
    }
}