using Microsoft.AspNetCore.Mvc;
using Orleans;
using RedacteurPortaal.DomainModels.Adress;
using RedacteurPortaal.Grains.GrainInterfaces;

namespace RedacteurPortaal.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : Controller
    {
        private IClusterClient _client;
        private ILogger _logger;

        public AddressController(IClusterClient client, ILogger<AddressController> logger)
        {
            _client = client;
            _logger = logger;
        }

        [Route("/address")]
        [HttpPost]
        public async Task<IActionResult> SaveAddress([FromBody] AddressModel address )
        {
            if (address == null) { return StatusCode(404, "Address is empty"); }
            else
            {
                var newguid = Guid.NewGuid();
                address.Id = newguid;
                var successMessage = "Address is saved";
                var grain = _client.GetGrain<IAddressGrain>(address.Id);
                await grain.AddAdress(address);
                _logger.LogInformation(successMessage);
                return StatusCode(201, successMessage);
            }
        }

        [Route("/address")]
        [HttpGet]
        public async Task<IActionResult> GetAddress(Guid guid)
        {
                
                var grain = _client.GetGrain<IAddressGrain>(guid);
                var response = await grain.GetAddress(guid);
                if (response is not null)
                {
                    _logger.LogInformation("Address fetched successfully");
                    return Ok(response);
                }
                else
                {
                    _logger.LogInformation("Address not found");
                    return StatusCode(400, "Address not found");
                }
        }

        [Route("/address")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAddress(Guid guid)
        {
            var grain = _client.GetGrain<IAddressGrain>(guid);
            var deleted = await grain.RemoveAdress(guid);
            if (deleted)
            {
                _logger.LogInformation("Address deleted successfully");
                return StatusCode(204, "Address deleted");
            }
            return StatusCode(400, "Address is not deleted");


        }

        [Route("/address")]
        [HttpPut]
        public async Task<IActionResult> UpdateAddress([FromBody]AddressModel address)
        {
            var grain = _client.GetGrain<IAddressGrain>(address.Id);
            await grain.UpdateAdress(address);
            _logger.LogInformation("Address updated succesfully");
            return StatusCode(204, "Address updated");
        }

            //_logger.LogInformation("News item not found");
            //return StatusCode(400, "News item not found");
            //return StatusCode(500, "An internal server error has occured");
    }
}
