using Mapster;
using RedacteurPortaal.DomainModels.Media;

namespace RedacteurPortaal.DomainModels.NewsItem;
[AdaptTo("[name]Dto"), GenerateMapper]
public class NewsItemModel
{
    public NewsItemModel()
    {
    }

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
        MediaPhotoItem[] photos)
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
        this.Photos = photos ?? throw new ArgumentNullException(nameof(photos));
    }

    public Guid Id { get; set; } = Guid.NewGuid();

    public string Title { get; private set; }

    public Status Status { get; private set; }

    public string Author { get; private set; }

    public FeedSource Source { get; private set; }

    public ItemBody Body { get; private set; }

    public string ContactDetails { get; private set; }

    public string LocationDetails { get; private set; }

    public DateTime ProdutionDate { get; private set; }

    public DateTime EndDate { get; private set; }

    public Category Category { get; private set; }

    public Region Region { get; private set; }

    public MediaVideoItem Video { get; private set; }

    public MediaAudioItem Audio { get; private set; }

    public MediaPhotoItem[] Photos { get; private set; }
}