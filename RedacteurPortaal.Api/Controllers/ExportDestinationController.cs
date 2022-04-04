﻿using Microsoft.AspNetCore.Mvc;
using Orleans;
using RedacteurPortaal.Api.Models;
using RedacteurPortaal.Api.Models.Request;
using RedacteurPortaal.Data.Context;
using RedacteurPortaal.DomainModels;
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
    private readonly DataContext context;

    /// <summary>
    ///     Initializes a new instance of the <see cref="NewsItemController" /> class.
    /// </summary>
    public ExportDestinationController(IExportPluginService pluginService, IClusterClient clusterClient, DataContext context)
    {
        this.pluginService = pluginService;
        this.clusterClient = clusterClient;
        this.context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var plugins = await this.pluginService.GetPlugins();

        return this.Ok(plugins);
    }

    [HttpGet("{guid}")]
    public async Task<IActionResult> GetById(Guid guid)
    {
        var plugin = (await this.pluginService.GetPlugins())
            .Single(x => x.Id == guid);

        return this.Ok(plugin);
    }

    [HttpPost("{guid}/Actions.Publish")]
    public async Task<IActionResult> Publish(Guid guid, [FromBody]PublishItemRequest request)
    {
        var plugin = (await this.pluginService.GetPlugins())
           .Single(x => x.Id == guid);
        _ = plugin ?? throw new KeyNotFoundException();

        var story = await this.clusterClient.GetGrain<INewsItemGrain>(request.StoryId).GetNewsItem(request.StoryId);
        var apiKey = this.context.PluginSettings.Single(x => x.PluginId == guid).ApiKey;

        _ = apiKey ?? throw new KeyNotFoundException();

        await plugin.Upload(new Export.Base.ExportItem()
        {
            AudioUri = story.Audio.MediaLocation,
            Images = story.Photos.Select(x => x.Image).ToArray(),
            Name = story.Title,
            ShortText = story.Body.ShortDescription,
            TextContent = story.Body.Description,
            VideoUri = story.Video.MediaLocation
        }, apiKey);

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