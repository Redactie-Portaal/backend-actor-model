using Orleans;
using RedacteurPortaal.DomainModels.Media;

namespace RedacteurPortaal.Grains.GrainInterfaces;

public interface IMediaPhotoGrain : IGrainWithGuidKey
{
    Task<MediaPhotoItem> GetMediaPhotoItem(Guid guid);

    Task AddMediaPhotoItem(MediaPhotoItem item);

    Task DeleteMediaPhotoItem(Guid guid);

    Task UpdateMediaPhotoItem(MediaPhotoItem item);
}