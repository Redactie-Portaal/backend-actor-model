using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.DomainModels.Archive;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.Grains.GrainInterfaces;
using System.Runtime;
using System.Collections.Generic;
using RedacteurPortaal.Grains.GrainServices;
using RedacteurPortaal.Helpers;

namespace RedacteurPortaal.Grains.Grains;

public class ArchiveGrain : Grain, IArchiveGrain
{
    private readonly IGrainManagementService<IMediaVideoGrain> videoGrainService;
    private readonly IGrainManagementService<IMediaAudioGrain> audioGrainService;
    private readonly IGrainManagementService<IMediaPhotoGrain> photoGrainService;
    private readonly IGrainManagementService<INewsItemGrain> newsItemGrainService;
    private readonly IPersistentState<ArchiveModel> archive;

    public ArchiveGrain(
    [PersistentState("archive", "OrleansStorage")]
    IPersistentState<ArchiveModel> archive,
    IGrainManagementService<IMediaVideoGrain> videoGrainService,
    IGrainManagementService<IMediaAudioGrain> audioGrainService,
    IGrainManagementService<IMediaPhotoGrain> photoGrainService,
    IGrainManagementService<INewsItemGrain> newsItemGrainService)
    {
        this.archive = archive;
        this.videoGrainService = videoGrainService;
        this.audioGrainService = audioGrainService;
        this.photoGrainService = photoGrainService;
        this.newsItemGrainService = newsItemGrainService;
    }

    public async Task<ArchiveModel> Get()
    {
        return await Task.FromResult(this.archive.State);
    }

    public async Task<MediaVideoItem> GetVideoItem(Guid guid)
    {
        var grain = await this.videoGrainService.GetGrain(guid);
        var item = await grain.Get();

        if (item == null)
        {
            throw new KeyNotFoundException("VideoItem with ID ${guid} not found!");
        }
        else
        {
            return await Task.FromResult(item);
        }
    }

    public async Task<MediaPhotoItem> GetPhotoItem(Guid guid)
    {
        var grain = await this.photoGrainService.GetGrain(guid);
        var item = await grain.Get();
        
        if (item == null)
        {
            throw new KeyNotFoundException("PhotoItem with ID ${guid} not found!");
        }
        else
        {
            return await Task.FromResult(item);
        }
    }

    public async Task<MediaAudioItem> GetAudioItem(Guid guid)
    {
        var grain = await this.audioGrainService.GetGrain(guid);
        var item = await grain.Get();
        if (item == null)
        {
            throw new KeyNotFoundException("AudioItem with ID ${guid} not found!");
        }
        else
        {
            return await Task.FromResult(item);
        }
    }

    public async Task<NewsItemModel> GetNewsItem(Guid guid)
    {
        var grain = await this.newsItemGrainService.GetGrain(guid);
        var item = await grain.Get();
        if (item == null)
        {
            throw new KeyNotFoundException("NewsItem with ID ${guid} not found!");
        }
        else
        {
            return await Task.FromResult(item);
        }
    }

    public async Task<ArchiveModel> CreateArchive(ArchiveModel archive)
    {
        this.archive.State = archive;
        await this.archive.WriteStateAsync();
        return await Task.FromResult(this.archive.State);
    }

    public async Task<Guid> AddVideoItem(Guid videoItemId)
    {
        if (videoItemId != Guid.Empty)
        {
            this.archive.State.MediaVideoItems.Add(videoItemId);
            await this.archive.WriteStateAsync();
            return await Task.FromResult(videoItemId);
        }
        else
        {
            throw new KeyNotFoundException("VideoItem with ID ${videoItemId} not found!");
        }
    }

    public async Task<Guid> AddPhotoItem(Guid photoItemId)
    {
        if (photoItemId != Guid.Empty)
        {
            this.archive.State.MediaPhotoItems.Add(photoItemId);
            await this.archive.WriteStateAsync();
            return await Task.FromResult(photoItemId);
        }
        else
        {
            throw new KeyNotFoundException("PhotoItem with ID ${photoItemId} not found!");
        }
    }

    public async Task<Guid> AddAudioItem(Guid audioItemId)
    {
        if (audioItemId != Guid.Empty)
        {
            this.archive.State.MediaAudioItems.Add(audioItemId);
            await this.archive.WriteStateAsync();
            return await Task.FromResult(audioItemId);
        }
        else
        {
            throw new KeyNotFoundException("AudioItem with ID ${audioItemId} not found!");
        }
    }

    public async Task<Guid> AddNewsItem(Guid newsItemId)
    {
        if (newsItemId != Guid.Empty)
        {
            this.archive.State.NewsItems.Add(newsItemId);
            await this.archive.WriteStateAsync();
            return await Task.FromResult(newsItemId);
        }
        else
        {
            throw new KeyNotFoundException("NewsItem with ID ${newsItemId} not found!");
        }
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
            var videoItem = videoItems.Find(x => x.Equals(videoItemId));
            if (videoItem == Guid.Empty)
            {
                throw new KeyNotFoundException("VideoItem with ID ${videoItemId} not found!");
            }
            else
            {
                videoItems.Remove(videoItemId);
                await this.archive.WriteStateAsync();
            }
        }
        else
        {
            throw new KeyNotFoundException("No VideoItems found in Archive ${this.archive.State.Id}!");
        }
    }

    public async Task DeletePhotoItem(Guid photoItemId)
    {
        var photoItems = this.archive.State.MediaPhotoItems;
        if (photoItems != null && photoItems.Count > 0)
        {
            var photoItem = photoItems.Find(x => x.Equals(photoItemId));
            if (photoItem == Guid.Empty)
            {
                throw new KeyNotFoundException("PhotoItem with ID ${videoItemId} not found!");
            }
            else
            {
                photoItems.Remove(photoItemId);
                await this.archive.WriteStateAsync();
            }
        }
        else
        {
            throw new KeyNotFoundException("No PhotoItems found in Archive ${this.archive.State.Id}!");
        }
    }

    public async Task DeleteAudioItem(Guid audioItemId)
    {
        var audioItems = this.archive.State.MediaAudioItems;
        if (audioItems != null && audioItems.Count > 0)
        {
            var audioItem = audioItems.Find(x => x.Equals(audioItemId));
            if (audioItem == Guid.Empty)
            {
                throw new KeyNotFoundException("AudioItem with ID ${videoItemId} not found!");
            }
            else
            {
                audioItems.Remove(audioItemId);
                await this.archive.WriteStateAsync();
            }
        }
        else
        {
            throw new KeyNotFoundException("No AudioItems found in Archive ${this.archive.State.Id}!");
        }
    }

    public async Task DeleteNewsItem(Guid newsItemId)
    {
        var newsItems = this.archive.State.NewsItems;
        if (newsItems != null && newsItems.Count > 0)
        {
            var newsItem = newsItems.Find(x => x.Equals(newsItemId));
            if (newsItem == Guid.Empty)
            {
                throw new KeyNotFoundException("NewsItem with ID ${videoItemId} not found!");
            }
            else
            {
                newsItems.Remove(newsItemId);
                await this.archive.WriteStateAsync();
            }
        }
        else
        {
            throw new KeyNotFoundException("No NewsItems found in Archive ${this.archive.State.Id}!");
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