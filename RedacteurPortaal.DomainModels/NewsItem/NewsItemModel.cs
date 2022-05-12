using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.DomainModels.Shared;

namespace RedacteurPortaal.DomainModels.NewsItem;

public class NewsItemModel : IBaseEntity
{
    public NewsItemModel()
    {
    }

    public NewsItemModel(
        Guid id,
        string title,
        Status status,
        ApprovalState approvalState,
        string author,
        FeedSource source,
        string body,
        List<Contact> contactDetails,
        Location locationDetails,
        DateTime productionDate,
        DateTime endDate,
        Category category,
        Region region,
        string dossier,
        List<MediaVideoItem> videos,
        List<MediaAudioItem> audio,
        List<MediaPhotoItem> photos)
    {
        this.Id = id;
        this.Title = title ?? throw new ArgumentNullException(nameof(title));
        this.Status = status;
        this.ApprovalState = approvalState;
        this.Author = author ?? throw new ArgumentNullException(nameof(author));
        this.Source = source ?? throw new ArgumentNullException(nameof(source));
        this.Body = body ?? throw new ArgumentNullException(nameof(body));
        this.ContactDetails = contactDetails ?? throw new ArgumentNullException(nameof(contactDetails));
        this.LocationDetails = locationDetails ?? throw new ArgumentNullException(nameof(locationDetails));
        this.ProductionDate = productionDate;
        this.EndDate = endDate;
        this.Category = category;
        this.Region = region;
        this.Dossier = dossier ?? throw new ArgumentNullException(nameof(dossier));
        this.Videos = videos ?? throw new ArgumentNullException(nameof(videos));
        this.Audio = audio ?? throw new ArgumentNullException(nameof(audio));
        this.Photos = photos ?? throw new ArgumentNullException(nameof(photos));
    }

    public Guid Id { get; set; }

    public string Title { get; private set; }

    public Status Status { get; private set; }

    public ApprovalState ApprovalState { get; private set; }

    public string Author { get; private set; }

    public FeedSource Source { get; private set; }

    public string Body { get; private set; }

    public List<Contact> ContactDetails { get; private set; }

    public Location LocationDetails { get; private set; }

    public DateTime ProductionDate { get; private set; }

    public DateTime EndDate { get; private set; }

    public Category Category { get; private set; }

    public Region Region { get; private set; }

    public string Dossier { get; private set; }

    public List<MediaVideoItem> Videos { get; private set; }

    public List<MediaAudioItem> Audio { get; private set; }

    public List<MediaPhotoItem> Photos { get; private set; }

    public void SetLocationDetails(Location location)
    {
        this.LocationDetails = location;
    }

    public void SetContactDetails(List<Contact> contactDetails)
    {
        this.ContactDetails = contactDetails;
    }

    public void SetVideos(List<MediaVideoItem> videos)
    {
        this.Videos = videos;
    }

    public void SetAudio(List<MediaAudioItem> audio)
    {
        this.Audio = audio;
    }

    public void SetPhotos(List<MediaPhotoItem> photos)
    {
        this.Photos = photos;
    }
}