using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.Grains.GrainInterfaces;

namespace RedacteurPortaal.Grains.Grains;

public class ContactGrain : Grain, IContactGrain
{
    private readonly IPersistentState<Contact> contactState;

    public ContactGrain([PersistentState("contact", "OrleansStorage")] IPersistentState<Contact> contact)
    {
        this.contactState = contact;
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