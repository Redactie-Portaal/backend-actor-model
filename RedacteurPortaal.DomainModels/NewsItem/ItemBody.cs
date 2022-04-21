namespace RedacteurPortaal.DomainModels.NewsItem;

public class ItemBody : IBaseEntity
{
    public ItemBody()
    {
    }

    public ItemBody(Guid id, string description, string shortDescription)
    {
        this.Id = id;
        this.Description = description ?? throw new ArgumentNullException(nameof(description));
        this.ShortDescription = shortDescription ?? throw new ArgumentNullException(nameof(shortDescription));
    }

    public Guid Id { get; set; }

    public string? Description { get; private set; }

    public string? ShortDescription { get; private set; }
}