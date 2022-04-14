using Orleans;
using RedacteurPortaal.DomainModels.Media;

namespace RedacteurPortaal.Grains.GrainInterfaces;

public interface IMediaVideoGrain : IManageableGrain<MediaVideoItem>
{
    Task Update(MediaVideoItem videoItem);
}