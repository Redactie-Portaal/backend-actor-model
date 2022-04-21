﻿using Mapster;
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

namespace RedacteurPortaal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NewsItemController : Controller
{
    private readonly ILogger logger;
    private readonly IGrainManagementService<INewsItemGrain> grainService;

    public NewsItemController(ILogger<NewsItemController> logger, IGrainManagementService<INewsItemGrain> grainService)
    {
        this.logger = logger;
        this.grainService = grainService;
    }

    [HttpPost]
    public async Task<IActionResult> SaveNewsItem([FromBody] NewsItemDetailDto newsitem)
    {
        var newguid = Guid.NewGuid();
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

        TypeAdapterConfig<MediaVideoItemDto, MediaVideoItem>
            .NewConfig()
            .Map(dest => dest.Duration,
                  src => TimeSpan.FromSeconds(src.DurationSeconds));

        const string successMessage = "News item was created";
        var grain = await this.grainService.GetGrain(newguid);

        var update = newsitem.Adapt<NewsItemModel>();
        await grain.Update(update);
        this.logger.LogInformation(successMessage);
        return this.CreatedAtRoute("GetNewsItem", new { id = newguid }, update);
    }

    [HttpGet]
    [Route("{id}", Name = "GetNewsItem")]
    public async Task<IActionResult> GetNewsItem(Guid id)
    {
        var grain = await this.grainService.GetGrain(id);
        var response = await grain.Get();
        this.logger.LogInformation("News item fetched successfully");
        return this.Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetNewsItems()
    {
        var grain = await this.grainService.GetGrains();
        this.logger.LogInformation("News item fetched successfully");
        return this.Ok(grain.Select(x => x.Get()));
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteNewsItem(Guid id)
    {
        await this.grainService.DeleteGrain(id);
        this.logger.LogInformation("News item deleted successfully");
        return this.StatusCode(200, "News item deleted");
    }

    [HttpPatch]
    [Route("{id}")]
    public async Task<IActionResult> UpdateNewsItem(Guid id, [FromBody] UpdateNewsItemRequest request)
    {
        var grain = await this.grainService.GetGrain(id);

        TypeAdapterConfig<UpdateNewsItemRequest, NewsItemModel>
        .NewConfig()
        .Map(dest => dest.Id, 
        src => id);
        
        var update = request.Adapt<NewsItemModel>();

        await grain.Update(update);

        this.logger.LogInformation("News item updated successfully");
        return this.StatusCode(204, "News item updated");
    }
}