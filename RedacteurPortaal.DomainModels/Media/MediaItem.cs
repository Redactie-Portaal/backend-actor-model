namespace RedacteurPortaal.DomainModels.Media
{
    public class MediaItem
    {
        Guid Guid { get; set; }
        string Title { get; set; }
        string Folder { get; set; }
        DateTime RepublishDate { get; set; }
        string Rights { get; set; } 
        string Camera { get; set; }
        string LastWords { get; set; }
        string ProxyFile { get; set; }
        string Presentation { get; set; }
        Location Location { get; set; }
        string Format { get; set; }

    }
}
