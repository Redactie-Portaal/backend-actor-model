using Mapster;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using RedacteurPortaal.Api.DTOs;
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
            this._client = client;
            this._logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> SaveAddress([FromBody] AddressDTO addressDTO )   
        {
            var newguid = Guid.NewGuid();
            TypeAdapterConfig<AddressDTO, AddressModel>
                .NewConfig()
                .Map(dest => dest.Id,
                    src => newguid);

            addressDTO.Id = newguid;
            var successMessage = "Address is saved";
            var grain = _client.GetGrain<IAddressGrain>(addressDTO.Id);
            var address = addressDTO.Adapt<AddressModel>();
            await grain.AddAdress(address);
            this._logger.LogInformation(successMessage);
            return StatusCode(201, successMessage);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAddress(Guid guid)
        {
                var grain = _client.GetGrain<IAddressGrain>(guid);
                var response = await grain.GetAddress(guid);
                _logger.LogInformation("Address fetched successfully");
                return Ok(response);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAddress(Guid guid)
        {
            var grain = _client.GetGrain<IAddressGrain>(guid);
            await grain.RemoveAdress(guid);
            this._logger.LogInformation("Address deleted successfully");
            return StatusCode(204, "Address deleted");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAddress([FromBody]AddressDTO addressDTO)
        {
            var address = addressDTO.Adapt<AddressModel>();
            var grain = this._client.GetGrain<IAddressGrain>(address.Id);
            await grain.UpdateAdress(address);
            this._logger.LogInformation("Address updated succesfully");
            return StatusCode(204, "Address updated");
        }

            //_logger.LogInformation("News item not found");
            //return StatusCode(400, "News item not found");
            //return StatusCode(500, "An internal server error has occured");
    }
}
