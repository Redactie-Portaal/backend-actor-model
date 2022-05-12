using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.Grains.GrainInterfaces;

namespace RedacteurPortaal.Grains.Grains;

public class SourceGrain : Grain, ISourceGrain
{
    private readonly IPersistentState<Source> source;

    public SourceGrain(
    [PersistentState("source", "OrleansStorage")]
    IPersistentState<Source> source)
    {
        this.source = source;
    }

    public Task<bool> HasState()
    {
        return Task.FromResult(this.source.RecordExists);
    }

    public Task<Source> Get()
    {
        var state = this.source.State;
        state.Id = this.GetGrainIdentity().PrimaryKey;
        return Task.FromResult(state);
    }

    public async Task Delete()
    {
        await this.source.ClearStateAsync();
    }

    public async Task Update(Source source)
    {
        this.source.State = source;
        await this.source.WriteStateAsync();
        return await this.Get();
    }
}