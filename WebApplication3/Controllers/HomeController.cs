using GrainInterfaces;
using Microsoft.AspNetCore.Mvc;
using Orleans;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private IClusterClient _client;

        public HomeController(IClusterClient client) => _client = client;

        [Route("/hello")]
        [HttpGet]
        public async Task<ActionResult> GetHello(string abc)
        {
            var grain = _client.GetGrain<IHello>(0);
            var response = await grain.SayHello("Good morning, HelloGrain!");
            return Ok(response);    
        }

        [Route("/bye")]
        [HttpGet]
        public async Task<ActionResult> GetBye([FromBody] SomeObject ob)
        {
            //var grain = _client.GetGrain<IBye>(0);
            //var response = await grain.SayBye("");
            var grain = _client.GetGrain<IHello>(1);
            var response = await grain.SayHello("Good morning, HelloGrain!");
            return Ok(response);
        }

        [Route("/test")]
        [HttpGet]
        public async Task<ActionResult> GetTest()
        {
            var grain = _client.GetGrain<IHello>(2);
            var response = await grain.SayHello("Good morning, HelloGrain!");
            return Ok(response);
        }
    }
}
