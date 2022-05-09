using Microsoft.AspNetCore.Mvc;
using Orleans;
using RedacteurPortaal.DomainModels.Archive;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.Api.DTOs;
using RedacteurPortaal.Grains.GrainInterfaces;
using RedacteurPortaal.Grains.GrainServices;
using RedacteurPortaal.Api.Models.Request;
using Mapster;
using RedacteurPortaal.DomainModels.NewsItem;

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
    public async Task<IActionResult> Get()
    {
        var archives = await this.grainService.GetGrains();
        return this.Ok(archives);
    }

    [HttpGet]
    [Route("{archiveId}")]
    public async Task<IActionResult> Get([FromRoute] Guid archiveId)
    {
        var archive = await this.grainService.GetGrain(archiveId);
        return this.Ok(archive.Get());
    }

    [HttpGet]
    [Route("{archiveId}/VideoItems")]
    public async Task<IActionResult> GetVideoItems([FromRoute] Guid archiveId)
    {
        var grain = await this.grainService.GetGrain(archiveId);
        return this.Ok(grain.GetAllVideoItems());
    }

    [HttpGet]
    [Route("{archiveId}/AudioItems")]
    public async Task<IActionResult> GetAudioItems([FromRoute] Guid archiveId)
    {
        var grain = await this.grainService.GetGrain(archiveId);
        return this.Ok(grain.GetAllAudioItems());
    }

    [HttpGet]
    [Route("{archiveId}/PhotoItems")]
    public async Task<IActionResult> GetPhotoItems([FromRoute] Guid archiveId)
    {
        var grain = await this.grainService.GetGrain(archiveId);
        return this.Ok(grain.GetAllPhotoItems());
    }

    [HttpGet]
    [Route("{archiveId}/Stories")]
    public async Task<IActionResult> GetNewsItems([FromRoute] Guid archiveId)
    {
        var grain = await this.grainService.GetGrain(archiveId);
        return this.Ok(grain.GetAllNewsItems());
    }

    [HttpGet]
    [Route("{archiveId}/VideoItems/{videoItemGuid}")]
    public async Task<IActionResult> GetVideoItem([FromRoute] Guid archiveId, [FromRoute] Guid videoItemGuid)
    {
        var grain = await this.grainService.GetGrain(archiveId);
        return this.Ok(grain.GetVideoItem(videoItemGuid));
    }

    [HttpGet]
    [Route("{archiveId}/AudioItems/{audioItemGuid}")]
    public async Task<IActionResult> GetAudioItem([FromRoute] Guid archiveId, [FromRoute] Guid audioItemGuid)
    {
        var grain = await this.grainService.GetGrain(archiveId);
        return this.Ok(grain.GetAudioItem(audioItemGuid));
    }

    [HttpGet]
    [Route("{archiveId}/PhotoItems/{photoItemGuid}")]
    public async Task<IActionResult> GetPhotoItem([FromRoute] Guid archiveId, [FromRoute] Guid photoItemGuid)
    {
        var grain = await this.grainService.GetGrain(archiveId);
        return this.Ok(grain.GetPhotoItem(photoItemGuid));
    }

    [HttpGet]
    [Route("{archiveId}/Stories/{newsItemGuid}")]
    public async Task<IActionResult> GetNewsItem([FromRoute] Guid archiveId, [FromRoute] Guid newsItemGuid)
    {
        var grain = await this.grainService.GetGrain(archiveId);
        return this.Ok(grain.GetNewsItem(newsItemGuid));
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
        return this.CreatedAtRoute("GetArchive", new { id = newguid }, archive);
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
        var archive = await this.grainService.GetGrain(guid);
        var update = new ArchiveUpdate(
            updateArchiveRequest.Title,
            updateArchiveRequest.Label,
            updateArchiveRequest.MediaPhotoItems,
            updateArchiveRequest.MediaVideoItems,
            updateArchiveRequest.MediaAudioItems,
            updateArchiveRequest.NewsItems,
            updateArchiveRequest.Scripts);
        await archive.Update(update);

        return this.Ok(archive.Get());
    }
}