﻿using Microsoft.AspNetCore.Mvc;
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
    ///     Initializes a new instance of the <see cref="NewsItemController" /> class.
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
            var grain = this.client.GetGrain<INewsItemGrain>(newsitem.Id);
            await grain.AddNewsItem(newsitem);
            this.logger.LogInformation(successMessage);
            return this.StatusCode(201, successMessage);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex.Message);
            return this.StatusCode(500, "An internal server error has occured");
        }
    }

    [Route("/newsitem")]
    [HttpGet]
    public async Task<IActionResult> GetNewsItem(Guid guid)
    {
        try
        {
            var grain = this.client.GetGrain<INewsItemGrain>(guid);
            var response = await grain.GetNewsItem(guid);
            this.logger.LogInformation("News item fetched successfully");
            return this.Ok(response);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex.Message);
            return this.StatusCode(500, "An internal server error has occured");
        }
    }

    [Route("/newsitem")]
    [HttpDelete]
    public async Task<IActionResult> DeleteNewsItem(Guid guid)
    {
        try
        {
            var grain = this.client.GetGrain<INewsItemGrain>(guid);
            await grain.DeleteNewsItem(guid);
            this.logger.LogInformation("News item deleted successfully");
            return this.StatusCode(204, "News item deleted");
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex.Message);
            return this.StatusCode(500, "An internal server error has occured");
        }
    }

    [Route("/newsitem")]
    [HttpPut]
    public async Task<IActionResult> UpdateNewsItem(string name, Guid guid)
    {
        try
        {
            var grain = this.client.GetGrain<INewsItemGrain>(guid);
            await grain.UpdateNewsItem(name, guid);
            this.logger.LogInformation("News item updated successfully");
            return this.StatusCode(204, "News item updated");
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex.Message);
            return this.StatusCode(500, "An internal server error has occured");
        }
    }
}