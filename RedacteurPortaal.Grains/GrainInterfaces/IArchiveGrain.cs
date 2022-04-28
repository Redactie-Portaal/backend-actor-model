using Orleans;
using RedacteurPortaal.DomainModels.Archive;
using RedacteurPortaal.DomainModels.Media;

namespace RedacteurPortaal.Grains.GrainInterfaces;

public interface IArchiveGrain : IManageableGrain<ArchiveModel>
{
    Task AddVideoItem(MediaVideoItem videoItem);

    Task AddPhotoItem(MediaPhotoItem photoItem);

    Task AddAudioItem(MediaAudioItem audioItem);

    Task Update(ArchiveModel archive);
}