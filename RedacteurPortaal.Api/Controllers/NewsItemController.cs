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
    public async Task<ActionResult<NewsItemDto>> SaveNewsItem([FromBody] UpdateNewsItemRequest newsitem)
    {
        Guid newguid = Guid.NewGuid();

        TypeAdapterConfig<NewsItemDto, NewsItemModel>
            .NewConfig()
            .Map(dest => dest.Id,
                src => newguid);

        TypeAdapterConfig<MediaVideoItemDto, MediaVideoItem>
            .NewConfig()
            .Map(dest => dest.Duration,
                  src => TimeSpan.FromSeconds(src.DurationSeconds));

        var grain = await this.grainService.CreateGrain(newguid);
        var update = newsitem.Adapt<NewsItemModel>();
        var createdGrain  = await grain.Update(update);
        var response = createdGrain.Adapt<NewsItemDto>();
        return this.CreatedAtRoute("GetNewsItem", new { id = newguid }, response);
    }

    [HttpGet]
    [Route("{id}", Name = "GetNewsItem")]
    public async Task<ActionResult<NewsItemDto>> GetNewsItem(Guid id)
    {
        var grain = await this.grainService.GetGrain(id);
        var response = await grain.Get();
        var dto = response.Adapt<NewsItemDto>();
        return this.Ok(dto);
    }

    [HttpGet]
    public async Task<ActionResult<List<NewsItemDto>>> GetNewsItems()
    {
        var grain = await this.grainService.GetGrains();
        
        var response = (await grain.SelectAsync(async x => await 
        x.Get())).AsQueryable().ProjectToType<NewsItemDto>(null).ToList();
        return this.Ok(response);
    }

    //[HttpGet]
    //public async Task<ActionResult<List<NewsItemDto>>> FilterNewsItem(DateTime startDate, DateTime endDate, string employee, string dossier, string status)
    //{
    //    var grain = await this.grainService.GetGrains();

    //    var list = (await grain.SelectAsync(async x => await
    //    x.Get())).AsQueryable().ProjectToType<NewsItemDto>(null).ToList();

    //    List <NewsItemDto> response = new List<NewsItemDto>();
        
    //    if(startDate != DateTime.MinValue || endDate != DateTime.MinValue)
    //    {
    //        response = list.Where(x => x.ProductionDate >= startDate && x.ProductionDate <= endDate).ToList();
    //    }

    //    if(string.IsNullOrEmpty(employee))
    //    {
    //        response = list.Where(x => x.Author == employee).ToList();
    //    }

    //    if(string.IsNullOrEmpty(dossier))
    //    {
    //        response = list.Where(x => x.Author == employee).ToList();
    //    }

    //    if (string.IsNullOrEmpty(status))
    //    {
    //        Status parseStatus = (Status)Enum.Parse(typeof(Status), status);
    //        response = list.Where(x => x.Status == parseStatus).ToList();
    //    }
        
    //    return this.Ok(response);
    //}
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteNewsItem(Guid id)
    {
        await this.grainService.DeleteGrain(id);
        return this.NoContent();
    }

    [HttpPatch]
    [Route("{id}")]
    public async Task<ActionResult<NewsItemDto>> UpdateNewsItem(Guid id, [FromBody] UpdateNewsItemRequest request)
    {
        TypeAdapterConfig<UpdateNewsItemRequest, NewsItemModel>
        .NewConfig()
        .Map(dest => dest.Id,
            src => id);

        var grain = await this.grainService.GetGrain(id);
        var update = request.Adapt<NewsItemModel>();
        var updatedGrain = await grain.Update(update);
        var response = updatedGrain.Adapt<NewsItemDto>();
        return this.Ok(response);
    }
}