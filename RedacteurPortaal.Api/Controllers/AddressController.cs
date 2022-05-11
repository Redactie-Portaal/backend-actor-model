using Mapster;
using Microsoft.AspNetCore.Mvc;
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

        public AddressController(IGrainManagementService<IAddressGrain> grainService)
        {
            this.grainService = grainService;    
        }

        [HttpPost]
        public async Task<ActionResult<AddressDTO>> SaveAddress([FromBody] AddAddressRequest addressDTO )   
        {
            Guid newguid = Guid.NewGuid();

            var address = addressDTO.Adapt<AddressModel>();
            var grain = await this.grainService.CreateGrain(newguid);
            var updatedGrain = await grain.UpdateAddress(address);
            var response = updatedGrain.Adapt<AddressDTO>();
            return this.CreatedAtRoute("GetAddress", new { id = newguid }, response);
        }

        [HttpGet]
        [Route("{id}", Name = "GetAddress")]
        public async Task<AddressDTO> GetAddress(Guid id)
        {
            var grain = await this.grainService.GetGrain(id);
            var response = await grain.Get();
            return response.Adapt<AddressDTO>();
        }

        [HttpGet]
        public async Task<List<AddressDTO>> Get()
        {
            var grain = await this.grainService.GetGrains();
            var addresses = await grain.SelectAsync(async x => await x.Get());
            return addresses.AsQueryable().ProjectToType<AddressDTO>().ToList();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAddress(Guid id)
        {
            await this.grainService.DeleteGrain(id);
            return this.NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<AddressDTO>> UpdateAddress(Guid id, [FromBody]UpdateAddressRequest addressDTO)
        {
            var grain = await this.grainService.GetGrain(guid);
            var address = addressDTO.Adapt<AddressModel>();
            var updatedAddress = await grain.UpdateAddress(address);
            this.logger.LogInformation("Address updated succesfully");

            return this.Ok(updatedAddress);
        }
    }
}