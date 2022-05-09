using Mapster;
using Microsoft.AspNetCore.Mvc;
using RedacteurPortaal.Api.DTOs;
using RedacteurPortaal.Api.Models;
using RedacteurPortaal.Api.Models.Request;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.Grains.GrainInterfaces;
using RedacteurPortaal.Grains.GrainServices;
using RedacteurPortaal.Helpers;

namespace RedacteurPortaal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NewsItemController : Controller
{
    private readonly IGrainManagementService<INewsItemGrain> grainService;

    public NewsItemController(IGrainManagementService<INewsItemGrain> grainService)
    {
        this.grainService = grainService;
    }

    [HttpPost]
    public async Task<ActionResult<NewsItemDto>> SaveNewsItem([FromBody] NewsItemDto newsitem)
    {
        Guid newguid = Guid.NewGuid();

        TypeAdapterConfig<NewsItemDto, NewsItemModel>
            .NewConfig()
            .Map(dest => dest.Id,
                src => newguid);

        TypeAdapterConfig<MediaVideoItemDto, MediaVideoItem>
            .NewConfig()
            .Map(dest => dest.Duration,
                  src => TimeSpan.FromSeconds(src.DurationSeconds));

        var grain = await this.grainService.CreateGrain(newguid);
        var update = newsitem.Adapt<NewsItemModel>();
        var createdGrain  = await grain.Update(update);
        var response = createdGrain.Adapt<NewsItemDto>();
        return this.CreatedAtRoute("GetNewsItem", new { id = newguid }, response);
    }

    [HttpGet]
    [Route("{id}", Name = "GetNewsItem")]
    public async Task<ActionResult<NewsItemDto>> GetNewsItem(Guid id)
    {
        var grain = await this.grainService.GetGrain(id);
        var response = await grain.Get();
        var dto = response.Adapt<NewsItemDto>();
        return this.Ok(dto);
    }

    [HttpGet]
    public async Task<ActionResult<List<NewsItemDto>>> GetNewsItems()
    {
        TypeAdapterConfig<NewsItemModel, NewsItemDto>
            .NewConfig()
            .Map(dest => dest.Source,
                src => new FeedSourceDto() { PlaceHolder = src.Source.PlaceHolder })
            .Map(dest => dest.LocationDetails,
                src => new LocationDto()
                {
                    City = src.LocationDetails.City,
                    Id = src.LocationDetails.Id,
                    Latitude = src.LocationDetails.Latitude,
                    Longitude = src.LocationDetails.Longitude,
                    Name = src.LocationDetails.Name,
                    Province = src.LocationDetails.Province,
                    Street = src.LocationDetails.Street,
                    Zip = src.LocationDetails.Zip
                })
            .Map(dest => dest.ContactDetails,
                src => src.ContactDetails.Select(x =>
                    new ContactDto()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        TelephoneNumber = x.TelephoneNumber,
                        Email = x.Email
                    }
                ).ToList())
            .Map(dest => dest.Audio,
                src => src.Audio.Select(x =>
                    new MediaAudioItemDto()).ToList())
            .Map(dest => dest.Photos,
                src => src.Photos.Select(x =>
                    new MediaPhotoItemDto()).ToList())
            .Map(dest => dest.Videos,
                src => src.Videos.Select(x =>
                    new MediaVideoItemDto()).ToList());

        var grain = await this.grainService.GetGrains();

        var response = (await grain.SelectAsync(async x => await
            x.Get())).AsQueryable();

        var converted = response.ProjectToType<NewsItemDto>(null).ToList();
        return this.Ok(converted);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteNewsItem(Guid id)
    {
        await this.grainService.DeleteGrain(id);
        return this.NoContent();
    }

    [HttpPatch]
    [Route("{id}")]
    public async Task<ActionResult<NewsItemDto>> UpdateNewsItem(Guid id, [FromBody] UpdateNewsItemRequest request)
    {
        TypeAdapterConfig<UpdateNewsItemRequest, NewsItemModel>
        .NewConfig()
        .Map(dest => dest.Id,
            src => id);

        var grain = await this.grainService.GetGrain(id);
        var update = request.Adapt<NewsItemModel>();
        var updatedGrain = await grain.Update(update);
        var response = updatedGrain.Adapt<NewsItemDto>();
        return this.Ok(response);
    }
}