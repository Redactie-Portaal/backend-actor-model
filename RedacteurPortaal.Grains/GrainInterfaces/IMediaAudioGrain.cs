using Orleans;
using RedacteurPortaal.DomainModels.Media;

namespace RedacteurPortaal.Grains.GrainInterfaces;

public interface IMediaAudioGrain : IGrainWithGuidKey
{
    Task<MediaAudioItem> GetMediaAudioItem();

    Task DeleteMediaAudioItem();

    Task UpdateMediaAudioItem(MediaAudioItem mediaAudio);
}