using GrainInterfaces;
using Microsoft.AspNetCore.Mvc;
using Orleans;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuidController : Controller
    {
        private IClusterClient _client;
        private ILogger _logger;

        public GuidController(IClusterClient client, ILogger<GuidController> logger)
        {
            _client = client;
            _logger = logger;
        }

        [Route("/guid")]
        [HttpGet]
        public async Task<IActionResult> GetHello(string greeting)
        {
            Guid guid = Guid.NewGuid();
            var grain = _client.GetGrain<IGuidGrain1>(guid);
            var response = grain.DoGrain(greeting, guid);
            //_logger.LogInformation(guid.ToString());
            return Ok("");
        }

        [Route("/newsitem")]
        [HttpPost]
        public async Task<IActionResult> SaveNewsItem(string newsitemname)
        {
            Guid newGuid = Guid.NewGuid();
            var grain = _client.GetGrain<INewsItemGrain>(newGuid);
            var response = grain.AddNewsItem(newsitemname, newGuid);
            return Ok(newGuid);
        }

        [Route("/newsitem")]
        [HttpGet]
        public async Task<IActionResult> GetNewsItem(Guid guid)
        {
            var grain = _client.GetGrain<INewsItemGrain>(guid);
            var response = await grain.GetNewsItem();
            return Ok($"The guid was:{response.Id}, while the name is {response.Name}");
        }
    }
}
