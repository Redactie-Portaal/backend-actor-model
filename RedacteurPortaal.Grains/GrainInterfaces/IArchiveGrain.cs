using Orleans;
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

    Task CreateArchive(ArchiveModel archive);

    Task AddVideoItem(MediaVideoItem videoItem);

    Task AddPhotoItem(MediaPhotoItem photoItem);

    Task AddAudioItem(MediaAudioItem audioItem);

    Task AddNewsItem(NewsItemModel newsItem);

    Task DeleteVideoItem(Guid videoItemId);

    Task DeletePhotoItem(Guid photoItemId);

    Task DeleteAudioItem(Guid audioItemId);

    Task DeleteNewsItem(Guid newsItemId);

    Task<ArchiveModel> Update(ArchiveModel archive);
}