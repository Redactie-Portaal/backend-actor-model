using Mapster;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using RedacteurPortaal.Api.DTOs;
using RedacteurPortaal.Api.Models.Request;
using RedacteurPortaal.DomainModels.Adress;
using RedacteurPortaal.Grains.GrainInterfaces;
using RedacteurPortaal.Grains.GrainServices;
using RedacteurPortaal.Helpers;

namespace RedacteurPortaal.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : Controller
    {
        private readonly IGrainManagementService<IAddressGrain> grainService;
        private readonly ILogger logger;
        
        public AddressController(IGrainManagementService<IAddressGrain> grainService, ILogger<AddressController> logger)
        {
            this.grainService = grainService;    
            this.logger = logger;
        }

        public void MapGuid(Guid guid)
        {
            TypeAdapterConfig<AddressDTO, AddressModel>
             .NewConfig()
             .Map(dest => dest.Id,
                 src => guid);
        }

        [HttpPost]
        public async Task<ActionResult<AddressDTO>> SaveAddress([FromBody] AddAddressRequest addressDTO )   
        {
            Guid newguid = Guid.NewGuid();
            TypeAdapterConfig<AddressDTO, AddressModel>
                .NewConfig()
                .Map(dest => dest.Id,
                    src => newguid);
            
            var address = addressDTO.Adapt<AddressModel>();
            address.Id = newguid;
            const string successMessage = "Address was created";
            var grain = await this.grainService.CreateGrain(address.Id);
            await grain.UpdateAdress(address);

            var createdGrain = await this.grainService.GetGrain(newguid);
            var createdItem = await createdGrain.Get();
            var response = createdItem.Adapt<AddressDTO>();

            this.logger.LogInformation(successMessage);
            return this.CreatedAtRoute("GetAddress", new { id = newguid }, response);
        }

        [HttpGet]
        [Route("{id}", Name = "GetAddress")]
        public async Task<AddressDTO> GetAddress(Guid id)
        {
            var grain = await this.grainService.GetGrain(id);
            var response = await grain.Get();
            this.logger.LogInformation("Address fetched successfully");
            return response.Adapt<AddressDTO>();
        }

        [HttpGet]
        public async Task<List<AddressDTO>> Get()
        {
            var grain = await this.grainService.GetGrains();
            this.logger.LogInformation("Addresses fetched successfully");
            var addresses = await grain.SelectAsync(async x => await x.Get());
            return addresses.AsQueryable().ProjectToType<AddressDTO>().ToList();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAddress(Guid id)
        {
            await this.grainService.DeleteGrain(id);
            this.logger.LogInformation("Address deleted successfully");
            return this.NoContent();
        }

        [HttpPatch]
        public async Task<ActionResult<AddressDTO>> UpdateAddress(Guid id, [FromBody]UpdateAddressRequest addressDTO)
        {
            TypeAdapterConfig<UpdateAddressRequest, AddressModel>
            .NewConfig()
            .Map(dest => dest.Id,
                src => id);

            var address = addressDTO.Adapt<AddressModel>();
            var grain = await this.grainService.GetGrain(id);
            await grain.UpdateAdress(address);
            this.logger.LogInformation("Address updated succesfully");
            var updatedGrain = await this.grainService.GetGrain(address.Id);
            var updatedItem = await updatedGrain.Get();
            var response = updatedItem.Adapt<AddressModel>();
            return this.Ok(response);
        }
    }
}
