﻿using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.Grains.GrainInterfaces;

namespace RedacteurPortaal.Grains.Grains;

public class MediaVideoGrain : Grain, IMediaVideoGrain
{
    private readonly IPersistentState<MediaVideoItem> videoItem;
    
    public MediaVideoGrain(
#if DEBUG
        // This works in testing.
        [PersistentState("videoItem")]
#else
        // This doesn't work in testing, but I don't know why.
        [PersistentState("videoItem", "OrleansStorage")]
#endif
        IPersistentState<MediaVideoItem> videoItem)
    {
        this.videoItem = videoItem;
    }

    public async Task AddMediaVideoItem(MediaVideoItem item)
    {
        this.videoItem.State = item;
        await this.videoItem.WriteStateAsync();
    }

    public Task<bool> HasState()
    {
        return Task.FromResult(this.videoItem.RecordExists);
    }

    public Task<MediaVideoItem> Get() 
    {
        return Task.FromResult(this.videoItem.State);
    }

    public async Task Delete() 
    {
        await this.videoItem.ClearStateAsync();
    }

    public async Task Update(MediaVideoItem videoItem)
    {
        this.videoItem.State = videoItem;
        await this.videoItem.WriteStateAsync();
    }


}