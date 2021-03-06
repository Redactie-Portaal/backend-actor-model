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

    public Task<bool> HasState()
    {
        return Task.FromResult(this.videoItem.RecordExists);
    }

    public Task<MediaVideoItem> Get() 
    {
        var state = this.videoItem.State;
        state.Id = this.GetGrainIdentity().PrimaryKey;
        return Task.FromResult(state);
    }

    public async Task Delete() 
    {
        await this.videoItem.ClearStateAsync();
    }

    public async Task<MediaVideoItem> Update(MediaVideoItem videoItem)
    {
        this.videoItem.State = videoItem;
        await this.videoItem.WriteStateAsync();
        return await this.Get();
    }
}