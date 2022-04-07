using Orleans.Runtime;
using RedacteurPortaal.DomainModels.NewsItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.Grains.Grains;

public class SourceGrain
{
    private readonly IPersistentState<Source> source;

    public SourceGrain(
        [PersistentState("source", "OrleansStorage")]
        IPersistentState<Source> source)
    {
        this.source = source;
    }

    public Task<Source> GetSource(Guid guid)
    {
        return Task.FromResult(this.source.State);
    }

    public async Task AddSource(Source item)
    {
        this.source.State = item;
        await this.source.WriteStateAsync();
    }

    public async Task DeleteSource(Guid guid)
    {
        await this.source.ClearStateAsync();
    }

    public async Task UpdateSource(Source item)
    {
        this.source.State = item;
        await this.source.WriteStateAsync();
    }
}