using Microsoft.AspNetCore.Mvc;
using Orleans;
using RedacteurPortaal.Api.Models;
using RedacteurPortaal.Api.Models.Request;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.Grains.GrainInterfaces;
using RedacteurPortaal.Grains.Grains;

namespace RedacteurPortaal.Api.Controllers;

[ApiController]
[Route("api/ExportDestination")]
public class ExportDestinationController : Controller
{
    private readonly IExportPluginService pluginService;
    private readonly IClusterClient clusterClient;

    /// <summary>
    ///     Initializes a new instance of the <see cref="NewsItemController" /> class.
    /// </summary>
    public ExportDestinationController(IExportPluginService pluginService, IClusterClient clusterClient)
    {
        this.pluginService = pluginService;
        this.clusterClient = clusterClient;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var plugins = await pluginService.GetPlugins();

        return Ok(plugins);
    }

    [HttpGet("{guid}")]
    public async Task<IActionResult> GetById(Guid guid)
    {
        var plugin = (await pluginService.GetPlugins())
            .Single(x => x.Id == guid);

        return Ok(plugin);
    }

    [HttpPost("{guid}/Actions.Publish")]
    public async Task<IActionResult> Publish(Guid guid, [FromBody]PublishItemRequest request)
    {
        var plugin = (await pluginService.GetPlugins())
           .Single(x => x.Id == guid);
        var story = await clusterClient.GetGrain<INewsItemGrain>(request.StoryId).GetNewsItem(request.StoryId);

        var apiKey = "";

        // TODO: Zorg dat deze values kloppen.
        await plugin.Upload(new Export.Base.ExportItem() {
            AudioUri = new Uri("audisource"),
            Images = new string[] { story.Photo.Image },
            Name = story.Title,
            ShortText = story.Body.ShortDescription,
            TextContent = story.Body.Description,
            VideoUri = new Uri(story.Video.EPG)
        }, apiKey);


        return Ok();
    }

    [HttpPatch]
    public async Task<IActionResult> Patch(Guid guid, [FromBody]UpdateExportPluginRequest request)
    {
        return Ok(new ExportPluginDto());
    }
}