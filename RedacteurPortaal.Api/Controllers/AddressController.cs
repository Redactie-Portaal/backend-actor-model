using Mapster;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using RedacteurPortaal.Api.DTOs;
using RedacteurPortaal.Api.Models.Request;
using RedacteurPortaal.DomainModels.Adress;
using RedacteurPortaal.Grains.GrainInterfaces;
using RedacteurPortaal.Grains.GrainServices;

namespace RedacteurPortaal.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : Controller
    {
        private IGrainManagementService<IAddressGrain> grainService;
        private ILogger logger;

        public AddressController(IGrainManagementService<IAddressGrain> grainService, ILogger<AddressController> logger)
        {
            this.grainService = grainService;    
            this.logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> SaveAddress([FromBody] AddAddressRequest addressDTO )   
        {
            var newguid = Guid.NewGuid();
            TypeAdapterConfig<AddressDTO, AddressModel>
                .NewConfig()
                .Map(dest => dest.Id,
                    src => newguid);

            var address = addressDTO.Adapt<AddressModel>();
            address.Id = newguid;
            const string successMessage = "Address was created";
            var grain = await this.grainService.GetGrain(address.Id);
            await grain.UpdateAdress(address);
            this.logger.LogInformation(successMessage);
            return this.CreatedAtRoute("GetAddress", new { id = newguid }, address);
        }

        [HttpGet]
        [Route("{id}", Name = "GetAddress")]
        public async Task<IActionResult> GetAddress(Guid id)
        {
            var grain = await this.grainService.GetGrain(id);
            var response = await grain.Get();
            this.logger.LogInformation("Address fetched successfully");
            return this.Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var grain = await this.grainService.GetGrains();
            this.logger.LogInformation("Addresses fetched successfully");
            return this.Ok(grain.Select(x => x.Get()));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAddress(Guid id)
        {
            await this.grainService.DeleteGrain(id);
            this.logger.LogInformation("Address deleted successfully");
            return this.Ok("Address deleted");
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateAddress(Guid guid,[FromBody]AddressDTO addressDTO)
        {
            var address = addressDTO.Adapt<AddressModel>();
            var grain = await this.grainService.GetGrain(guid);
            await grain.UpdateAdress(address);
            this.logger.LogInformation("Address updated succesfully");
            return this.StatusCode(204, "Address updated");
        }

            //_logger.LogInformation("News item not found");
            //return StatusCode(400, "News item not found");
            //return StatusCode(500, "An internal server error has occured");
    }
}
