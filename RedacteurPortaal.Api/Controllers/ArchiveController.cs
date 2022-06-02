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
    public async Task<ActionResult<ArchiveModel>> GetAllArchives()
    {
        var grain = await this.grainService.GetGrains();

        var response = (await grain.SelectAsync(async x => await
        x.Get())).AsQueryable().ProjectToType<ArchiveDto>(null).ToList();

        return this.Ok(response);
    }

    [HttpGet]
    [Route("{archiveId}", Name = nameof(GetArchiveById))]
    public async Task<ActionResult<ArchiveModel>> GetArchiveById([FromRoute] Guid archiveId)
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
    public async Task<ActionResult<MediaVideoItemDto>> AddVideoItem([FromRoute] Guid archiveId, [FromBody] MediaVideoItemDto videoItem)
    {
        var newguid = Guid.NewGuid();

        var archivedVideoItem = MediaItemDTOConverter.ConvertMediaVideoDTO(videoItem, newguid);
        var grain = await this.grainService.GetGrain(archiveId);
        await grain.AddVideoItem(archivedVideoItem);
        return this.Ok(await grain.Get());
    }

    [HttpPost]
    [Route("{archiveId}/AudioItems")]
    public async Task<IActionResult> AddAudioItems([FromRoute] Guid archiveId, [FromBody] MediaAudioItemDto audioItem)
    {
        var newguid = Guid.NewGuid();
        TypeAdapterConfig<MediaAudioItemDto, MediaAudioItem>
            .NewConfig()
            .Map(dest => dest.Id,
                src => newguid);

        var archivedAudioItem = audioItem.Adapt<MediaAudioItem>();

        var grain = await this.grainService.GetGrain(archiveId);
        await grain.AddAudioItem(archivedAudioItem);
        return this.Ok(await grain.Get());
    }

    [HttpPost]
    [Route("{archiveId}/PhotoItems")]
    public async Task<IActionResult> AddPhotoItem([FromRoute] Guid archiveId, [FromBody] MediaPhotoItemDto photoItem)
    {
        var newguid = Guid.NewGuid();
        TypeAdapterConfig<MediaPhotoItemDto, MediaPhotoItem>
            .NewConfig()
            .Map(dest => dest.Id,
                src => newguid);

        var archivedPhotoItem = photoItem.Adapt<MediaPhotoItem>();

        var grain = await this.grainService.GetGrain(archiveId);
        await grain.AddPhotoItem(archivedPhotoItem);
        return this.Ok(await grain.Get());
    }

    [HttpPost]
    [Route("{archiveId}/NewsItems")]
    public async Task<IActionResult> AddNewsItems([FromRoute] Guid archiveId, [FromBody] NewsItemDto newsItem)
    {

        Guid newguid = Guid.NewGuid();

        TypeAdapterConfig<NewsItemDto, NewsItemModel>
            .NewConfig()
            .Map(dest => dest.Id,
                src => newguid);

        var grain = await this.grainService.GetGrain(archiveId);
        var update = newsItem.Adapt<NewsItemModel>();
        var createdGrain = await grain.AddNewsItem(update);
        var response = createdGrain.Adapt<NewsItemDto>();
        return this.Ok(await grain.Get());
    }

    [HttpDelete]
    [Route("{archiveId}")]
    public async Task<IActionResult> DeleteArchive(Guid archiveId)
    {
        await this.grainService.DeleteGrain(archiveId);
        return this.Ok();
    }

    [HttpDelete]
    [Route("{archiveId}/VideoItems/{videoItemGuid}")]
    public async Task<IActionResult> DeleteVideoItem([FromRoute] Guid archiveId, [FromRoute] Guid videoItemGuid)
    {
        var grain = await this.grainService.GetGrain(archiveId);
        await grain.DeleteVideoItem(videoItemGuid);
        return this.Ok();
    }

    [HttpDelete]
    [Route("{archiveId}/AudioItems/{audioItemGuid}")]
    public async Task<IActionResult> DeleteAudioItem([FromRoute] Guid archiveId, [FromRoute] Guid audioItemGuid)
    {
        var grain = await this.grainService.GetGrain(archiveId);
        await grain.DeleteAudioItem(audioItemGuid);
        return this.Ok();
    }

    [HttpDelete]
    [Route("{archiveId}/PhotoItems/{photoItemGuid}")]
    public async Task<IActionResult> DeletePhotoItem([FromRoute] Guid archiveId, [FromRoute] Guid photoItemGuid)
    {
        var grain = await this.grainService.GetGrain(archiveId);
        await grain.DeletePhotoItem(photoItemGuid);
        return this.Ok();
    }

    [HttpDelete]
    [Route("{archiveId}/Stories/{newsItemGuid}")]
    public async Task<IActionResult> DeleteNewsItem([FromRoute] Guid archiveId, [FromRoute] Guid newsItemGuid)
    {
        var grain = await this.grainService.GetGrain(archiveId);
        await grain.DeleteNewsItem(newsItemGuid);
        return this.Ok();
    }

    [HttpPatch]
    [Route("{archiveId}")]
    public async Task<IActionResult> UpdateArchive([FromRoute] Guid archiveId, [FromBody] UpdateArchiveRequest updateArchiveRequest)
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