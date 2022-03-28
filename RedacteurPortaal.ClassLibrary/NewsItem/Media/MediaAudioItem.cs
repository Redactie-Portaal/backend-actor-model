
namespace RedacteurPortaal.ClassLibrary.NewsItem.Media
{
    public class MediaAudioItem : MediaItem
    {
        TimeSpan Duration { get; set; }
        Weather Weather { get; set; }
        Location Location { get; set; }
        string FirstWords { get; set; }
        string ProgramName { get; set; }
        string Presentation { get; set; }

    }
}
