using Orleans;
using RedacteurPortaal.DomainModels.NewsItem;

namespace RedacteurPortaal.Grains.GrainInterfaces
{
    public interface IContactGrain : IManageableGrain<Contact>
    { 
        Task Update(Contact contact);
    }
}