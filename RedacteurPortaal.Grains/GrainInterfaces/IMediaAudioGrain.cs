using Orleans;
using RedacteurPortaal.DomainModels.Media;

namespace RedacteurPortaal.Grains.GrainInterfaces;

public interface IMediaAudioGrain : IManageableGrain<MediaAudioItem>
{
    Task AddMediaAudioItem(MediaAudioItem item);

    Task Update(MediaAudioItem mediaItem);
}