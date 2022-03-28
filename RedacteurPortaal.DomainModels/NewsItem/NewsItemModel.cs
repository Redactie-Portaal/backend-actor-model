using RedacteurPortaal.DomainModels.Media;

namespace RedacteurPortaal.DomainModels.NewsItem;

public class NewsItemModel
{
    public NewsItemModel(
        string title,
        Status status,
        string author,
        FeedSource source,
        ItemBody body,
        string contactDetails,
        string locationDetails,
        DateTime produtionDate,
        DateTime endDate,
        Category category,
        Region region,
        MediaVideoItem video,
        MediaAudioItem audio,
        MediaPhotoItem photo)
    {
        this.Title = title ?? throw new ArgumentNullException(nameof(title));
        this.Status = status;
        this.Author = author ?? throw new ArgumentNullException(nameof(author));
        this.Source = source ?? throw new ArgumentNullException(nameof(source));
        this.Body = body ?? throw new ArgumentNullException(nameof(body));
        this.ContactDetails = contactDetails ?? throw new ArgumentNullException(nameof(contactDetails));
        this.LocationDetails = locationDetails ?? throw new ArgumentNullException(nameof(locationDetails));
        this.ProdutionDate = produtionDate;
        this.EndDate = endDate;
        this.Category = category;
        this.Region = region;
        this.Video = video ?? throw new ArgumentNullException(nameof(video));
        this.Audio = audio ?? throw new ArgumentNullException(nameof(audio));
        this.Photo = photo ?? throw new ArgumentNullException(nameof(photo));
    }

    public Guid Id { get; set; } = Guid.NewGuid();

    public string Title { get; }

    public Status Status { get; }

    public string Author { get; }

    public FeedSource Source { get; }

    public ItemBody Body { get; }

    public string ContactDetails { get; }

    public string LocationDetails { get; }

    public DateTime ProdutionDate { get; }

    public DateTime EndDate { get; }

    public Category Category { get; }

    public Region Region { get; }

    public MediaVideoItem Video { get; }

    public MediaAudioItem Audio { get; }

    public MediaPhotoItem Photo { get; }
}