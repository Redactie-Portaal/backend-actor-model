﻿using Orleans;
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
    public ConcurrentDictionary<GrainReference, IGrainState> Storage { get; } = new();

    public Task ClearStateAsync(string grainType, GrainReference grainReference, IGrainState grainState)
        => Task.FromResult(Storage.TryRemove(grainReference, out _));


    public Task ReadStateAsync(string grainType, GrainReference grainReference, IGrainState grainState)
        => Task.CompletedTask;


    public Task WriteStateAsync(string grainType, GrainReference grainReference, IGrainState grainState)
        => Task.FromResult(Storage.TryAdd(grainReference, grainState));

#pragma warning disable CS8603 // Possible null reference return.
    public TState GetGrainState<TState>(IGrain grain)
    //{
    //    //var stor = Storage.TryGetValue((GrainReference)grain, out var state) ? (TState)state.State : default;
    //    var stor = Storage.TryGetValue((GrainReference)grain, out var state);
    //    //return stor;
    //    return (TState)state.State;
    //}

    => Storage.TryGetValue((GrainReference)grain, out var state) ? (TState)state.State : default;
#pragma warning restore CS8603 // Possible null reference return.
}
