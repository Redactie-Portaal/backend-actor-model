using Orleans;
using RedacteurPortaal.DomainModels.Media;

namespace RedacteurPortaal.Grains.GrainInterfaces;

public interface IMediaVideoGrain : IManageableGrain<MediaVideoItem>
{
    Task AddMediaVideoItem(MediaVideoItem item);

    Task Update(MediaVideoItem videoItem);
}