namespace RedacteurPortaal.DomainModels.Media;

public abstract class MediaItem
{
    private Guid Guid { get; set; }

    protected MediaItem(Guid guid, string title, string folder, DateTime republishDate, string rights, string camera, string lastWords, string proxyFile, string presentation, Location location, string format)
    {
        this.Guid = guid;
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
    }

    private string Title { get; set; }

    private string Folder { get; set; }

    private DateTime RepublishDate { get; set; }

    private string Rights { get; set; }

    private string Camera { get; set; }

    private string LastWords { get; set; }

    private string ProxyFile { get; set; }

    private string Presentation { get; set; }

    private Location Location { get; set; }

    private string Format { get; set; }
}