using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.DomainModels.Shared;
using RedacteurPortaal.Grains.GrainInterfaces;
using RedacteurPortaal.Grains.GrainServices;
using RedacteurPortaal.Grains.GrainState;
using RedacteurPortaal.Helpers;

namespace RedacteurPortaal.Grains.Grains;

public class NewsItemGrain : Grain, INewsItemGrain
{
    private readonly IGrainManagementService<ILocationGrain> locationGrainService;
    private readonly IGrainManagementService<IContactGrain> contactGrainService;
    private readonly IGrainManagementService<IMediaVideoGrain> videoGrainService;
    private readonly IGrainManagementService<IMediaAudioGrain> audioGrainService;
    private readonly IGrainManagementService<IMediaPhotoGrain> photoGrainService;
    private readonly IPersistentState<NewsItemGrainState> newsItem;

    public Task<bool> HasState()
    {
        return Task.FromResult(this.newsItem.RecordExists);
    }

    public NewsItemGrain(
    [PersistentState("newsitem", "OrleansStorage")]
    IPersistentState<NewsItemGrainState> newsItem,
    IGrainManagementService<ILocationGrain> locationGrainService,
    IGrainManagementService<IContactGrain> contactGrainService,
    IGrainManagementService<IMediaVideoGrain> videoGrainService,
    IGrainManagementService<IMediaAudioGrain> audioGrainService,
    IGrainManagementService<IMediaPhotoGrain> photoGrainService)
    {
        this.newsItem = newsItem;
        this.contactGrainService = contactGrainService;
        this.locationGrainService = locationGrainService;
        this.videoGrainService = videoGrainService;
        this.audioGrainService = audioGrainService;
        this.photoGrainService = photoGrainService;
    }

    public async Task<NewsItemModel> Get()
    {
        var locationGrain = await this.locationGrainService.GetGrainOrCreate(this.GetPrimaryKey());

        var contactDetails = await this.newsItem.State.ContactDetails
            .Where(x => x != Guid.Empty)
            .SelectAsync(async x => {
                var contactGrain = await this.contactGrainService.GetGrain(x);
                return await contactGrain.Get();
            });

        var videos = await this.newsItem.State.Videos
            .Where(x => x != Guid.Empty)
            .SelectAsync(async x => {
                var videoGrain = await this.videoGrainService.GetGrain(x);
                return await videoGrain.Get();
            });

        var audio = await this.newsItem.State.Audio
            .Where(x => x != Guid.Empty)
            .SelectAsync(async x => {
                var grain = await this.audioGrainService.GetGrain(x);
                return await grain.Get();
            });

        var photos = await this.newsItem.State.Photos
            .Where(x => x != Guid.Empty)
            .SelectAsync(async x => {
                var grain = await this.photoGrainService.GetGrain(x);
                return await grain.Get();
            });

        await this.newsItem.WriteStateAsync();

        return await Task.FromResult(
            new NewsItemModel(
                this.GetPrimaryKey(),
                this.newsItem.State.Title,
                this.newsItem.State.Status,
                this.newsItem.State.ApprovalState,
                this.newsItem.State.Author,
                this.newsItem.State.Source,
                this.newsItem.State.Body,
                contactDetails.ToList(),
                //await locationGrain.Get(),
                this.newsItem.State.LocationDetails,
                this.newsItem.State.ProductionDate,
                this.newsItem.State.EndDate,
                this.newsItem.State.Category,
                this.newsItem.State.Region,
                videos.ToList(),
                audio.ToList(),
                photos.ToList())
            );
    }

    public async Task Delete()
    {
        foreach (var contact in this.newsItem.State.ContactDetails)
        {
            var contactGrain = await this.contactGrainService.GetGrain(contact);
            await contactGrain.Delete();
        }

        foreach (var video in this.newsItem.State.Videos)
        {
            var videoGrain = await this.videoGrainService.GetGrain(video);
            await videoGrain.Delete();
        }

        foreach (var audio in this.newsItem.State.Audio)
        {
            var audioGrain = await this.audioGrainService.GetGrain(audio);
            await audioGrain.Delete();
        }

        foreach (var photo in this.newsItem.State.Photos)
        {
            var photoGrain = await this.photoGrainService.GetGrain(photo);
            await photoGrain.Delete();
        }

        await this.newsItem.ClearStateAsync();
    }

    public async Task<NewsItemModel> Update(NewsItemModel newsItem)
    {
        this.newsItem.State.Title = newsItem.Title;
        this.newsItem.State.Status = newsItem.Status;
        this.newsItem.State.ApprovalState = newsItem.ApprovalState;
        this.newsItem.State.Author = newsItem.Author;
        this.newsItem.State.Source = newsItem.Source;
        this.newsItem.State.Body = newsItem.Body;

        foreach (var contact in newsItem.ContactDetails)
        {
            if (contact.Id == Guid.Empty && contact is not null)
            {
                contact.Id = Guid.NewGuid();
            }

            var contactGrain = await this.contactGrainService.GetGrainOrCreate(contact.Id);
            await contactGrain.Update(contact);
        }

        this.newsItem.State.ContactDetails = newsItem.ContactDetails.Select(x => x.Id).ToList();
        if (newsItem.LocationDetails.Id == Guid.Empty)
        {
            newsItem.LocationDetails.Id = Guid.NewGuid();
        }
        this.newsItem.State.LocationDetails = newsItem.LocationDetails;
        this.newsItem.State.ProductionDate = newsItem.ProductionDate;
        this.newsItem.State.EndDate = newsItem.EndDate;
        this.newsItem.State.Category = newsItem.Category;
        this.newsItem.State.Region = newsItem.Region;

        foreach (var video in newsItem.Videos)
        {
            if(video is not null)
            {
                if (video.Id == Guid.Empty)
                {
                    video.Id = Guid.NewGuid();
                }
                
                var videoGrain = await this.videoGrainService.CreateGrain(video.Id);
                await videoGrain.Update(video);
            }
        }

        this.newsItem.State.Videos = newsItem.Videos.DiscardNullValues().Select(x => x.Id).ToList();

        foreach (var audio in newsItem.Audio)
        {
            if(audio is not null)
            {
                if (audio.Id == Guid.Empty)
                {
                    audio.Id = Guid.NewGuid();
                }

                var audioGrain = await this.audioGrainService.CreateGrain(audio.Id);
                await audioGrain.Update(audio);
            }

        }

        this.newsItem.State.Audio = newsItem.Audio.DiscardNullValues().Select(x => x.Id).ToList();

        foreach (var photo in newsItem.Photos)
        {
            if (photo is not null)
            {
                if (photo.Id == Guid.Empty)
                {
                    photo.Id = Guid.NewGuid();
                }

                var photoGrain = await this.photoGrainService.CreateGrain(photo.Id);
                await photoGrain.Update(photo);
            }
        }

        this.newsItem.State.Photos = newsItem.Photos.DiscardNullValues().Select(x => x.Id).ToList();

        await this.newsItem.WriteStateAsync();
        return await this.Get();
    }
}