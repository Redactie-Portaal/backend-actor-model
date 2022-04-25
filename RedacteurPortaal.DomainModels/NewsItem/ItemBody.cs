namespace RedacteurPortaal.DomainModels.NewsItem;

public class ItemBody
{
    public ItemBody()
    {
    }

    public ItemBody(Guid guid, string description)
    {
        this.Guid = guid;
        this.Description = description ?? throw new ArgumentNullException(nameof(description));
    }

    public Guid? Guid { get; private set; }

    public string? Description { get; private set; }
}