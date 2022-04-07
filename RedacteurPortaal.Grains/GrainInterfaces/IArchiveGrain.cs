using Orleans;
using RedacteurPortaal.DomainModels.Archive;
using RedacteurPortaal.DomainModels.Media;

namespace RedacteurPortaal.Grains.GrainInterfaces;

public interface IArchiveGrain : IGrainWithGuidKey
{
    Task DeleteArchive(Guid guid);

    Task AddVideoItem(ArchiveModel archive, MediaVideoItem videoItem);

    Task AddPhotoItem(ArchiveModel archive, MediaPhotoItem photoItem);

    Task AddAudioItem(ArchiveModel archive, MediaAudioItem audioItem);

    Task<ArchiveModel> GetArchive(Guid guid);
}