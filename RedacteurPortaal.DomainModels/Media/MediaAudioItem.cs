using RedacteurPortaal.DomainModels.NewsItem;

namespace RedacteurPortaal.DomainModels.Media;

public class MediaAudioItem : MediaItem
{
    public MediaAudioItem(
        Guid guid, string title, string folder, DateTime republishDate, string rights, string camera, string lastWords, string proxyFile, string presentation, Location location, string format, Uri audioUrl,
        TimeSpan duration,
        Weather weather,
        string firstWords,
        string programName)
        : base(guid, title, folder, republishDate, rights, camera, lastWords, proxyFile, presentation, location, format, audioUrl)
    {
        this.Duration = duration;
        this.Weather = weather;
        this.FirstWords = firstWords ?? throw new ArgumentNullException(nameof(firstWords));
        this.ProgramName = programName ?? throw new ArgumentNullException(nameof(programName));
    }

    // Delete deze comments als je deze class wilt gebruiken.
    // ReSharper disable UnusedAutoPropertyAccessor.Global
    public TimeSpan Duration { get; }

    public Weather Weather { get; }

    public string FirstWords { get; }

    public string ProgramName { get; }

    // ReSharper restore UnusedAutoPropertyAccessor.Global
}