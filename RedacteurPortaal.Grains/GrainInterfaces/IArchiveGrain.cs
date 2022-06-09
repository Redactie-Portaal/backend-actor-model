using RedacteurPortaal.DomainModels.Archive;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.DomainModels.NewsItem;

namespace RedacteurPortaal.Grains.GrainInterfaces;

public interface IArchiveGrain : IManageableGrain<ArchiveModel>
{ 
    Task<MediaVideoItem> GetVideoItem(Guid guid);

    Task<MediaPhotoItem> GetPhotoItem(Guid guid);

    Task<MediaAudioItem> GetAudioItem(Guid guid);

    Task<NewsItemModel> GetNewsItem(Guid guid);

    Task<ArchiveModel> CreateArchive(ArchiveModel archive);

    Task<Guid> AddVideoItem(Guid videoItemId);

    Task<Guid> AddPhotoItem(Guid photoItemId);

    Task<Guid> AddAudioItem(Guid audioItemId);

    Task<Guid> AddNewsItem(Guid newsItemId);

    Task DeleteVideoItem(Guid videoItemId);

    Task DeletePhotoItem(Guid photoItemId);

    Task DeleteAudioItem(Guid audioItemId);

    Task DeleteNewsItem(Guid newsItemId);

    Task<ArchiveModel> Update(ArchiveModel archive);
}