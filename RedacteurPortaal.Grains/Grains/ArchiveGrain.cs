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

    public async Task<ArchiveModel> Update(ArchiveModel archive)
    {
        this.archive.State = archive;
        await this.archive.WriteStateAsync();

        return await Task.FromResult(this.archive.State);
    }

    public Task<bool> HasState()
    {
        return Task.FromResult(this.archive.RecordExists);
    }
}