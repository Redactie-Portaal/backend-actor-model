using Orleans;
using RedacteurPortaal.DomainModels.Media;

namespace RedacteurPortaal.Grains.GrainInterfaces;

public interface IMediaAudioGrain : IGrainWithGuidKey
{
    MediaAudioItem GetMediaAudioItem(Guid guid);

    Task AddMediaAudioItem(MediaAudioItem mediaAudio);

    Task DeleteMediaAudioItem(Guid guid);

    Task UpdateMediaAudioItem(MediaAudioItem mediaAudio);
}