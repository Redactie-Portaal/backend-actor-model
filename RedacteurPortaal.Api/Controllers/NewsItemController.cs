using Mapster;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using RedacteurPortaal.Api.Models;
using RedacteurPortaal.Api.Models.Request;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.DomainModels.NewsItem.Requests;
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
    public async Task<IActionResult> SaveNewsItem([FromBody] NewsItemDetailDTO newsitem)
    {
        var newguid = Guid.NewGuid();
        TypeAdapterConfig<NewsItemDetailDTO, NewsItemModel>
            .NewConfig()
            .Map(dest => dest.Id,
                src => newguid);

            var tosave = newsitem.Adapt<NewsItemModel>();

            const string successMessage = "News item was created";
            var grain = await this.grainService.GetGrain(tosave.Id);
            var update = new NewsItemUpdate();
            await grain.Update(update);
            this.logger.LogInformation(successMessage);
            return this.CreatedAtRoute("GetNewsItem", new { guid = newguid }, newsitem);
    }

/*
    [HttpGet]
    [Route("id", Name ="GetNewsItem")]
    public async Task<IActionResult> GetNewsItem(Guid guid)
    {
        var grain = this.client.GetGrain<INewsItemGrain>(guid);
        var response = await grain.GetNewsItem(guid);
        this.logger.LogInformation("News item fetched successfully");
        return this.Ok(response);
    } */

    [HttpGet]
    [Route("id", Name = "GetNewsItem")]
    public async Task<IActionResult> GetNewsItems()
    {
        var grain = await this.grainService.GetGrains();
        this.logger.LogInformation("News item fetched successfully");
        return this.Ok(grain.Select(x=> x.Get()));
    }

    [HttpDelete]
    [Route("id")]
    public async Task<IActionResult> DeleteNewsItem(Guid guid)
    {
        var grain = await this.grainService.GetGrain(guid);
        await grain.Delete();
        this.logger.LogInformation("News item deleted successfully");
        return this.StatusCode(204, "News item deleted");
    }

    [HttpPatch]
    [Route("id")]
    public async Task<IActionResult> UpdateNewsItem([FromBody] UpdateNewsItemRequest request)
    {
        var grain = await this.grainService.GetGrain(request.Guid);
        var updateRequest = new NewsItemUpdate();
        await grain.Update(updateRequest);
        this.logger.LogInformation("News item updated successfully");
        return this.StatusCode(204, "News item updated");
    }
}