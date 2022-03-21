using GrainInterfaces;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using ClassLibrary;

namespace ActorModelApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsItemController : Controller
    {
        private IClusterClient _client;
        private readonly ILogger _logger;

        public NewsItemController(IClusterClient client, ILogger<NewsItemController> logger)
        {
            _client = client;
            _logger = logger;
        }

        [Route("/newsitem")]
        [HttpPost]
        public IActionResult SaveNewsItem([FromBody] NewsItem newsitem)
        {
            var grain = _client.GetGrain<INewsItemGrain>(newsitem.Id);
            grain.AddNewsItem(newsitem);
            _logger.LogInformation("News Item Created");
            return Ok(newsitem.Id);
        }

        [Route("/newsitem")]
        [HttpGet]
        public async Task<IActionResult> GetNewsItem(Guid guid)
        {
            var grain = _client.GetGrain<INewsItemGrain>(guid);
            var response = await grain.GetNewsItem();
            return Ok($"The guid was:{response.Id}, while the name is {response.Title}");
        }

        [Route("/newsitem")]
        [HttpDelete]
        public IActionResult DeleteNewsItem(Guid guid)
        {
            var grain = _client.GetGrain<INewsItemGrain>(guid);
            var response = grain.DeleteNewsItem(guid);
            return Ok($"Item with id: {response.Id} was deleted from the datastore");
        }

    }
}
