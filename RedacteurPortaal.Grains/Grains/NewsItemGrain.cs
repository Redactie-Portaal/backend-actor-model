using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.DomainModels.Shared;
using RedacteurPortaal.Grains.GrainInterfaces;
using RedacteurPortaal.Grains.GrainServices;

namespace RedacteurPortaal.Grains.Grains;

public class NewsItemGrain : Grain, INewsItemGrain
{
    private readonly IGrainManagementService<ILocationGrain> locationGrainService;
    private readonly IGrainManagementService<IContactGrain> contactGrainService;
    private readonly IGrainManagementService<IMediaVideoGrain> videoGrainService;
    private readonly IGrainManagementService<IMediaAudioGrain> audioGrainService;
    private readonly IGrainManagementService<IMediaPhotoGrain> photoGrainService;
    private readonly IPersistentState<NewsItemModel> newsItem;
  
    public Task<bool> HasState()
    {
        return Task.FromResult(this.newsItem.RecordExists);
    }
    
    public NewsItemGrain(
    [PersistentState("newsitem", "OrleansStorage")]
    IPersistentState<NewsItemModel> newsItem,
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
        var location = await this.locationGrainService.GetGrain(this.newsItem.State.Id);
        this.newsItem.State.SetLocationDetails(await location.Get());

        List<Contact> contactList = new List<Contact>();
        List<MediaAudioItem> audioList = new List<MediaAudioItem>();
        List<MediaVideoItem> videoList = new List<MediaVideoItem>();
        List<MediaPhotoItem> photoList = new List<MediaPhotoItem>();

        foreach (var contact in this.newsItem.State.ContactDetails)
        {
            var contactGrain = await this.contactGrainService.GetGrain(contact.Id);
            contactList.Add(await contactGrain.Get());
        }
        
        foreach (var video in this.newsItem.State.Videos)
        {
            var videoGrain = await this.videoGrainService.GetGrain(video.Id);
            videoList.Add(await videoGrain.Get());
        }
        
        foreach (var audio in this.newsItem.State.Audio)
        {
            var audioGrain = await this.audioGrainService.GetGrain(audio.Id);
            audioList.Add(await audioGrain.Get());
        }
        
        foreach (var photo in this.newsItem.State.Photos)
        {
            var photoGrain = await this.photoGrainService.GetGrain(photo.Id);
            photoList.Add(await photoGrain.Get());
        }

        this.newsItem.State.SetContactDetails(contactList);
        this.newsItem.State.SetVideos(videoList);
        this.newsItem.State.SetAudio(audioList);
        this.newsItem.State.SetPhotos(photoList);

        return await Task.FromResult(this.newsItem.State);
    }

    public async Task Delete()
    {
        foreach (var contact in this.newsItem.State.ContactDetails)
        {
            var contactGrain = await this.contactGrainService.GetGrain(contact.Id);
            await contactGrain.Delete();
        }

        foreach (var video in this.newsItem.State.Videos)
        {
            var videoGrain = await this.videoGrainService.GetGrain(video.Id);
            await videoGrain.Delete();
        }

        foreach (var audio in this.newsItem.State.Audio)
        {
            var audioGrain = await this.audioGrainService.GetGrain(audio.Id);
            await audioGrain.Delete();
        }

        foreach (var photo in this.newsItem.State.Photos)
        {
            var photoGrain = await this.photoGrainService.GetGrain(photo.Id);
            await photoGrain.Delete();
        }
        
        await this.newsItem.ClearStateAsync();
    }

    public async Task<NewsItemModel> Update(NewsItemModel newsItem)
    {
        this.newsItem.State = newsItem;
        List<Contact> contactList = new List<Contact>();
        List<MediaAudioItem> audioList = new List<MediaAudioItem>();
        List<MediaVideoItem> videoList = new List<MediaVideoItem>();
        List<MediaPhotoItem> photoList = new List<MediaPhotoItem>();
        
        foreach (var contact in this.newsItem.State.ContactDetails)
        {
            var result = this.contactGrainService.GrainExists(contact.Id);
            if(result)
            {
                var contactGrain = await this.contactGrainService.GetGrain(contact.Id);
                await contactGrain.Update(contact);
            }
            else
            {
                var newGrain = await this.contactGrainService.CreateGrain(Guid.NewGuid());
                var updatedGrain = await newGrain.Update(contact);
                contactList.Add(updatedGrain);
            }
        }
        
        foreach (var video in this.newsItem.State.Videos)
        {
            var result = this.videoGrainService.GrainExists(video.Id);
            if (result)
            {
                var videoGrain = await this.videoGrainService.GetGrain(video.Id);
                await videoGrain.Update(video);
            }
            else
            {
                var newGrain = await this.videoGrainService.CreateGrain(Guid.NewGuid());
                var updatedGrain = await newGrain.Update(video);
                videoList.Add(updatedGrain);
            }
        }
        
        foreach (var audio in this.newsItem.State.Audio)
        {
            var result = this.audioGrainService.GrainExists(audio.Id);
            if (result)
            {
                var audioGrain = await this.audioGrainService.GetGrain(audio.Id);
                await audioGrain.Update(audio);
            }
            else
            {
                var newGrain = await this.audioGrainService.CreateGrain(Guid.NewGuid());
                var updatedGrain = await newGrain.Update(audio);
                audioList.Add(updatedGrain);
            }
        }
        
        foreach (var photo in this.newsItem.State.Photos)
        {
            var result = this.photoGrainService.GrainExists(photo.Id);
            if (result)
            {
                var photoGrain = await this.photoGrainService.GetGrain(photo.Id);
                await photoGrain.Update(photo);
            }
            else
            {
                var newGrain = await this.photoGrainService.CreateGrain(Guid.NewGuid());
                var updatedGrain = await newGrain.Update(photo);
                photoList.Add(updatedGrain);
            }
        }
        
        this.newsItem.State.SetContactDetails(contactList);
        this.newsItem.State.SetVideos(videoList);
        this.newsItem.State.SetAudio(audioList);
        this.newsItem.State.SetPhotos(photoList);
        
        await this.newsItem.WriteStateAsync();
        return this.newsItem.State;
    }
}