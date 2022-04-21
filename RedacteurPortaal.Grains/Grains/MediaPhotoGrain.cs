using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.Grains.GrainInterfaces;

namespace RedacteurPortaal.Grains.Grains;

public class MediaPhotoGrain : Grain, IMediaPhotoGrain
{
    private readonly IPersistentState<MediaPhotoItem> photoItem;

    public MediaPhotoGrain(
#if DEBUG
        // This works in testing.
        [PersistentState("photoItem")]
#else
        // This doesn't work in testing, but I don't know why.
        [PersistentState("photoItem", "OrleansStorage")]
#endif
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

    public async Task Update(MediaPhotoItem photoItem)
    {
        this.photoItem.State = photoItem;
        await this.photoItem.WriteStateAsync();
    }
}