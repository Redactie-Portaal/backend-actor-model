using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.Grains.GrainInterfaces;

namespace RedacteurPortaal.Grains.Grains;

public class MediaAudioGrain : Grain, IMediaAudioGrain
{
    private readonly IPersistentState<MediaAudioItem> audioItem;
    
    public MediaAudioGrain(
    [PersistentState("audioItem", "OrleansStorage")]
    IPersistentState<MediaAudioItem> audioItem)
    {
        this.audioItem = audioItem;
    }
    
    public Task<bool> HasState()
    {
        return Task.FromResult(this.audioItem.RecordExists);
    }

    public Task<MediaAudioItem> Get()
    {
        return Task.FromResult(this.audioItem.State);
    }

    public async Task Delete()
    {
        await this.audioItem.ClearStateAsync();
    }

    public async Task<MediaAudioItem> Update(MediaAudioItem mediaItem)
    {
        this.audioItem.State = mediaItem;
        await this.audioItem.WriteStateAsync();
        return this.audioItem.State;
    }
}