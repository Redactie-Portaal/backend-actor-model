using Orleans;
using Orleans.Runtime;
using Orleans.Storage;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.Tests
{
    public class FakeGrainStorage : IGrainStorage
    {
        public ConcurrentDictionary<GrainReference, IGrainState> Storage { get; } = new();

        public Task ClearStateAsync(string grainType, GrainReference grainReference, IGrainState grainState)
            => Task.FromResult(Storage.TryRemove(grainReference, out _));

        public Task ReadStateAsync(string grainType, GrainReference grainReference, IGrainState grainState)
            => Task.FromResult(Storage.TryGetValue(grainReference, out grainState));

        public Task WriteStateAsync(string grainType, GrainReference grainReference, IGrainState grainState)
            => Task.FromResult(Storage.TryAdd(grainReference, grainState));
        public TState GetGrainState<TState>(IGrain grain)
    => Storage.TryGetValue((GrainReference)grain, out var state)
        ? (TState)state.State
        : default;

    }

}
