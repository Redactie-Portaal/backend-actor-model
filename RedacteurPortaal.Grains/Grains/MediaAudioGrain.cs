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

    public MediaAudioItem GetMediaAudioItem(Guid guid)
    {
        return this.audioItem.State;
    }

    public async Task AddMediaAudioItem(MediaAudioItem item)
    {
        this.audioItem.State = item;
        await this.audioItem.WriteStateAsync();
    }

    public async Task DeleteMediaAudioItem(Guid guid)
    {
        await this.audioItem.ClearStateAsync();
    }

    public async Task UpdateMediaAudioItem(MediaAudioItem item)
    {
        this.audioItem.State = item;
        await this.audioItem.WriteStateAsync();
    }
}