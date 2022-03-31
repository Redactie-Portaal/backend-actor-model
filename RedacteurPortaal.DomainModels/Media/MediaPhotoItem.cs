namespace RedacteurPortaal.DomainModels.Media;

public class MediaPhotoItem : MediaItem
{
    public MediaPhotoItem(Guid guid, string title, string folder, DateTime republishDate, string rights, string camera, string lastWords, string proxyFile, string presentation, Location location, string format, string image)
            : base(guid, title, folder, republishDate, rights, camera, lastWords, proxyFile, presentation, location, format)
    {
        this.Image = image;
    }

    public string Image { get; }
}