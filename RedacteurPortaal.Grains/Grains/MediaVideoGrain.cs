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

    public MediaVideoItem GetMediaVideoItem(Guid guid) 
    {
        return this.videoItem.State;
    }

    public async Task AddMediaVideoItem(MediaVideoItem videoItem)
    {
        await this.videoItem.WriteStateAsync();
    }

    public async Task DeleteMediaVideoItem(Guid guid) 
    {
        await this.videoItem.ClearStateAsync();
    }

    public async Task UpdateMediaVideoItem(MediaVideoItem videoItem)
    {
        await this.videoItem.WriteStateAsync();
    }
}