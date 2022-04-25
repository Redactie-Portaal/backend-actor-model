using Mapster;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using RedacteurPortaal.Api.DTOs;
using RedacteurPortaal.Api.Models.Request;
using RedacteurPortaal.DomainModels.Profile;
using RedacteurPortaal.Grains.GrainInterfaces;
using RedacteurPortaal.Grains.GrainServices;

namespace RedacteurPortaal.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly IGrainManagementService<IProfileGrain> grainService;

        public ProfileController(IGrainManagementService<IProfileGrain> grainService)
        {
            this.grainService = grainService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProfile([FromBody] ProfileDto profile)
        {
            var grain = await this.grainService.CreateGrain(profile.Id);
            var updateProfile = profile.Adapt<Profile>();
            await grain.Update(updateProfile);
            return this.Ok(updateProfile);
        }

        // Get list of profiles
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var profile = await this.grainService.GetGrains();
            return this.Ok(profile);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var profile = await this.grainService.GetGrain(id);
            return this.Ok(await profile.Get());
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, PatchProfileRequest patch)
        {
            var profile = await this.grainService.GetGrain(id);

            TypeAdapterConfig<PatchProfileRequest, Profile>
            .NewConfig()
                .Map(dest => dest.FullName, src => src.Name)
                .Map(dest => dest.ProfilePicture, src => src.ProfilePicture)
                .Map(dest => dest.ContactDetails.Email, src => src.ContactDetails.Email)
                .Map(dest => dest.ContactDetails.PhoneNumber, src => src.ContactDetails.Phone)
                .Map(dest => dest.ContactDetails.Address, src => src.ContactDetails.Address)
                .Map(dest => dest.ContactDetails.Province, src => src.ContactDetails.Province)
                .Map(dest => dest.ContactDetails.City, src => src.ContactDetails.City)
                .Map(dest => dest.ContactDetails.PostalCode, src => src.ContactDetails.PostalCode);
            
            var profileUpdate = patch.Adapt<Profile>();
            profileUpdate.Id = id;

            await profile.Update(profileUpdate);

            return this.Ok(await profile.Get());
        }
    }
}
