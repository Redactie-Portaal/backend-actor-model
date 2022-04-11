using RedacteurPortaal.DomainModels.NewsItem;

namespace RedacteurPortaal.DomainModels.Media;

public abstract class MediaItem
{
    public MediaItem()
    {
    }

    protected MediaItem(
        Guid id,
        string title,
        string folder,
        DateTime republishDate,
        string rights,
        string camera,
        string lastWords,
        string proxyFile,
        string presentation,
        Location location,
        string format,
        Uri mediaLocation)
    {
        this.Id = id;
        this.Title = title ?? throw new ArgumentNullException(nameof(title));
        this.Folder = folder ?? throw new ArgumentNullException(nameof(folder));
        this.RepublishDate = republishDate;
        this.Rights = rights ?? throw new ArgumentNullException(nameof(rights));
        this.Camera = camera ?? throw new ArgumentNullException(nameof(camera));
        this.LastWords = lastWords ?? throw new ArgumentNullException(nameof(lastWords));
        this.ProxyFile = proxyFile ?? throw new ArgumentNullException(nameof(proxyFile));
        this.Presentation = presentation ?? throw new ArgumentNullException(nameof(presentation));
        this.Location = location ?? throw new ArgumentNullException(nameof(location));
        this.Format = format ?? throw new ArgumentNullException(nameof(format));
        this.MediaLocation = mediaLocation ?? throw new ArgumentNullException(nameof(mediaLocation));
    }

    private Guid Id { get; }

    public string Title { get; }

    public string Folder { get; }

    public DateTime RepublishDate { get; }

    public string Rights { get; }

    public string Camera { get; }

    public string LastWords { get; }

    public string ProxyFile { get; }

    public string Presentation { get; }

    public Location Location { get; }

    public string Format { get; }

    public Uri MediaLocation { get; }
}