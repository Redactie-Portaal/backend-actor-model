using FluentValidation;
using RedacteurPortaal.DomainModels.Validation.NewsItem;

namespace RedacteurPortaal.DomainModels.NewsItem;

public class Contact
{
    public Contact(Guid id, string name, string email, string telephoneNumber)
    {
        this.Id = id;
        this.Name = name;
        this.Email = email;
        this.TelephoneNumber = telephoneNumber;

        new ContactValidator().ValidateAndThrow(this);
    }

    public Guid Id { get; private set; }
    
    public string Name { get; private set; }
    
    public string Email { get; private set; }
    
    public string TelephoneNumber { get; private set; }
}