using Microsoft.AspNetCore.Mvc;
using Orleans;
using RedacteurPortaal.Api.DTOs;

namespace RedacteurPortaal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArchiveController : Controller
{
    private readonly IClusterClient client;
    private readonly ILogger logger;

    public ArchiveController(IClusterClient client, ILogger<ArchiveController> logger)
    {
        this.client = client;
        this.logger = logger;
    }
}