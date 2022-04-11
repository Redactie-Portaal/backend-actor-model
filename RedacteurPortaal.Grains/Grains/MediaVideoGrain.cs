using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.Grains.GrainInterfaces;

namespace RedacteurPortaal.Grains.Grains;

public class MediaVideoGrain : Grain, IMediaVideoGrain
{
    private readonly IPersistentState<MediaVideoItem> videoItem;

    public MediaVideoGrain(
        [PersistentState("videoItem", "OrleansStorage")]
        IPersistentState<MediaVideoItem> videoItem)
    {
        this.videoItem = videoItem;
    }

    public Task<MediaVideoItem> GetMediaVideoItem() 
    {
        return Task.FromResult(this.videoItem.State);
    }

    public async Task DeleteMediaVideoItem() 
    {
        await this.videoItem.ClearStateAsync();
    }

    public async Task UpdateMediaVideoItem(MediaVideoItem videoItem)
    {
        this.videoItem.State = videoItem;
        await this.videoItem.WriteStateAsync();
    }
}