using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.DomainModels.Archive;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.Grains.GrainInterfaces;

namespace RedacteurPortaal.Grains.Grains;

public class ArchiveGrain : Grain, IArchiveGrain
{
    private readonly IPersistentState<ArchiveModel> archive;

    public ArchiveGrain(
    [PersistentState("archive", "OrleansStorage")]
    IPersistentState<ArchiveModel> archive)
    {
        this.archive = archive;
    }

    public Task<ArchiveModel> Get()
    {
        var state = this.archive.State;
        return Task.FromResult(state);
    }

    public async Task<ArchiveModel> GetArchive(Guid guid)
    {
        var archive = this.GrainFactory.GetGrain<IArchiveGrain>(guid);
        return (ArchiveModel)await Task.FromResult(archive);
    }

    public async Task<List<MediaVideoItem>> GetAllVideoItems()
    {
        return await Task.FromResult(this.archive.State.MediaVideoItems);
    }

    public async Task<List<MediaPhotoItem>> GetAllPhotoItems()
    {
        return await Task.FromResult(this.archive.State.MediaPhotoItems);
    }

    public async Task<List<MediaAudioItem>> GetAllAudioItems()
    {
        return await Task.FromResult(this.archive.State.MediaAudioItems);
    }

    public async Task<List<NewsItemModel>> GetAllNewsItems()
    {
        return await Task.FromResult(this.archive.State.NewsItems);
    }

    public async Task<MediaVideoItem> GetVideoItem(Guid guid)
    {
        var videoItems = this.archive.State.MediaVideoItems;
        var videoItem = videoItems.Find(x => x.Id.Equals(guid));
        return await Task.FromResult(videoItem);
    }

    public async Task<MediaPhotoItem> GetPhotoItem(Guid guid)
    {
        var photoItems = this.archive.State.MediaPhotoItems;
        var photoItem = photoItems.Find(x => x.Id.Equals(guid));
        return await Task.FromResult(photoItem);
    }

    public async Task<MediaAudioItem> GetAudioItem(Guid guid)
    {
        var audioItems = this.archive.State.MediaAudioItems;
        var audioItem = audioItems.Find(x => x.Id.Equals(guid));
        return await Task.FromResult(audioItem);
    }

    public async Task<NewsItemModel> GetNewsItem(Guid guid)
    {
        var newsItems = this.archive.State.NewsItems;
        var newsItem = newsItems.Find(x => x.Id.Equals(guid));
        return await Task.FromResult(newsItem);
    }

    public async Task CreateArchive(ArchiveModel archive)
    {
        this.archive.State = archive;
        await this.archive.WriteStateAsync();
    }

    public async Task AddVideoItem(MediaVideoItem videoItem)
    {
        this.archive.State.MediaVideoItems.Add(videoItem);
        await this.archive.WriteStateAsync();
    }

    public async Task AddPhotoItem(MediaPhotoItem photoItem)
    {
        this.archive.State.MediaPhotoItems.Add(photoItem);
        await this.archive.WriteStateAsync();
    }

    public async Task AddAudioItem(MediaAudioItem audioItem)
    {
        this.archive.State.MediaAudioItems.Add(audioItem);
        await this.archive.WriteStateAsync();
    }

    public async Task AddNewsItem(NewsItemModel newsItem)
    {
        this.archive.State.NewsItems.Add(newsItem);
        await this.archive.WriteStateAsync();
    }
    
    public async Task Delete()
    {
        await this.archive.ClearStateAsync();
    }

    public async Task DeleteVideoItem(Guid videoItemId)
    {
        var videoItems = this.archive.State.MediaVideoItems;
        var videoItem = videoItems.Find(x => x.Id.Equals(videoItemId));
        this.archive.State.MediaVideoItems.Remove(videoItem);
        await this.archive.WriteStateAsync();
    }

    public async Task DeletePhotoItem(Guid photoItemId)
    {
        var photoItems = this.archive.State.MediaPhotoItems;
        var photoItem = photoItems.Find(x => x.Id.Equals(photoItemId));
        this.archive.State.MediaPhotoItems.Remove(photoItem);
        await this.archive.WriteStateAsync();
    }

    public async Task DeleteAudioItem(Guid audioItemId)
    {
        var audioItems = this.archive.State.MediaAudioItems;
        var audioItem = audioItems.Find(x => x.Id.Equals(audioItemId));
        this.archive.State.MediaAudioItems.Remove(audioItem);
        await this.archive.WriteStateAsync();
    }

    public async Task DeleteNewsItem(Guid newsItemId)
    {
        var newsItems = this.archive.State.MediaAudioItems;
        var newsItem = newsItems.Find(x => x.Id.Equals(newsItemId));
        this.archive.State.MediaAudioItems.Remove(newsItem);
        await this.archive.WriteStateAsync();
    }

    public async Task<ArchiveModel> Update(ArchiveUpdate archive)
    {
        this.archive.State.Title = archive.Title;
        this.archive.State.Label = archive.Label;
        this.archive.State.MediaPhotoItems = archive.MediaPhotoItems;
        this.archive.State.MediaVideoItems = archive.MediaVideoItems;
        this.archive.State.MediaAudioItems = archive.MediaAudioItems;
        this.archive.State.NewsItems = archive.NewsItems;
        this.archive.State.Scripts = archive.Scripts;
        await this.archive.WriteStateAsync();

        return await Task.FromResult(this.archive.State);
    }

    public Task<bool> HasState()
    {
        return Task.FromResult(this.archive.RecordExists);
    }
}