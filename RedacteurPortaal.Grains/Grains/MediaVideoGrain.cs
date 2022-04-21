using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.Grains.GrainInterfaces;

namespace RedacteurPortaal.Grains.Grains;

public class MediaVideoGrain : Grain, IMediaVideoGrain
{
    private readonly IPersistentState<MediaVideoItem> videoItem;

    public bool HasState => this.videoItem.RecordExists;

    public MediaVideoGrain(
        [PersistentState("videoItem", "OrleansStorage")]
        IPersistentState<MediaVideoItem> videoItem)
    {
        this.videoItem = videoItem;
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