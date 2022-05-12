using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.DomainModels.Profile;
using RedacteurPortaal.Grains.GrainInterfaces;

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

    public async Task<Profile> Update(Profile profile)
    {
        this.profile.State = profile;
        await this.profile.WriteStateAsync();
        return this.profile.State;
    }
}
