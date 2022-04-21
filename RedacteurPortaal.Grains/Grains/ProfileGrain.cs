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
    [PersistentState("profile", "OrleansStorage")]
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

    public Task<Profile> Get()
    {
        return Task.FromResult(this.profile.State);
    }

    public async Task Update(Profile profile)
    {
        this.profile.State = profile;
        await this.profile.WriteStateAsync();
    }
}
