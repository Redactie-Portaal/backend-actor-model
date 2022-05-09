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
    public async Task<IActionResult> GetAllArchives()
    {
        var grain = await this.grainService.GetGrains();
        
        var response = (await grain.SelectAsync(async x => await 
        x.Get())).AsQueryable().ProjectToType<ArchiveDto>(null).ToList();
        return this.Ok(response);
    }

    [HttpGet]
    [Route("{archiveId}", Name = nameof(GetArchiveById))]
    public async Task<IActionResult> GetArchiveById(Guid archiveId)
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
    public async Task<IActionResult> AddVideoItem(Guid guid, MediaVideoItem videoItem)
    {
        var grain = await this.grainService.GetGrain(guid);
        await grain.AddVideoItem(videoItem);
        return this.Ok(grain.Get());
    }

    [HttpPost]
    [Route("{archiveId}/AudioItems")]
    public async Task<IActionResult> AddAudioItems(Guid guid, MediaAudioItem audioItem)
    {
        var grain = await this.grainService.GetGrain(guid);
        await grain.AddAudioItem(audioItem);
        return this.Ok(grain.Get());
    }

    [HttpPost]
    [Route("{archiveId}/PhotoItems")]
    public async Task<IActionResult> AddPhotoItem(Guid guid, MediaPhotoItem photoItem)
    {
        var grain = await this.grainService.GetGrain(guid);
        await grain.AddPhotoItem(photoItem);
        return this.Ok(grain.Get());
    }

    [HttpPost]
    [Route("{archiveId}/NewsItems")]
    public async Task<IActionResult> AddNewsItems(Guid guid, NewsItemModel newsItem)
    {
        var grain = await this.grainService.GetGrain(guid);
        await grain.AddNewsItem(newsItem);
        return this.Ok(grain.Get());
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteArchive(Guid guid)
    {
        await this.grainService.DeleteGrain(guid);
        return this.Ok();
    }

    [HttpDelete]
    [Route("{archiveId}/VideoItems/{videoItemGuid}")]
    public async Task<IActionResult> DeleteVideoItem(Guid guid, Guid videoItemGuid)
    {
        var grain = await this.grainService.GetGrain(guid);
        await grain.DeleteVideoItem(videoItemGuid);
        return this.Ok();
    }

    [HttpDelete]
    [Route("{archiveId}/AudioItems/{audioItemGuid}")]
    public async Task<IActionResult> DeleteAudioItem(Guid guid, Guid audioItemGuid)
    {
        var grain = await this.grainService.GetGrain(guid);
        await grain.DeleteAudioItem(audioItemGuid);
        return this.Ok();
    }

    [HttpDelete]
    [Route("{archiveId}/PhotoItems/{photoItemGuid}")]
    public async Task<IActionResult> DeletePhotoItem(Guid guid, Guid photoItemGuid)
    {
        var grain = await this.grainService.GetGrain(guid);
        await grain.DeletePhotoItem(photoItemGuid);
        return this.Ok();
    }

    [HttpDelete]
    [Route("{archiveId}/Stories/{newsItemGuid}")]
    public async Task<IActionResult> DeleteNewsItem(Guid guid, Guid newsItemGuid)
    {
        var grain = await this.grainService.GetGrain(guid);
        await grain.DeleteNewsItem(newsItemGuid);
        return this.Ok();
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateArchive(Guid guid, UpdateArchiveRequest updateArchiveRequest)
    {
        TypeAdapterConfig<UpdateArchiveRequest, ArchiveModel>
       .NewConfig()
       .Map(dest => dest.Id,
           src => guid);

        var grain = await this.grainService.GetGrain(guid);
        var update = updateArchiveRequest.Adapt<ArchiveModel>();
        var updatedGrain = await grain.Update(update);
        var response = updatedGrain.Adapt<ArchiveDto>();
        return this.Ok(response);
    }
}