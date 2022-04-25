using Microsoft.AspNetCore.Mvc;
using Orleans;
using RedacteurPortaal.DomainModels.Archive;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.Api.DTOs;
using RedacteurPortaal.Grains.GrainInterfaces;
using RedacteurPortaal.Grains.GrainServices;
using RedacteurPortaal.Api.Models.Request;

namespace RedacteurPortaal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArchiveController : Controller
{
    private readonly ILogger logger;
    private readonly IGrainManagementService<IArchiveGrain> grainService;

    public ArchiveController(IGrainManagementService<IArchiveGrain> grainService, ILogger<ArchiveController> logger)
    {
        this.grainService = grainService;
        this.logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var archives = await this.grainService.GetGrains();
        return this.Ok(archives);
    }

    [HttpGet]
    [Route(":id")]
    public async Task<IActionResult> Get(Guid guid)
    {
        var archive = await this.grainService.GetGrain(guid);
        this.logger.LogInformation("Archive fetched successfully");
        return this.Ok(archive.Get());
    }


    [HttpPost]
    public async Task<IActionResult> AddVideoItem(Guid guid, UpdateArchiveRequest patch)
    {
        var grain = await this.grainService.GetGrain(guid);
        var archiveUpdate = new ArchiveUpdate(
            patch.Label);

        await grain.Update(archiveUpdate);

        return this.Ok(grain.Get());
    }

}