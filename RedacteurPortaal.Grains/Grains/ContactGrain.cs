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

    public Task GetContactDetails(Guid guid)
    {
        return Task.FromResult(this.contactState.State);
    }

    public async Task AddContactDetails(Contact contact)
    {
        this.contactState.State = contact;
        await this.contactState.WriteStateAsync();
    }

    public async Task DeleteContactDetails(Guid guid)
    {
        await this.contactState.ClearStateAsync();
    }

    public async Task UpdateContactDetails(Contact contact)
    {
        this.contactState.State = contact;
        await this.contactState.WriteStateAsync();
    }
}