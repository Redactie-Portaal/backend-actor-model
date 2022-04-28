using Microsoft.AspNetCore.Mvc;
using Orleans;
using RedacteurPortaal.Api.Models;
using RedacteurPortaal.Api.Models.Request;
using RedacteurPortaal.Data.Context;
using RedacteurPortaal.DomainModels;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.Grains.GrainInterfaces;
using RedacteurPortaal.Grains.Grains;

namespace RedacteurPortaal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExportDestinationController : Controller
{
    private readonly IExportPluginService pluginService;
    private readonly IClusterClient clusterClient;
    private readonly DataContext context;
    private readonly ILogger logger;

    public ExportDestinationController(IExportPluginService pluginService, IClusterClient clusterClient, DataContext context, ILogger<ExportDestinationController> logger)
    {
        this.pluginService = pluginService;
        this.clusterClient = clusterClient;
        this.context = context;
        this.logger = logger;
    }

    [HttpGet]
    public ActionResult<List<ExportPluginDto>> Get()
    {
        var plugins = this.pluginService.GetPlugins();

        return this.Ok(plugins);
    }

    [HttpGet("{guid}")]
    public ActionResult<ExportPluginDto> GetById(Guid guid)
    {
        var plugin = this.pluginService.GetPlugins()
            .Single(x => x.Id == guid);

        return this.Ok(plugin);
    }

    [HttpPost("{guid}/Actions.Publish")]
    public async Task<IActionResult> Publish(Guid guid, [FromBody]PublishItemRequest request)
    {
        var plugin =  this.pluginService.GetPlugins()
           .Single(x => x.Id == guid);
        _ = plugin ?? throw new KeyNotFoundException(); this.logger.LogWarning("est");

        var story = await this.clusterClient.GetGrain<INewsItemGrain>(request.StoryId).Get();
        var apiKey = this.context.PluginSettings.Single(x => x.PluginId == guid).ApiKey;

        _ = apiKey ?? throw new KeyNotFoundException();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        await plugin.Upload(new Export.Base.ExportItem()
        {
            AudioUri = story.Audio.Select(v => v.MediaLocation).ToArray(),
            Images = story.Photos.Select(x => x.Image).ToArray(),
            Name = story.Title,
            TextContent = plugin.TruncateForSocialMedia(story.Body, 140),
            VideoUri = story.Videos.Select(v => v.MediaLocation).ToArray(),
        }, apiKey);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        return this.Ok();
    }

    [HttpPatch]
    public async Task<IActionResult> Patch(Guid guid, [FromBody]UpdateExportPluginRequest request)
    {
        if (String.IsNullOrWhiteSpace(request.ApiKey))
        {
            throw new AppException("Api key cannot be null or empty");
        }

        var plugin = this.context.PluginSettings.Single(x => x.PluginId == guid);
        plugin.ApiKey = request.ApiKey;
        this.context.PluginSettings.Update(plugin);
        await this.context.SaveChangesAsync();

        return this.Ok();
    }
}