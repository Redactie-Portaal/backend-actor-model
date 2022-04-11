using Orleans;
using RedacteurPortaal.DomainModels.Media;

namespace RedacteurPortaal.Grains.GrainInterfaces;

public interface IMediaVideoGrain : IGrainWithGuidKey
{
    Task<MediaVideoItem> GetMediaVideoItem();
    
    Task DeleteMediaVideoItem();

    Task UpdateMediaVideoItem(MediaVideoItem videoItem);
}