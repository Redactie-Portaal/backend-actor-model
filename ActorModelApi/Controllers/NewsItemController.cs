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
        private ILogger _logger;

        public NewsItemController(IClusterClient client, ILogger<NewsItemController> logger)
        {
            _client = client;
            _logger = logger;
        }

        [Route("/newsitem")]
        [HttpPost]
        public async Task<IActionResult> SaveNewsItem([FromBody] NewsItem newsitem)
        {
            try
            {
                var successMessage = "News item was created";
                var grain = _client.GetGrain<INewsItemGrain>(newsitem.Id);
                await grain.AddNewsItem(newsitem);
                _logger.LogInformation(successMessage);
                return StatusCode(201, successMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An internal server error has occured");
            }
        }

        [Route("/newsitem")]
        [HttpGet]
        public async Task<IActionResult> GetNewsItem(Guid guid)
        {
            try
            {
                var grain = _client.GetGrain<INewsItemGrain>(guid);
                var response = await grain.GetNewsItem();
                if(response.Title is not null)
                {
                    _logger.LogInformation("News item fetched successfully");
                    return Ok(response);
                }
                else
                {
                    _logger.LogInformation("News item not found");
                    return StatusCode(400, "News item not found");
                }
                
            }
            catch(Exception ex)
            {
                _logger?.LogError(ex.Message);
                return StatusCode(500, "An internal server error has occured");
            }
  
        }

        [Route("/newsitem")]
        [HttpDelete]
        public async Task<IActionResult> DeleteNewsItem(Guid guid)
        {
            try
            {
                var grain = _client.GetGrain<INewsItemGrain>(guid);
                await grain.DeleteNewsItem(guid);
                _logger.LogInformation("News item deleted successfully");
                return StatusCode(204, "News item deleted");
            }
            catch(Exception ex) {
                _logger?.LogError(ex.Message);
                return StatusCode(500, "An internal server error has occured");
            }
        }

        [Route("/newsitem")]
        [HttpPut]
        public async Task<IActionResult> UpdateNewsItem(string name, Guid guid)
        {
            try
            {
                var grain = _client.GetGrain<INewsItemGrain>(guid);
                await grain.UpdateNewsItem(name, guid);
                _logger.LogInformation("News item updated successfully");
                return StatusCode(204, "News item updated");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message);
                return StatusCode(500, "An internal server error has occured");
            }
  
        }

    }
}
