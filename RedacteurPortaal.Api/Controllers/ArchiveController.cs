using Microsoft.AspNetCore.Mvc;
using Orleans;
using RedacteurPortaal.DomainModels.Archive;
using RedacteurPortaal.Api.DTOs;

namespace RedacteurPortaal.Api.Controllers;

[ApiController]
[Route("api/archive")]
public class ArchiveController : Controller
{
    private readonly IClusterClient client;
    private readonly ILogger logger;

    public ArchiveController(IClusterClient client, ILogger logger)
    {
        this.client = client;
        this.logger = logger;
    }
}