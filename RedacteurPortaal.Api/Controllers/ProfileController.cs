using Mapster;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<ProfileDto>> CreateProfile([FromBody] AddProfileRequest profile)
        {
            var guid = Guid.NewGuid();
            var grain = await this.grainService.CreateGrain(guid);

            TypeAdapterConfig<AddProfileRequest, Profile>
            .NewConfig()
                .Map(dest => dest.FullName, src => src.FullName)
                .Map(dest => dest.ProfilePicture, src => src.ProfilePicture)
                .Map(dest => dest.ContactDetails, src => new ContactDetails(src.ContactDetails.Email,
                                                                            src.ContactDetails.PhoneNumber,
                                                                            src.ContactDetails.Address,
                                                                            src.ContactDetails.Province,
                                                                            src.ContactDetails.City,
                                                                            src.ContactDetails.PostalCode));

            var updateProfile = profile.Adapt<Profile>();
            updateProfile.Id = guid;
            var returnProfile = await grain.Update(updateProfile);
            return this.CreatedAtRoute("GetProfile", new { id = guid }, returnProfile.Adapt<ProfileDto>());
        }

        [HttpGet]
        public async Task<ActionResult<List<ProfileDto>>> Get()
        {
            var profile = await this.grainService.GetGrains();
            return this.Ok(profile.AsQueryable().ProjectToType<ProfileDto>().ToList());
        }

        [HttpGet("{id}", Name = "GetProfile")]
        public async Task<ActionResult<ProfileDto>> Get(Guid id)
        {
            var profile = await this.grainService.GetGrain(id);
            var profileItem = await profile.Get();
            return this.Ok(profileItem.Adapt<ProfileDto>());
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] PatchProfileRequest patch)
        {
            var profile = await this.grainService.GetGrain(id);

            TypeAdapterConfig<PatchProfileRequest, Profile>
            .NewConfig()
                .Map(dest => dest.FullName, src => src.Name)
                .Map(dest => dest.ProfilePicture, src => src.ProfilePicture)
                .Map(dest => dest.ContactDetails.Email, src => src.ContactDetails.Email)
                .Map(dest => dest.ContactDetails.PhoneNumber, src => src.ContactDetails.PhoneNumber)
                .Map(dest => dest.ContactDetails.Address, src => src.ContactDetails.Address)
                .Map(dest => dest.ContactDetails.Province, src => src.ContactDetails.Province)
                .Map(dest => dest.ContactDetails.City, src => src.ContactDetails.City)
                .Map(dest => dest.ContactDetails.PostalCode, src => src.ContactDetails.PostalCode);

            var profileUpdate = patch.Adapt<Profile>();
            profileUpdate.Id = id;

            var updatedProfile = await profile.Update(profileUpdate);

            return this.Ok(updatedProfile.Adapt<ProfileDto>());
        }
    }
}