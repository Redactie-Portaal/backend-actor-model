﻿using Mapster;
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
        private readonly ILogger<ProfileController> logger;

        public ProfileController(IGrainManagementService<IProfileGrain> grainService, ILogger<ProfileController> logger)
        {
            this.grainService = grainService;
            this.logger = logger;
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
                                                                            src.ContactDetails.Phone,
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
        public async Task<IActionResult> Get()
        {
            var profile = await this.grainService.GetGrains();
            return this.Ok(profile);
        }

        [HttpGet("{id}", Name = "GetProfile")]
        public async Task<ActionResult<ProfileDto>> Get(Guid id)
        {
            var profile = await this.grainService.GetGrain(id);
            var profileItem = await profile.Get();
            return this.Ok(profileItem.Adapt<ProfileDto>());
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<ProfileDto>> Patch(Guid id, PatchProfileRequest patch)
        {
            var profile = await this.grainService.GetGrain(id);

            TypeAdapterConfig<PatchProfileRequest, Profile>
            .NewConfig()
                .Map(dest => dest.FullName, src => src.Name)
                .Map(dest => dest.ContactDetails.PhoneNumber, src => src.ContactDetails.Phone);

            var profileUpdate = patch.Adapt<Profile>();
            profileUpdate.Id = id;

            var updatedProfile = await profile.Update(profileUpdate);

            return this.Ok(updatedProfile.Adapt<ProfileDto>());
        }
    }
}
