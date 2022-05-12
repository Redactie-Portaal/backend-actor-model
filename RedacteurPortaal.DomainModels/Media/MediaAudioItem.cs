using RedacteurPortaal.DomainModels.Shared;

namespace RedacteurPortaal.DomainModels.Media;

public class MediaAudioItem : MediaItem
{
    public MediaAudioItem()
    {
    }

    public MediaAudioItem(
        Guid guid,
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
        Uri audioUrl,
        TimeSpan duration,
        Weather weather,
        string firstWords,
        string programName)
        : base(
            guid,
            title,
            folder,
            republishDate,
            rights,
            camera,
            lastWords,
            proxyFile,
            presentation,
            location,
            format,
            audioUrl)
    {
        this.Duration = duration;
        this.Weather = weather;
        this.FirstWords = firstWords ?? throw new ArgumentNullException(nameof(firstWords));
        this.ProgramName = programName ?? throw new ArgumentNullException(nameof(programName));
    }

    // Delete deze comments als je deze class wilt gebruiken.
    // ReSharper disable UnusedAutoPropertyAccessor.Global
    public TimeSpan Duration { get; private set; }

    public Weather Weather { get; private set; }

    public string FirstWords { get; private set; }

    public string ProgramName { get; private set; }

    // ReSharper restore UnusedAutoPropertyAccessor.Global
}