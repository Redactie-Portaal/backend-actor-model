using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.Grains.GrainInterfaces;

namespace RedacteurPortaal.Grains.Grains;

public class MediaPhotoGrain : Grain, IMediaPhotoGrain
{
    private readonly IPersistentState<MediaPhotoItem> photoItem;

    public MediaPhotoGrain(
    [PersistentState("mediaItem","OrleansStorage")]
    IPersistentState<MediaPhotoItem> photoItem)
    {
        this.photoItem = photoItem;
    }

    public Task<bool> HasState()
    {
        return Task.FromResult(this.photoItem.RecordExists);
    }

    public Task<MediaPhotoItem> Get()
    {
        return Task.FromResult(this.photoItem.State);
    }

    public async Task Delete()
    {
        await this.photoItem.ClearStateAsync();
    }

    public async Task<MediaPhotoItem> Update(MediaPhotoItem mediaItem)
    {
        this.photoItem.State = mediaItem;
        await this.photoItem.WriteStateAsync();
        return this.photoItem.State;
    }
}