using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.Grains.GrainInterfaces;

namespace RedacteurPortaal.Grains.Grains;

public class MediaAudioGrain : Grain, IMediaAudioGrain
{
    private readonly IPersistentState<MediaAudioItem> audioItem;

    public Task<bool> HasState()
    {
        return Task.FromResult(this.audioItem.RecordExists);
    }

    public MediaAudioGrain(
#if DEBUG
        // This works in testing.
        [PersistentState("audioItem")]
#else
        // This doesn't work in testing, but I don't know why.
        [PersistentState("audioItem", "OrleansStorage")]
#endif
        IPersistentState<MediaAudioItem> audioItem)
    {
        this.audioItem = audioItem;
    }

    public async Task AddMediaAudioItem(MediaAudioItem item)
    {
        this.audioItem.State = item;
        await this.audioItem.WriteStateAsync();
    }

    public Task<MediaAudioItem> Get()
    {
        return Task.FromResult(this.audioItem.State);
    }

    public async Task Delete()
    {
        await this.audioItem.ClearStateAsync();
    }

    public async Task Update(MediaAudioItem mediaItem)
    {
        this.audioItem.State = mediaItem;
        await this.audioItem.WriteStateAsync();
    }


}