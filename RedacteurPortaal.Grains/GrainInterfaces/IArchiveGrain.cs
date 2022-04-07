using RedacteurPortaal.DomainModels.Archive;
using RedacteurPortaal.DomainModels.Media;

namespace RedacteurPortaal.Grains.GrainInterfaces;

    public interface IArchiveGrain : IGrainWithGuidKey
{
    Task RemoveArchive();

    Task AddVideoItem(MediaVideoItem item);

    Task AddPhotoItem(MediaPhotoItem item);

    Task AddAudioItem(MediaAudioItem item);

    Task<ArchiveModel> GetArchive(Guid guid);

}

