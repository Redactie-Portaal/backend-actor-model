using Orleans;
using RedacteurPortaal.DomainModels.Archive;
using RedacteurPortaal.DomainModels.Media;

namespace RedacteurPortaal.Grains.GrainInterfaces;

public interface IArchiveGrain : IGrainWithGuidKey
{
    Task RemoveArchive();

    Task AddVideoItem(MediaVideoItem videoItem);

    Task AddPhotoItem(MediaPhotoItem photoItem);

    Task AddAudioItem(MediaAudioItem audioItem);

    Task<ArchiveModel> GetArchive(Guid guid);
}