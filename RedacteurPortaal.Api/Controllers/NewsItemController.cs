using Microsoft.AspNetCore.Mvc;
using Orleans;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.Grains.GrainInterfaces;

namespace RedacteurPortaal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NewsItemController : Controller
{
    private readonly IClusterClient client;
    private readonly ILogger logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="NewsItemController" /> class.
    /// </summary>
    /// <param name="client">Cluster client to use.</param>
    /// <param name="logger">Logger to use.</param>
    public NewsItemController(IClusterClient client, ILogger logger)
    {
        this.client = client;
        this.logger = logger;
    }

    [Route("/newsitem")]
    [HttpPost]
    public async Task<IActionResult> SaveNewsItem([FromBody] NewsItemModel newsitem)
    {
        try
        {
            var newguid = Guid.NewGuid();
            newsitem.Id = newguid;
            const string successMessage = "News item was created";
            var grain = client.GetGrain<INewsItemGrain>(newsitem.Id);
            await grain.AddNewsItem(newsitem);
            logger.LogInformation(successMessage);
            return StatusCode(201, successMessage);
        }

        [HttpPost]
        public async Task<IActionResult> SaveNewsItem([FromBody] NewsItemModel newsitem)
        {
            logger.LogError(ex.Message);
            return StatusCode(500, "An internal server error has occured");
        }
    }

        [HttpGet]
        [Route("{guid}")]
        public async Task<IActionResult> GetNewsItem(Guid guid)
        {
            var grain = client.GetGrain<INewsItemGrain>(guid);
            var response = await grain.GetNewsItem(guid);
            logger.LogInformation("News item fetched successfully");
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteNewsItem(Guid guid)
        {
            logger.LogError(ex.Message);
            return StatusCode(500, "An internal server error has occured");
        }
    }

        [HttpPut]
        public async Task<IActionResult> UpdateNewsItem(string name, Guid guid)
        {
            var grain = client.GetGrain<INewsItemGrain>(guid);
            await grain.DeleteNewsItem(guid);
            logger.LogInformation("News item deleted successfully");
            return StatusCode(204, "News item deleted");
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return StatusCode(500, "An internal server error has occured");
        }
    }

    [Route("/newsitem")]
    [HttpPut]
    public async Task<IActionResult> UpdateNewsItem(string name, Guid guid)
    {
        try
        {
            var grain = client.GetGrain<INewsItemGrain>(guid);
            await grain.UpdateNewsItem(name, guid);
            logger.LogInformation("News item updated successfully");
            return this.StatusCode(204, "News item updated");
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return StatusCode(500, "An internal server error has occured");
        }
    }
}