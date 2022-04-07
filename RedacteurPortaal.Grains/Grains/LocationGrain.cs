using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.Grains.GrainInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        var grain = this.GrainFactory.GetGrain<ILocationGrain>(guid);
        return Task.FromResult(this.location.State);
    }

    public async Task AddLocation(Location location)
    {
        var grain = this.GrainFactory.GetGrain<ILocationGrain>(location.Id);
        this.location.State = location;
        await this.location.WriteStateAsync();
    }

    public async Task UpdateLocation(Location location)
    {
        this.location.State = location;
        await this.location.WriteStateAsync();
    }

    public async Task DeleteLocation(Guid guid)
    {
        var grain = this.GrainFactory.GetGrain<ILocationGrain>(guid);
        await this.location.ClearStateAsync();
    }
}