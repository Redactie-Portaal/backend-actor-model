using Microsoft.AspNetCore.Mvc;
using Orleans;
using RedacteurPortaal.Grains;

namespace RedacteurPortaal.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        private IClusterClient _client;
        private ILogger _logger;

        public TestController(IClusterClient client, ILogger<TestController> logger)
        {
            _client = client;
            _logger = logger;
        }

        [Route("")]
        [HttpGet]
        public async Task<IActionResult> Test()
        {
            var guid = Guid.NewGuid();


            var grain = _client.GetGrain<ITestGrain>(guid);
            var res = await grain.Test();
            _logger.LogInformation(res);
            return Ok(res);
        }

    }
}
