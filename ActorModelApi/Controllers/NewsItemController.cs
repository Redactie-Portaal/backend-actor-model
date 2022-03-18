using GrainInterfaces;
using Microsoft.AspNetCore.Mvc;
using Orleans;

namespace ActorModelApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsItemController : Controller
    {
        private IClusterClient _client;
        private ILogger _logger;

        public NewsItemController(IClusterClient client) => _client = client;

        [Route("/newsitem")]
        [HttpPost]
        public async Task<IActionResult> SaveNewsItem(string newsitemname)
        {
            var newGuid = Guid.NewGuid();
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
