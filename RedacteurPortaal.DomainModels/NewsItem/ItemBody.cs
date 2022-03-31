namespace RedacteurPortaal.DomainModels.NewsItem;

public class ItemBody
{
    public ItemBody(Guid guid, string description, string shortDescription)
    {
        Guid = guid;
        Description = description ?? throw new ArgumentNullException(nameof(description));
        ShortDescription = shortDescription ?? throw new ArgumentNullException(nameof(shortDescription));
    }

    public Guid Guid { get; }

    public string Description { get; }

    public string ShortDescription { get; }
}