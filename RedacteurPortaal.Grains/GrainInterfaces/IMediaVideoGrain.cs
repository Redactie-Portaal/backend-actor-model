using Orleans;
using RedacteurPortaal.DomainModels.Media;

namespace RedacteurPortaal.Grains.GrainInterfaces;

public interface IMediaVideoGrain : IGrainWithGuidKey
{
    Task<MediaVideoItem> GetMediaVideoItem(Guid guid);

    Task AddMediaVideoItem(MediaVideoItem videoItem);

    Task DeleteMediaVideoItem(Guid guid);

    Task UpdateMediaVideoItem(MediaVideoItem videoItem);
}