namespace RedacteurPortaal.DomainModels.NewsItem;

public class Source : IBaseEntity
{
    public Source()
    {
    }

    public Source(Guid id, string name, string uri, DateTime date)
    {
        this.Id = id;
        this.Name = name ?? throw new ArgumentNullException(nameof(name));
        this.Uri = uri ?? throw new ArgumentNullException(nameof(uri));
        this.Date = date;
    }

    public Guid Id { get; set; }

    public string Name { get; private set; }
    
    public string Uri { get; private set; }

    public DateTime Date { get; private set; }
}
