using RedacteurPortaal.DomainModels.NewsItem;

namespace RedacteurPortaal.Grains.GrainInterfaces;

public interface IContactGrain : IManageableGrain<Contact>
{
    Task<Contact> Update(Contact contact);   
}
