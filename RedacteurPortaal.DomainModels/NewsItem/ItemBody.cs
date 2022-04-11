namespace RedacteurPortaal.DomainModels.NewsItem;

public class ItemBody
{
    public ItemBody()
    {
    }

    public ItemBody(Guid guid, string description, string shortDescription)
    {
        this.Guid = guid;
        this.Description = description ?? throw new ArgumentNullException(nameof(description));
        this.ShortDescription = shortDescription ?? throw new ArgumentNullException(nameof(shortDescription));
    }

    public Guid? Guid { get; private set; }

    public string? Description { get; private set; }

    public string? ShortDescription { get; private set; }
}