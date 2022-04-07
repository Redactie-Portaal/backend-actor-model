﻿using Mapster;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using RedacteurPortaal.Api.Models;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.Grains.GrainInterfaces;

namespace RedacteurPortaal.Api.Controllers;

[ApiController]
[Route("api/newsitem")]
public class NewsItemController : Controller
{
    private readonly IClusterClient client;
    private readonly ILogger logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="NewsItemController" /> class.
    /// </summary>
    /// <param name="client">Cluster client to use.</param>
    /// <param name="logger">Logger to use.</param>
    public NewsItemController(IClusterClient client, ILogger<NewsItemController> logger)
    {
        this.client = client;
        this.logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> SaveNewsItem([FromBody] NewsItemDetailDTO newsitem)
    {
            var newguid = Guid.NewGuid();
            var tosave = newsitem.Adapt<NewsItemModel>();
            tosave.Id = newguid;

            const string successMessage = "News item was created";
            var grain = this.client.GetGrain<INewsItemGrain>(tosave.Id);
            await grain.AddNewsItem(tosave);
            this.logger.LogInformation(successMessage);
        return this.StatusCode(201, newguid.ToString());
    }

    [HttpGet]
    [Route(":id")]
    public async Task<IActionResult> GetNewsItem(Guid guid)
    {
            var grain = this.client.GetGrain<INewsItemGrain>(guid);
            var response = await grain.GetNewsItem(guid);
            this.logger.LogInformation("News item fetched successfully");
            return this.Ok(response);
    }

    [HttpDelete]
    [Route(":id")]
    public async Task<IActionResult> DeleteNewsItem(Guid guid)
    {
            var grain = this.client.GetGrain<INewsItemGrain>(guid);
            await grain.DeleteNewsItem(guid);
            this.logger.LogInformation("News item deleted successfully");
            return this.StatusCode(204, "News item deleted");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateNewsItem(string name, Guid guid)
    {
            var grain = this.client.GetGrain<INewsItemGrain>(guid);
            await grain.UpdateNewsItem(name, guid);
            this.logger.LogInformation("News item updated successfully");
            return this.StatusCode(204, "News item updated");
    }
}