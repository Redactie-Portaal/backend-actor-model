using Orleans;
using RedacteurPortaal.DomainModels.NewsItem;

namespace RedacteurPortaal.Grains.GrainInterfaces
{
    public interface IContactGrain : IManageableGrain<Contact>
    {
        Task AddContact(Contact contact);

        Task Update(Contact contact);
    }
}