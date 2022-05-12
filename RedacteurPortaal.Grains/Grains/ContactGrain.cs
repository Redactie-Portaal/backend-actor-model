using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.Grains.GrainInterfaces;

namespace RedacteurPortaal.Grains.Grains;

public class ContactGrain : Grain, IContactGrain
{
    private readonly IPersistentState<Contact> contactState;

    public ContactGrain(
    [PersistentState("contact", "OrleansStorage")]
    IPersistentState<Contact> contact)
    {
        this.contactState = contact;
    }

    public Task<bool> HasState()
    {
        return Task.FromResult(this.contactState.RecordExists);
    }

    public Task<Contact> Get()
    {
        var state = this.contactState.State;
        state.Id = this.GetGrainIdentity().PrimaryKey;
        return Task.FromResult(state);
    }

    public async Task Delete()
    {
        await this.contactState.ClearStateAsync();
    }

    public async Task<Contact> Update(Contact contact)
    {
        this.contactState.State = contact;
        await this.contactState.WriteStateAsync();
        return await this.Get();
    }
}