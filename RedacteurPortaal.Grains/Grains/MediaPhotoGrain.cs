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
        [PersistentState("photoItem", "OrleansStorage")]
        IPersistentState<MediaPhotoItem> photoItem)
    {
        this.photoItem = photoItem;
    }

    public MediaPhotoItem GetMediaPhotoItem(Guid guid)
    {
        return this.photoItem.State;
    }

    public async Task AddMediaPhotoItem(MediaPhotoItem item)
    {
        this.photoItem.State = item;
        await this.photoItem.WriteStateAsync();
    }

    public async Task DeleteMediaPhotoItem(Guid guid)
    {
        await this.photoItem.ClearStateAsync();
    }

    public async Task UpdateMediaPhotoItem(MediaPhotoItem item)
    {
        this.photoItem.State = item;
        await this.photoItem.WriteStateAsync();
    }
}