using Mapster;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using RedacteurPortaal.Api.DTOs;
using RedacteurPortaal.Api.Models;
using RedacteurPortaal.Api.Models.Request;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.DomainModels.Shared;
using RedacteurPortaal.Grains.GrainInterfaces;
using RedacteurPortaal.Grains.GrainServices;
using RedacteurPortaal.Helpers;

namespace RedacteurPortaal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NewsItemController : Controller
{
    private readonly ILogger logger;
    private readonly IGrainManagementService<INewsItemGrain> grainService;
    private Guid newguid;

    public NewsItemController(ILogger<NewsItemController> logger, IGrainManagementService<INewsItemGrain> grainService)
    {
        this.logger = logger;
        this.grainService = grainService;

        TypeAdapterConfig<NewsItemDetailDto, NewsItemModel>
            .NewConfig()
            .Map(dest => dest.ContactDetails,
                src => src.ContactDetails.AsQueryable().ProjectToType<Contact>(null).ToList())
            .Map(dest => dest.LocationDetails,
                src => src.LocationDetails.Adapt<Location>())
            .Map(dest => dest.Body,
                src => src.Body != null ? src.Body.Adapt<ItemBody>() : new ItemBody())
            .Map(dest => dest.Source,
                src => src.Source != null ? src.Source.Adapt<FeedSource>() : new FeedSource())
            .Map(dest => dest.Videos,
                src => src.Videos.AsQueryable().ProjectToType<MediaVideoItem>(null).ToList())
            .Map(dest => dest.Audio,
                src => src.Audio.AsQueryable().ProjectToType<MediaAudioItem>(null).ToList())
            .Map(dest => dest.Photos,
                src => src.Photos.AsQueryable().ProjectToType<MediaPhotoItem>(null).ToList())
            .Map(dest => dest.Id,
                src => newguid);

        TypeAdapterConfig<NewsItemModel, NewsItemDetailDto>
            .NewConfig()
            .Map(dest => dest.ContactDetails,
                src => src.ContactDetails.AsQueryable().ProjectToType<ContactDto>(null).ToList())
            .Map(dest => dest.LocationDetails,
                src => src.LocationDetails.Adapt<LocationDto>())
            .Map(dest => dest.Body,
                src => src.Body != null ? src.Body.Adapt<ItemBodyDto>() : new ItemBodyDto())
            .Map(dest => dest.Source,
                src => src.Source != null ? src.Source.Adapt<FeedSourceDto>() : new FeedSourceDto())
            .Map(dest => dest.Videos,
                src => src.Videos.AsQueryable().ProjectToType<MediaVideoItemDto>(null).ToList())
            .Map(dest => dest.Audio,
                src => src.Audio.AsQueryable().ProjectToType<MediaAudioItemDto>(null).ToList())
            .Map(dest => dest.Photos,
                src => src.Photos.AsQueryable().ProjectToType<MediaPhotoItemDto>(null).ToList())
            .Map(dest => dest.Id,
                src => src.Id);
    }

    [HttpPost]
    public async Task<ActionResult<NewsItemDetailDto>> SaveNewsItem([FromBody] NewsItemDetailDto newsitem)
    {
        newguid = Guid.NewGuid();

        TypeAdapterConfig<MediaVideoItemDto, MediaVideoItem>
            .NewConfig()
            .Map(dest => dest.Duration,
                  src => TimeSpan.FromSeconds(src.DurationSeconds));

        const string successMessage = "News item was created";
        var grain = await this.grainService.CreateGrain(newguid);
        var update = newsitem.Adapt<NewsItemModel>();
        await grain.Update(update);

        var createdGrain = await this.grainService.GetGrain(newguid);
        var createdItem = await createdGrain.Get();
        var response = createdItem.Adapt<NewsItemDetailDto>();

        this.logger.LogInformation(successMessage);
        return this.CreatedAtRoute("GetNewsItem", new { id = newguid }, response);
    }

    [HttpGet]
    [Route("{id}", Name = "GetNewsItem")]
    public async Task<ActionResult<NewsItemDetailDto>> GetNewsItem(Guid id)
    {
        var grain = await this.grainService.GetGrain(id);
        var response = await grain.Get();
        this.logger.LogInformation("News item fetched successfully");
        var dto = response.Adapt<NewsItemDetailDto>();
        return this.Ok(dto);
    }

    [HttpGet]
    public async Task<ActionResult<List<NewsItemDetailDto>>> GetNewsItems()
    {
        var grain = await this.grainService.GetGrains();
        var response = (await grain.SelectAsync(async x => await x.Get())).AsQueryable().ProjectToType<NewsItemDetailDto>(null).ToList();
        var dto = response.Adapt<NewsItemDetailDto>();
        this.logger.LogInformation("News item fetched successfully");
        return this.Ok(response);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult<string>> DeleteNewsItem(Guid id)
    {
        await this.grainService.DeleteGrain(id);
        this.logger.LogInformation("News item deleted successfully");
        return this.StatusCode(200, "News item deleted");
    }

    [HttpPatch]
    [Route("{id}")]
    public async Task<ActionResult<NewsItemDetailDto>> UpdateNewsItem(Guid id, [FromBody] UpdateNewsItemRequest request)
    {
        var grain = await this.grainService.GetGrain(id);

        TypeAdapterConfig<UpdateNewsItemRequest, NewsItemModel>
        .NewConfig()
        .Map(dest => dest.Id,
        src => id);

        var update = request.Adapt<NewsItemModel>();

        await grain.Update(update);
        var updatedGrain = await this.grainService.GetGrain(update.Id);
        var updatedItem = await updatedGrain.Get();
        var response = updatedItem.Adapt<NewsItemDetailDto>();
        this.logger.LogInformation("News item updated successfully");
        return this.StatusCode(200, response);
    }
}