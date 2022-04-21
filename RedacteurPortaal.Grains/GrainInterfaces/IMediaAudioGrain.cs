using Orleans;
using RedacteurPortaal.DomainModels.Media;

namespace RedacteurPortaal.Grains.GrainInterfaces;

public interface IMediaAudioGrain : IManageableGrain<MediaAudioItem>
{ 
    Task Update(MediaAudioItem mediaItem);
}