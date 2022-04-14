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
        private IClusterClient client;
        private ILogger logger;

        public AddressController(IClusterClient client, ILogger<AddressController> logger)
        {
            this.client = client;
            this.logger = logger;
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
            var grain = this.client.GetGrain<IAddressGrain>(addressDTO.Id);
            var address = addressDTO.Adapt<AddressModel>();
            await grain.AddAdress(address);
            this.logger.LogInformation(successMessage);
            return this.StatusCode(201, successMessage);
        }


        [HttpGet]
        [Route("{id}")]
        public IActionResult GetAddress(Guid guid)
        {
            var grain = this.client.GetGrain<IAddressGrain>(guid);
            var response = grain.GetAddress(guid);
            this.logger.LogInformation("Address fetched successfully");
            return this.Ok(response);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAddress(Guid guid)
        {
            var grain = this.client.GetGrain<IAddressGrain>(guid);
            await grain.RemoveAdress(guid);
            this.logger.LogInformation("Address deleted successfully");
            return this.StatusCode(204, "Address deleted");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAddress([FromBody]AddressDTO addressDTO)
        {
            var address = addressDTO.Adapt<AddressModel>();
            var grain = this.client.GetGrain<IAddressGrain>(address.Id);
            await grain.UpdateAdress(address);
            this.logger.LogInformation("Address updated succesfully");
            return this.StatusCode(204, "Address updated");
        }

            //_logger.LogInformation("News item not found");
            //return StatusCode(400, "News item not found");
            //return StatusCode(500, "An internal server error has occured");
    }
}
