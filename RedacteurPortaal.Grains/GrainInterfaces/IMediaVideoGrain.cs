using Orleans;
using RedacteurPortaal.DomainModels.Media;

namespace RedacteurPortaal.Grains.GrainInterfaces;

public interface IMediaVideoGrain : IManageableGrain<MediaVideoItem>
{
    Task<MediaVideoItem> Update(MediaVideoItem videoItem);
}