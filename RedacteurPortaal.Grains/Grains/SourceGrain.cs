using Orleans.Runtime;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.Grains.GrainInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.Grains.Grains;

public class SourceGrain : ISourceGrain
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
        return Task.FromResult(this.source.State);
    }

    public async Task Delete()
    {
        await this.source.ClearStateAsync();
    }

    public async Task Update(Source source)
    {
        this.source.State = source;
        await this.source.WriteStateAsync();
    }
}