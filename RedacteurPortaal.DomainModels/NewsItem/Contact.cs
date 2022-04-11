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

    public Guid Id { get; private set; }
    
    public string Name { get; private set; }
    
    public string Email { get; private set; }
    
    public string TelephoneNumber { get; private set; }
}