using Orleans;
using RedacteurPortaal.DomainModels.Media;

namespace RedacteurPortaal.Grains.GrainInterfaces;

public interface IMediaPhotoGrain : IGrainWithGuidKey
{
    MediaPhotoItem GetMediaPhotoItem(Guid guid);

    Task AddMediaPhotoItem(MediaPhotoItem mediaPhoto);

    Task DeleteMediaPhotoItem(Guid guid);

    Task UpdateMediaPhotoItem(MediaPhotoItem mediaPhoto);
}