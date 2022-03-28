namespace RedacteurPortaal.DomainModels.Media
{
    public abstract class MediaItem
    {
        Guid Guid { get; }
        Guid NewsItemGuid { get; }
        string Title { get; }
        string Folder { get; }
        DateTime RepublishDate { get; }
        string Rights { get; }
        string Camera { get; }
        string LastWords { get; }
        string ProxyFile { get; }
        string Presentation { get; }
        Location Location { get; }
        string Format { get; }

        public MediaItem(
            Guid guid,
            Guid newsItemGuid,
            string title,
            string folder,
            DateTime republishDate,
            string rights,
            string camera,
            string lastWords,
            string proxyFile,
            string presentation,
            Location location,
            string format
            )
        {
            Guid = guid;
            NewsItemGuid = newsItemGuid;
            Title = title;
            Folder = folder;
            RepublishDate = republishDate;
            Rights = rights;
            Camera = camera;
            LastWords = lastWords;
            ProxyFile = proxyFile;
            Presentation = presentation;
            Location = location;
            Format = format;
        }
    }
}