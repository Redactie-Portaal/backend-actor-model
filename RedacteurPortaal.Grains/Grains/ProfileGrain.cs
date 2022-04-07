using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.DomainModels.Profile;
using RedacteurPortaal.Grains.GrainInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.Grains.Grains
{
    public class ProfileGrain : Grain, IProfileGrain
    {
        private readonly IPersistentState<Profile> profile;

        public ProfileGrain([PersistentState("profile", "OrleansStorage")]
        IPersistentState<Profile> profile)
        {
            this.profile = profile;
        }

        public async Task Delete()
        {
            await this.profile.ClearStateAsync();
        }

        public Task<Profile> Get()
        {
            return Task.FromResult(this.profile.State);
        }

        public Task<Profile> Update(ProfileUpdate profile)
        {
            return Task.FromResult(this.profile.State);
        }
    }
}
