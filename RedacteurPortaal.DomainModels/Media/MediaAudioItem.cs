namespace RedacteurPortaal.DomainModels.Media;

public class MediaAudioItem : MediaItem
{
    public MediaAudioItem(
        TimeSpan duration,
        Weather weather,
        Location location,
        string firstWords,
        string programName,
        string presentation)
    {
        this.Duration = duration;
        this.Weather = weather;
        this.Location = location ?? throw new ArgumentNullException(nameof(location));
        this.FirstWords = firstWords ?? throw new ArgumentNullException(nameof(firstWords));
        this.ProgramName = programName ?? throw new ArgumentNullException(nameof(programName));
        this.Presentation = presentation ?? throw new ArgumentNullException(nameof(presentation));
    }

    // Delete deze comments als je deze class wilt gebruiken.
    // ReSharper disable UnusedAutoPropertyAccessor.Global
    public TimeSpan Duration { get; }

    public Weather Weather { get; }

    public Location Location { get; }

    public string FirstWords { get; }

    public string ProgramName { get; }

    public string Presentation { get; }

    // ReSharper restore UnusedAutoPropertyAccessor.Global
}