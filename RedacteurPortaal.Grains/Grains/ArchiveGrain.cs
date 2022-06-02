using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.DomainModels.Archive;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.Grains.GrainInterfaces;
using System.Runtime;
using System.Collections.Generic;

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
        var videoItem = videoItems.Single(x => x.Id.Equals(guid));
        
        if (videoItem == null)
        {
            throw new KeyNotFoundException();
        }
        else
        {
            return await Task.FromResult(videoItem);
        }
    }

    public async Task<MediaPhotoItem> GetPhotoItem(Guid guid)
    {
        var photoItems = this.archive.State.MediaPhotoItems;
        var photoItem = photoItems.Find(x => x.Id.Equals(guid));
        if (photoItem == null)
        {
            throw new KeyNotFoundException();
        }
        else
        {
            return await Task.FromResult(photoItem);
        }
    }

    public async Task<MediaAudioItem> GetAudioItem(Guid guid)
    {
        var audioItems = this.archive.State.MediaAudioItems;
        var audioItem = audioItems.Find(x => x.Id.Equals(guid));
        if (audioItem == null)
        {
            throw new KeyNotFoundException();
        }
        else
        {
            return await Task.FromResult(audioItem);
        }
    }

    public async Task<NewsItemModel> GetNewsItem(Guid guid)
    {
        var newsItems = this.archive.State.NewsItems;
        var newsItem = newsItems.Find(x => x.Id.Equals(guid));
        if (newsItem == null)
        {
            throw new KeyNotFoundException();
        }
        else
        {
            return await Task.FromResult(newsItem);
        }
    }

    public async Task<ArchiveModel> CreateArchive(ArchiveModel archive)
    {
        this.archive.State = archive;
        await this.archive.WriteStateAsync();
        return await Task.FromResult(this.archive.State);
    }

    public async Task<MediaVideoItem> AddVideoItem(MediaVideoItem videoItem)
    {
        this.archive.State.MediaVideoItems.Add(videoItem);
        await this.archive.WriteStateAsync();
        return await Task.FromResult(videoItem);
    }

    public async Task<MediaPhotoItem> AddPhotoItem(MediaPhotoItem photoItem)
    {
        this.archive.State.MediaPhotoItems.Add(photoItem);
        await this.archive.WriteStateAsync();
        return await Task.FromResult(photoItem);
    }

    public async Task<MediaAudioItem> AddAudioItem(MediaAudioItem audioItem)
    {
        this.archive.State.MediaAudioItems.Add(audioItem);
        await this.archive.WriteStateAsync();
        return await Task.FromResult(audioItem);
    }

    public async Task<NewsItemModel> AddNewsItem(NewsItemModel newsItem)
    {
        this.archive.State.NewsItems.Add(newsItem);
        await this.archive.WriteStateAsync();
        return await Task.FromResult(newsItem);
    }

    public async Task Delete()
    {
        await this.archive.ClearStateAsync();
    }

    public async Task DeleteVideoItem(Guid videoItemId)
    {
        var videoItems = this.archive.State.MediaVideoItems;
        if (videoItems != null && videoItems.Count > 0)
        {
            var videoItem = videoItems.Find(x => x.Id.Equals(videoItemId));
            if (videoItem == null)
            {
                throw new KeyNotFoundException("Video item with id not found");
            }
            else
            {
                this.archive.State.MediaVideoItems.Remove(videoItem);
                await this.archive.WriteStateAsync();
            }
        }
        else
        {
            throw new KeyNotFoundException("List of video items with given archive id not found");
        }
    }

    public async Task DeletePhotoItem(Guid photoItemId)
    {
        var photoItems = this.archive.State.MediaPhotoItems;
        if (photoItems != null && photoItems.Count > 0)
        {
            var photoItem = photoItems.Find(x => x.Id.Equals(photoItemId));
            if (photoItem == null)
            {
                throw new KeyNotFoundException("Photo item with id not found");
            }
            else
            {
                this.archive.State.MediaPhotoItems.Remove(photoItem);
                await this.archive.WriteStateAsync();
            }
        }
        else
        {
            throw new KeyNotFoundException("List of photo items with given archive id not found");
        }
    }

    public async Task DeleteAudioItem(Guid audioItemId)
    {
        var audioItems = this.archive.State.MediaAudioItems;
        if (audioItems != null && audioItems.Count > 0)
        {
            var audioItem = audioItems.Find(x => x.Id.Equals(audioItemId));
            if (audioItem == null)
            {
                throw new KeyNotFoundException("audio item with id not found");
            }
            else
            {
                this.archive.State.MediaAudioItems.Remove(audioItem);
                await this.archive.WriteStateAsync();
            }
        }
        else
        {
            throw new KeyNotFoundException("List of audio items with given archive id not found");
        }
    }

    public async Task DeleteNewsItem(Guid newsItemId)
    {
        var newsItems = this.archive.State.NewsItems;
        if (newsItems != null && newsItems.Count > 0)
        {
            var newsItem = newsItems.Find(x => x.Id.Equals(newsItemId));
            if (newsItem == null)
            {
                throw new KeyNotFoundException("News item with id not found");
            }
            else
            {
                this.archive.State.NewsItems.Remove(newsItem);
                await this.archive.WriteStateAsync();
            }
        }
        else
        {
            throw new KeyNotFoundException("List of news items with given archive id not found");
        }
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