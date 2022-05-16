using Microsoft.AspNetCore.Mvc;
using Orleans;
using RedacteurPortaal.DomainModels.Archive;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.Api.DTOs;
using RedacteurPortaal.Api.Models;
using RedacteurPortaal.Grains.GrainInterfaces;
using RedacteurPortaal.Grains.GrainServices;
using RedacteurPortaal.Api.Models.Request;
using Mapster;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.Helpers;

namespace RedacteurPortaal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArchiveController : Controller
{
    private readonly IGrainManagementService<IArchiveGrain> grainService;

    public ArchiveController(IGrainManagementService<IArchiveGrain> grainService)
    {
        this.grainService = grainService;
    }

    [HttpGet]
    public async Task<ActionResult<ArchiveModel>> GetAllArchives()
    {
        TypeAdapterConfig<ArchiveModel, ArchiveDto>
     .NewConfig()
     .Map(dest => dest.MediaAudioItems,
         src => src.MediaAudioItems.Select(x => new MediaAudioItemDto {
             DurationSeconds = Convert.ToInt32(x.Duration.TotalSeconds),
             FirstWords = x.FirstWords,
             ProgramName = x.ProgramName,
             Weather = x.Weather,
         }).ToList()
         ).Map(dest => dest.MediaPhotoItems, src => src.MediaPhotoItems.Select(x => new MediaPhotoItemDto {
             Camera = x.Camera,
             Folder = x.Folder,
             Image = x.Image,
             Format = x.Format,
             Id = x.Id,
             LastWords = x.LastWords,
             Location = new LocationDto { City = x.Location.City, Latitude = x.Location.Latitude, Longitude = x.Location.Longitude, Street = x.Location.Street },
             Presentation = x.Presentation,
             ProxyFile = x.ProxyFile,
             RepublishDate = x.RepublishDate,
             Title = x.Title
         }).ToList()
         ).Map(dest => dest.MediaVideoItems, src => src.MediaVideoItems.Select(x => new MediaVideoItemDto {
             Camera = x.Camera,
             ProgramDate = x.ProgramDate,
             ProgramName = x.ProgramName,
             ProxyFile = x.ProxyFile,
             RepublishDate = x.RepublishDate,
             Title = x.Title,
             Weather = x.Weather
         }).ToList()).Map(dest => dest.NewsItems, src => src.NewsItems.Select(x => new NewsItemDto {
             Id = x.Id,
             ApprovalStatus = x.ApprovalState.ToString(),
             Author = x.Author,
             Category = x.Category,
             Audio = new(),
             Photos = new(),
             Videos = new(),
             Title = x.Title,
             Body = x.Body,
             ContactDetails = new List<ContactDto>(),
             EndDate = x.EndDate,
             LocationDetails = new LocationDto { City = x.LocationDetails.City, Latitude = x.LocationDetails.Latitude, Longitude = x.LocationDetails.Longitude, Street = x.LocationDetails.Street },
             ProdutionDate = new DateTime(),
             Region = x.Region,
             Source = new FeedSourceDto { PlaceHolder = x.Source.PlaceHolder  },
             Status = x.Status.ToString()
         }).ToList());

        var grain = await this.grainService.GetGrains();

        var response = (await grain.SelectAsync(async x => await
        x.Get())).AsQueryable().ProjectToType<ArchiveDto>(null).ToList();
        
        return this.Ok(response);
    }

    [HttpGet]
    [Route("{archiveId}/", Name = nameof(GetArchiveById))]
    public async Task<ActionResult<ArchiveModel>> GetArchiveById(Guid archiveId)
    {
        var archive = await this.grainService.GetGrain(archiveId);
        var response = await archive.Get();
        var dto = response.Adapt<ArchiveModel>();
        return this.Ok(dto);
    }

    [HttpGet]
    [Route("{archiveId}/VideoItems")]
    public async Task<ActionResult<List<MediaVideoItemDto>>> GetVideoItems([FromRoute] Guid archiveId)
    {
        var archive = await this.grainService.GetGrain(archiveId);
        var response = await archive.Get();
        var dto = response.Adapt<ArchiveModel>();
        return this.Ok(dto.MediaVideoItems);
    }

    [HttpGet]
    [Route("{archiveId}/AudioItems")]
    public async Task<ActionResult<List<MediaAudioItem>>> GetAudioItems([FromRoute] Guid archiveId)
    {
        var archive = await this.grainService.GetGrain(archiveId);
        var response = await archive.Get();
        var dto = response.Adapt<ArchiveModel>();
        return this.Ok(dto.MediaAudioItems);
    }

    [HttpGet]
    [Route("{archiveId}/PhotoItems")]
    public async Task<ActionResult<List<MediaPhotoItemDto>>> GetPhotoItems([FromRoute] Guid archiveId)
    {
        var archive = await this.grainService.GetGrain(archiveId);
        var response = await archive.Get();
        var dto = response.Adapt<ArchiveModel>();
        return this.Ok(dto.MediaPhotoItems);
    }

    [HttpGet]
    [Route("{archiveId}/Stories")]
    public async Task<ActionResult<List<NewsItemDto>>> GetNewsItems([FromRoute] Guid archiveId)
    {
        var archive = await this.grainService.GetGrain(archiveId);
        var response = await archive.Get();
        var dto = response.Adapt<ArchiveModel>();
        return this.Ok(dto.NewsItems);
    }

    [HttpGet]
    [Route("{archiveId}/VideoItems/{videoItemGuid}")]
    public async Task<IActionResult> GetVideoItem([FromRoute] Guid archiveId, [FromRoute] Guid videoItemGuid)
    {
        var grain = await this.grainService.GetGrain(archiveId);
        var response = await grain.GetVideoItem(videoItemGuid);
        var dto = response.Adapt<MediaVideoItem>();
        return this.Ok(dto);
    }

