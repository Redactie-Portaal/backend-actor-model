using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.DomainModels.Shared;
using RedacteurPortaal.DomainModels.Validation.Shared;

namespace RedacteurPortaal.DomainModels.Media;

public abstract class MediaItem : IBaseEntity
{
    protected MediaItem()
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

    public Guid Id { get; set; }

    public string Title { get; private set; }

    public string Folder { get; private set; }

    public DateTime RepublishDate { get; private set; }

    public string Rights { get; private set; }

    public string Camera { get; private set; }

    public string LastWords { get; private set; }

    public string ProxyFile { get; private set; }

    public string Presentation { get; private set; }

    public Location Location { get; private set; }
  
    public string Format { get; private set; }

    public Uri MediaLocation { get; private set; }
}