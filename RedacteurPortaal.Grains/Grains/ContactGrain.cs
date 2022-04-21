using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.Grains.GrainInterfaces;

namespace RedacteurPortaal.Grains.Grains;

public class ContactGrain : Grain, IContactGrain
{
    private readonly IPersistentState<Contact> contactState;

    public ContactGrain(
#if DEBUG
        // This works in testing.
        [PersistentState("contact")]
#else
        // This doesn't work in testing, but I don't know why.
        [PersistentState("contact", "OrleansStorage")]
#endif        
        IPersistentState<Contact> contact)
    {
        this.contactState = contact;
    }

    public Task<bool> HasState()
    {
        return Task.FromResult(this.contactState.RecordExists);
    }

    public async Task AddContact(Contact contact)
    {
        this.contactState.State = contact;
        await this.contactState.WriteStateAsync();
    }

    public Task<Contact> Get()
    {
        return Task.FromResult(this.contactState.State);
    }

    public async Task Delete()
    {
        await this.contactState.ClearStateAsync();
    }

    public async Task Update(Contact contact)
    {
        this.contactState.State = contact;
        await this.contactState.WriteStateAsync();
    }
}