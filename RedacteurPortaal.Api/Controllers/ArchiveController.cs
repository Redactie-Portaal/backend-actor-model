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
using RedacteurPortaal.Api.Converters;

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
    public async Task<ActionResult<ArchiveDto>> GetAllArchives()
    {
       var grain = await this.grainService.GetGrains();

        var response = (await grain.SelectAsync(async x => await
        x.Get())).AsQueryable().ProjectToType<ArchiveDto>(null).ToList();

        return this.Ok(response);
    }

    [HttpGet]
    [Route("{archiveId}", Name = nameof(GetArchiveById))]
    public async Task<ActionResult<ArchiveDto>> GetArchiveById([FromRoute] Guid archiveId)
    {
        var archive = await this.grainService.GetGrain(archiveId);
        var response = await archive.Get();
        var dto = response.Adapt<ArchiveDto>();
        return this.Ok(dto);
    }

    [HttpGet]
    [Route("{archiveId}/VideoItems")]
    public async Task<ActionResult<List<MediaVideoItemDto>>> GetVideoItems([FromRoute] Guid archiveId)
    {
        var archive = await this.grainService.GetGrain(archiveId);
        var response = await archive.Get();
        var dto = response.Adapt<ArchiveDto>();
        return this.Ok(dto.MediaVideoItems);
    }

    [HttpGet]
    [Route("{archiveId}/AudioItems")]
    public async Task<ActionResult<List<MediaAudioItem>>> GetAudioItems([FromRoute] Guid archiveId)
    {
        var archive = await this.grainService.GetGrain(archiveId);
        var response = await archive.Get();
        var dto = response.Adapt<ArchiveDto>();
        return this.Ok(dto.MediaAudioItems);
    }

    [HttpGet]
    [Route("{archiveId}/PhotoItems")]
    public async Task<ActionResult<List<MediaPhotoItemDto>>> GetPhotoItems([FromRoute] Guid archiveId)
    {
        var archive = await this.grainService.GetGrain(archiveId);
        var response = await archive.Get();
        var dto = response.Adapt<ArchiveDto>();
        return this.Ok(dto.MediaPhotoItems);
    }

    [HttpGet]
    [Route("{archiveId}/Stories")]
    public async Task<ActionResult<List<NewsItemDto>>> GetNewsItems([FromRoute] Guid archiveId)
    {
        var archive = await this.grainService.GetGrain(archiveId);
        var response = await archive.Get();
        var dto = response.Adapt<ArchiveDto>();
        return this.Ok(dto.NewsItems);
    }

    [HttpGet]
    [Route("{archiveId}/VideoItems/{videoItemGuid}")]
    public async Task<ActionResult<MediaVideoItemDto>> GetVideoItem([FromRoute] Guid archiveId, [FromRoute] Guid videoItemGuid)
    {
        var grain = await this.grainService.GetGrain(archiveId);
        var response = await grain.GetVideoItem(videoItemGuid);
        var dto = response.Adapt<MediaVideoItemDto>();
        return this.Ok(dto);
    }

    [HttpGet]
    [Route("{archiveId}/AudioItems/{audioItemGuid}")]
    public async Task<ActionResult<MediaAudioItemDto>> GetAudioItem([FromRoute] Guid archiveId, [FromRoute] Guid audioItemGuid)
    {
        var grain = await this.grainService.GetGrain(archiveId);
        var response = await grain.GetAudioItem(audioItemGuid);
        var dto = response.Adapt<MediaAudioItemDto>();
        return this.Ok(dto);
    }

    [HttpGet]
    [Route("{archiveId}/PhotoItems/{photoItemGuid}")]
    public async Task<ActionResult<MediaPhotoItemDto>> GetPhotoItem([FromRoute] Guid archiveId, [FromRoute] Guid photoItemGuid)
    {
        var grain = await this.grainService.GetGrain(archiveId);
        var response = await grain.GetPhotoItem(photoItemGuid);
        var dto = response.Adapt<MediaPhotoItemDto>();
        return this.Ok(dto);
    }

    [HttpGet]
    [Route("{archiveId}/NewsItems/{newsItemGuid}")]
    public async Task<ActionResult<NewsItemDto>> GetNewsItem([FromRoute] Guid archiveId, [FromRoute] Guid newsItemGuid)
    {
        var grain = await this.grainService.GetGrain(archiveId);
        var response = await grain.GetNewsItem(newsItemGuid);
        var dto = response.Adapt<NewsItemDto>();
        return this.Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<ArchiveDto>> CreateArchive([FromBody] UpdateArchiveRequest archiveDTO)
    {
        var newguid = Guid.NewGuid();
        var archive = archiveDTO.AsDomainModel(newguid);
        var grain = await this.grainService.CreateGrain(archive.Id);
        var createdArchive  = await grain.CreateArchive(archive);
        var response = createdArchive.Adapt<ArchiveDto>();
        return this.CreatedAtRoute(nameof(this.GetArchiveById), new { archiveId = newguid }, response);
    }

    [HttpPost]
    [Route("{archiveId}/VideoItems")]
    public async Task<ActionResult<Guid>> AddVideoItem([FromRoute] Guid archiveId, [FromBody] Guid videoItemId)
    {
        var grain = await this.grainService.GetGrain(archiveId);
        await grain.AddVideoItem(videoItemId);
        return this.Ok(await grain.Get());
    }

    [HttpPost]
    [Route("{archiveId}/AudioItems")]
    public async Task<ActionResult<Guid>> AddAudioItems([FromRoute] Guid archiveId, [FromBody] Guid audioItemId)
    {
        var grain = await this.grainService.GetGrain(archiveId);
        await grain.AddAudioItem(audioItemId);
        return this.Ok(await grain.Get());
    }

    [HttpPost]
    [Route("{archiveId}/PhotoItems")]
    public async Task<ActionResult<Guid>> AddPhotoItem([FromRoute] Guid archiveId, [FromBody] Guid photoItemId)
    {
        var grain = await this.grainService.GetGrain(archiveId);
        await grain.AddPhotoItem(photoItemId);
        return this.Ok(await grain.Get());
    }

    [HttpPost]
    [Route("{archiveId}/NewsItems")]
    public async Task<ActionResult<Guid>> AddNewsItems([FromRoute] Guid archiveId, [FromBody] Guid newsItemId)
    {
        var grain = await this.grainService.GetGrain(archiveId);
        var createdGrain = await grain.AddNewsItem(newsItemId);
        var archive = await grain.Get();
        return this.Ok(archive);
    }

    [HttpDelete]
    [Route("{archiveId}")]
    public async Task<IActionResult> DeleteArchive(Guid archiveId)
    {
        await this.grainService.DeleteGrain(archiveId);
        return this.NoContent();
    }

    [HttpDelete]
    [Route("{archiveId}/VideoItems/{videoItemGuid}")]
    public async Task<IActionResult> DeleteVideoItem([FromRoute] Guid archiveId, [FromRoute] Guid videoItemGuid)
    {
        var grain = await this.grainService.GetGrain(archiveId);
        await grain.DeleteVideoItem(videoItemGuid);
        return this.NoContent();
    }

    [HttpDelete]
    [Route("{archiveId}/AudioItems/{audioItemGuid}")]
    public async Task<IActionResult> DeleteAudioItem([FromRoute] Guid archiveId, [FromRoute] Guid audioItemGuid)
    {
        var grain = await this.grainService.GetGrain(archiveId);

        var item = grain.Get();
        
        await grain.DeleteAudioItem(audioItemGuid);
        return this.NoContent();
    }

    [HttpDelete]
    [Route("{archiveId}/PhotoItems/{photoItemGuid}")]
    public async Task<IActionResult> DeletePhotoItem([FromRoute] Guid archiveId, [FromRoute] Guid photoItemGuid)
    {
        var grain = await this.grainService.GetGrain(archiveId);
        await grain.DeletePhotoItem(photoItemGuid);
        return this.NoContent();
    }

    [HttpDelete]
    [Route("{archiveId}/NewsItems/{newsItemGuid}")]
    public async Task<IActionResult> DeleteNewsItem([FromRoute] Guid archiveId, [FromRoute] Guid newsItemGuid)
    {
        var grain = await this.grainService.GetGrain(archiveId);
        await grain.DeleteNewsItem(newsItemGuid);
        return this.NoContent();
    }

    [HttpPatch]
    [Route("{archiveId}")]
    public async Task<IActionResult> UpdateArchive([FromRoute] Guid archiveId, [FromBody] UpdateArchiveRequest updateArchiveRequest)
    {
        var typeConfig = new TypeAdapterConfig();
        typeConfig.NewConfig<ArchiveModel, ArchiveDto>().Map(dest => dest.Id, src => archiveId)
            .Fork(config => config.ForType<MediaAudioItem, MediaAudioItemDto>()
            .Map(dest => dest.DurationSeconds, src => src.Duration.TotalSeconds));
        typeConfig.ForType<MediaVideoItem, MediaVideoItemDto>().Map(dest => dest.DurationSeconds, src => src.Duration.TotalSeconds);

        var grain = await this.grainService.GetGrain(archiveId);
        var update = updateArchiveRequest.AsDomainModel(archiveId);
        var updatedGrain = await grain.Update(update);
        var response = updatedGrain.Adapt<ArchiveDto>(typeConfig);
        return this.Ok(response);
    }
}