using Orleans;
using RedacteurPortaal.DomainModels.Archive;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.DomainModels.NewsItem;

namespace RedacteurPortaal.Grains.GrainInterfaces;

public interface IArchiveGrain : IManageableGrain<ArchiveModel>
{
<<<<<<< Updated upstream
    Task AddVideoItem(MediaVideoItem videoItem);

    Task AddPhotoItem(MediaPhotoItem photoItem);

    Task AddAudioItem(MediaAudioItem audioItem);    
=======
    Task<ArchiveModel> GetArchive(Guid guid);

    Task<List<MediaVideoItem>> GetAllVideoItems();

    Task<List<MediaPhotoItem>> GetAllPhotoItems();

    Task<List<MediaAudioItem>> GetAllAudioItems();

    Task<List<NewsItemModel>> GetAllNewsItems();

    Task<MediaVideoItem> GetVideoItem(Guid guid);

    Task<MediaPhotoItem> GetPhotoItem(Guid guid);

    Task<MediaAudioItem> GetAudioItem(Guid guid);

    Task<NewsItemModel> GetNewsItem(Guid guid);

    Task CreateArchive(ArchiveModel archive);

    Task AddVideoItem(MediaVideoItem videoItem);

    Task AddPhotoItem(MediaPhotoItem photoItem);

    Task AddAudioItem(MediaAudioItem audioItem);

    Task AddNewsItem(NewsItemModel newsItem);

    Task DeleteArchive(Guid guid);

    Task DeleteVideoItem(Guid videoItemId);

    Task DeletePhotoItem(Guid photoItemId);

    Task DeleteAudioItem(Guid audioItemId);

    Task DeleteNewsItem(Guid newsItemId);

    Task Update(ArchiveModel archive);
>>>>>>> Stashed changes
}