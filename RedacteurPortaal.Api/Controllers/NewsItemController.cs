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

    public NewsItemController(ILogger<NewsItemController> logger, IGrainManagementService<INewsItemGrain> grainService)
    {
        this.logger = logger;
        this.grainService = grainService;
    }

    [HttpPost]
    public async Task<ActionResult<NewsItemDetailDto>> SaveNewsItem([FromBody] NewsItemDetailDto newsitem)
    {
        Guid newguid = Guid.NewGuid();

        TypeAdapterConfig<NewsItemDetailDto, NewsItemModel>
            .NewConfig()
            .Map(dest => dest.Id,
                src => newguid);

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
        return this.CreatedAtRoute("GetNewsItem", new { id = this.newguid }, response);
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
        this.logger.LogInformation("News item fetched successfully");
        return this.Ok(response);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult<string>> DeleteNewsItem(Guid id)
    {
        await this.grainService.DeleteGrain(id);
        this.logger.LogInformation("News item deleted successfully");
        return this.NoContent();
    }

    [HttpPatch]
    [Route("{id}")]
    public async Task<ActionResult<NewsItemDetailDto>> UpdateNewsItem(Guid id, [FromBody] UpdateNewsItemRequest request)
    {
        TypeAdapterConfig<UpdateNewsItemRequest, NewsItemModel>
        .NewConfig()
        .Map(dest => dest.Id,
            src => id);

        var grain = await this.grainService.GetGrain(id);
        var update = request.Adapt<NewsItemModel>();
        await grain.Update(update);
        var updatedGrain = await this.grainService.GetGrain(update.Id);
        var updatedItem = await updatedGrain.Get();
        var response = updatedItem.Adapt<NewsItemDetailDto>();
        this.logger.LogInformation("News item updated successfully");
        return this.Ok(response);
    }
}