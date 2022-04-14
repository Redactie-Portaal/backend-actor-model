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

    public Task<MediaAudioItem> Get()
    {
        return Task.FromResult(this.audioItem.State);
    }

    public async Task Delete()
    {
        await this.audioItem.ClearStateAsync();
    }

    public async Task Update(MediaAudioItem item)
    {
        this.audioItem.State = mediaAudio;
        await this.audioItem.WriteStateAsync();
    }
}