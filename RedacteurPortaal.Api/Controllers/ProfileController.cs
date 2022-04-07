using Microsoft.AspNetCore.Mvc;
using Orleans;
using RedacteurPortaal.Api.Models.Request;
using RedacteurPortaal.DomainModels.Profile;
using RedacteurPortaal.Grains.GrainInterfaces;
using RedacteurPortaal.Grains.GrainServices;

namespace RedacteurPortaal.Api.Controllers
{
    [ApiController]
    [Route("api/Profiles")]
    public class ProfileController : ControllerBase
    {
        private readonly IGrainManagementService<IProfileGrain> grainService;

        public ProfileController(IGrainManagementService<IProfileGrain> grainService)
        {
            this.grainService = grainService;
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
            return this.Ok(profile.Get());
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, PatchProfileRequest patch)
        {
            var profile = await this.grainService.GetGrain(id);
            var update = new ProfileUpdate(
                patch.Name,
                patch.ProfilePicture,
                new ContactDetails(
                    patch.ContactDetails.Email,
                    patch.ContactDetails.Phone,
                    patch.ContactDetails.Address,
                    patch.ContactDetails.Province,
                    patch.ContactDetails.City,
                    patch.ContactDetails.PostalCode));

            await profile.Update(update);

            return this.Ok(profile.Get());
        }
    }
}
