namespace RedacteurPortaal.DomainModels.NewsItem;

public class Contact
{
    public Contact(Guid id, string name, string email, string telephoneNumber)
    {
        this.Id = id;
        this.Name = name;
        this.Email = email;
        this.TelephoneNumber = telephoneNumber;
    }

    public Guid Id { get; }
    
    public string Name { get; }
    
    public string Email { get; }
    
    public string TelephoneNumber { get; }
}