    [HttpGet]
    [Route("{archiveId}/AudioItems/{audioItemGuid}")]
    public async Task<IActionResult> GetAudioItem([FromRoute] Guid archiveId, [FromRoute] Guid audioItemGuid)
    {
        var grain = await this.grainService.GetGrain(archiveId);
        var response = await grain.GetAudioItem(audioItemGuid);
        var dto = response.Adapt<MediaAudioItem>();
        return this.Ok(dto);
    }

    [HttpGet]
    [Route("{archiveId}/PhotoItems/{photoItemGuid}")]
    public async Task<IActionResult> GetPhotoItem([FromRoute] Guid archiveId, [FromRoute] Guid photoItemGuid)
    {
        var grain = await this.grainService.GetGrain(archiveId);
        var response = await grain.GetPhotoItem(photoItemGuid);
        var dto = response.Adapt<MediaPhotoItem>();
        return this.Ok(dto);
    }

    [HttpGet]
    [Route("{archiveId}/Stories/{newsItemGuid}")]
    public async Task<IActionResult> GetNewsItem([FromRoute] Guid archiveId, [FromRoute] Guid newsItemGuid)
    {
        var grain = await this.grainService.GetGrain(archiveId);
        var response = await grain.GetNewsItem(newsItemGuid);
        var dto = response.Adapt<NewsItemModel>();
        return this.Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<ArchiveModel>> CreateArchive([FromBody] ArchiveDto archiveDTO)
    {
        var newguid = Guid.NewGuid();
        TypeAdapterConfig<ArchiveDto, ArchiveModel>
            .NewConfig()
            .Map(dest => dest.Id,
                src => newguid);

        var archive = archiveDTO.Adapt<ArchiveModel>();
        archive.Id = newguid;
        var grain = await this.grainService.CreateGrain(archive.Id);
        await grain.CreateArchive(archive);
        return this.CreatedAtRoute(nameof(this.GetArchiveById), new { archiveId = newguid }, archive);
    }

    [HttpPost]
    [Route("{archiveId}/VideoItems")]
    public async Task<IActionResult> AddVideoItem(Guid archiveId, MediaVideoItem videoItem)
    {
        var grain = await this.grainService.GetGrain(archiveId);
        await grain.AddVideoItem(videoItem);
        return this.Ok(grain.Get());
    }

    [HttpPost]
    [Route("{archiveId}/AudioItems")]
    public async Task<IActionResult> AddAudioItems(Guid archiveId, MediaAudioItem audioItem)
    {
        var grain = await this.grainService.GetGrain(archiveId);
        await grain.AddAudioItem(audioItem);
        return this.Ok(grain.Get());
    }

    [HttpPost]
    [Route("{archiveId}/PhotoItems")]
    public async Task<IActionResult> AddPhotoItem(Guid archiveId, MediaPhotoItem photoItem)
    {
        var grain = await this.grainService.GetGrain(archiveId);
        await grain.AddPhotoItem(photoItem);
        return this.Ok(grain.Get());
    }

    [HttpPost]
    [Route("{archiveId}/NewsItems")]
    public async Task<IActionResult> AddNewsItems(Guid archiveId, NewsItemModel newsItem)
    {
        var grain = await this.grainService.GetGrain(archiveId);
        await grain.AddNewsItem(newsItem);
        return this.Ok(grain.Get());
    }

    [HttpDelete]
    [Route("{archiveId}/")]
    public async Task<IActionResult> DeleteArchive(Guid archiveId)
    {
        await this.grainService.DeleteGrain(archiveId);
        return this.Ok();
    }

    [HttpDelete]
    [Route("{archiveId}/VideoItems/{videoItemGuid}")]
    public async Task<IActionResult> DeleteVideoItem(Guid archiveId, Guid videoItemGuid)
    {
        var grain = await this.grainService.GetGrain(archiveId);
        await grain.DeleteVideoItem(videoItemGuid);
        return this.Ok();
    }

    [HttpDelete]
    [Route("{archiveId}/AudioItems/{audioItemGuid}")]
    public async Task<IActionResult> DeleteAudioItem(Guid archiveId, Guid audioItemGuid)
    {
        var grain = await this.grainService.GetGrain(archiveId);
        await grain.DeleteAudioItem(audioItemGuid);
        return this.Ok();
    }

    [HttpDelete]
    [Route("{archiveId}/PhotoItems/{photoItemGuid}")]
    public async Task<IActionResult> DeletePhotoItem(Guid archiveId, Guid photoItemGuid)
    {
        var grain = await this.grainService.GetGrain(archiveId);
        await grain.DeletePhotoItem(photoItemGuid);
        return this.Ok();
    }

    [HttpDelete]
    [Route("{archiveId}/Stories/{newsItemGuid}")]
    public async Task<IActionResult> DeleteNewsItem(Guid archiveId, Guid newsItemGuid)
    {
        var grain = await this.grainService.GetGrain(archiveId);
        await grain.DeleteNewsItem(newsItemGuid);
        return this.Ok();
    }

    [HttpPatch]
    [Route("{archiveId}/")]
    public async Task<IActionResult> UpdateArchive(Guid archiveId, UpdateArchiveRequest updateArchiveRequest)
    {
        TypeAdapterConfig<UpdateArchiveRequest, ArchiveModel>
       .NewConfig()
       .Map(dest => dest.Id,
           src => archiveId);

        var grain = await this.grainService.GetGrain(archiveId);
        var update = updateArchiveRequest.Adapt<ArchiveModel>();
        var updatedGrain = await grain.Update(update);
        var response = updatedGrain.Adapt<ArchiveDto>();
        return this.Ok(response);
    }
}