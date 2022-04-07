using Mapster;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using RedacteurPortaal.Api.Models;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.Grains.GrainInterfaces;
using RedacteurPortaal.Grains.GrainServices;

namespace RedacteurPortaal.Api.Controllers;

[ApiController]
[Route("api/newsitem")]
public class NewsItemController : Controller
{
    private readonly ILogger logger;
    private readonly IGrainManagementService<INewsItemGrain> grainService;

    /// <summary>
    ///     Initializes a new instance of the <see cref="NewsItemController" /> class.
    /// </summary>
    /// <param name="client">Cluster client to use.</param>
    /// <param name="logger">Logger to use.</param>
    public NewsItemController(ILogger<NewsItemController> logger, IGrainManagementService<INewsItemGrain> grainService)
    {
        this.logger = logger;
        this.grainService = grainService;
    }

    [HttpPost]
    public async Task<IActionResult> SaveNewsItem([FromBody] NewsItemDetailDTO newsitem)
    {
            var newguid = Guid.NewGuid();
            var tosave = newsitem.Adapt<NewsItemModel>();
            tosave.Id = newguid;

            const string successMessage = "News item was created";
            var grain = await this.grainService.GetGrain(tosave.Id);
            await grain.AddNewsItem(tosave);
            this.logger.LogInformation(successMessage);
        return this.StatusCode(201, newguid.ToString());
    }

    [HttpGet]
    [Route(":id")]
    public async Task<IActionResult> GetNewsItem(Guid guid)
    {
            var grain = await this.grainService.GetGrain(guid);
            var response = await grain.GetNewsItem(guid);
            this.logger.LogInformation("News item fetched successfully");
            return this.Ok(response);
    }

    [HttpDelete]
    [Route(":id")]
    public async Task<IActionResult> DeleteNewsItem(Guid guid)
    {
            await this.grainService.DeleteGrain(guid);
            this.logger.LogInformation("News item deleted successfully");
            return this.StatusCode(204, "News item deleted");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateNewsItem(string name, Guid guid)
    {
        var grain = await this.grainService.GetGrain(guid);

        await grain.UpdateNewsItem(name, guid);
            this.logger.LogInformation("News item updated successfully");
            return this.StatusCode(204, "News item updated");
    }
}