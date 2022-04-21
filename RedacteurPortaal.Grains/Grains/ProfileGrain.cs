using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.DomainModels.Profile;
using RedacteurPortaal.Grains.GrainInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.Grains.Grains;

public class ProfileGrain : Grain, IProfileGrain
{
    private readonly IPersistentState<Profile> profile;

    public ProfileGrain(
#if DEBUG
    // This works in testing.
    [PersistentState("profile")]
#else
    // This doesn't work in testing, but I don't know why.
    [PersistentState("profile", "OrleansStorage")]
#endif
        IPersistentState<Profile> profile)
    {
        this.profile = profile;
    }

        public Task<bool> HasState()
        {
            return Task.FromResult(this.profile.RecordExists);
        }

        public async Task Delete()
        {
            await this.profile.ClearStateAsync();
        }
    public async Task AddProfile(Profile profile)
    {
        this.profile.State = profile;
        await this.profile.WriteStateAsync();
    }
    

    public Task<Profile> Get()
    {
        return Task.FromResult(this.profile.State);
    }

    public Task<Profile> Update(ProfileUpdate profile)
    {
        this.profile.State.ProfilePicture = profile.ProfilePicture;
        this.profile.State.FullName = profile.Name;
        this.profile.State.ContactDetails = profile.ContactDetails;

        return Task.FromResult(this.profile.State);
    }
}
