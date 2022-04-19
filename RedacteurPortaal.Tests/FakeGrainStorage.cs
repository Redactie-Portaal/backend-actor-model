using Orleans;
using Orleans.Runtime;
using Orleans.Storage;
using RedacteurPortaal.DomainModels.NewsItem;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.Tests;

public class FakeGrainStorage : IGrainStorage
{
    private readonly string name;


    public FakeGrainStorage()
    {
        this.name = "OrleansStorage";
    }
    
    public ConcurrentDictionary<GrainReference, IGrainState> Storage { get; } = new();

    public Task ClearStateAsync(string grainType, GrainReference grainReference, IGrainState grainState)
        => Task.FromResult(Storage.TryRemove(grainReference, out _));


    public Task ReadStateAsync(string grainType, GrainReference grainReference, IGrainState grainState)
        => Task.CompletedTask;


    public Task WriteStateAsync(string grainType, GrainReference grainReference, IGrainState grainState)
        => Task.FromResult(Storage.TryAdd(grainReference, grainState));

    public TState GetGrainState<TState>(IGrain grain)
    {
        //var stor = Storage.TryGetValue((GrainReference)grain, out var state) ? (TState)state.State : default;
        var stor = Storage.TryGetValue((GrainReference)grain, out var state);
        //return stor;
        return (TState)state.State;
    }
    //=> Storage.TryGetValue((GrainReference)grain, out var state) ? (TState)state.State : default;
}
