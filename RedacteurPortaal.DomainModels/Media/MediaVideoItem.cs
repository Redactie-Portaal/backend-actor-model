namespace RedacteurPortaal.DomainModels.Media;

[Serializable]
public class MediaVideoItem : MediaItem
{
    public MediaVideoItem()
    {
    }

    public MediaVideoItem(
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
        string format,
        string reporter,
        string sound,
        string editor,
        string lastPicture,
        List<string> keywords,
        string voiceOver,
        string description,
        DateTime programDate,
        string itemName,
        string ePG,
        TimeSpan duration,
        string archiveMaterial,
        Weather weather,
        string producer,
        string director,
        List<string> guests,
        string firstPicture,
        string programName,
        string firstWords,
        Uri mediaLocation)
     : base(
           newsItemGuid,
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
           mediaLocation)
    {
        this.Reporter = reporter;
        this.Sound = sound;
        this.Editor = editor;
        this.LastPicture = lastPicture;
        this.Keywords = keywords;
        this.VoiceOver = voiceOver;
        this.Description = description;
        this.ProgramDate = programDate;
        this.ItemName = itemName;
        this.EPG = ePG;
        this.Presentation = presentation;
        this.Duration = duration;
        this.ArchiveMaterial = archiveMaterial;
        this.Weather = weather;
        this.Producer = producer;
        this.Director = director;
        this.Guests = guests;
        this.FirstPicture = firstPicture;
        this.LastWords = lastWords;
        this.ProgramName = programName;
        this.FirstWords = firstWords;
    }

    public string Reporter { get; }

    public string Sound { get; }

    public string Editor { get; }

    public string LastPicture { get; }

    public List<string> Keywords { get; }

    public string VoiceOver { get; }

    public string Description { get; }

    public DateTime ProgramDate { get; }

    public string ItemName { get; }

    public string EPG { get; }

    public TimeSpan Duration { get; }

    public string ArchiveMaterial { get; }

    public Weather Weather { get; }

    public string Producer { get; }

    public string Director { get; }

    public List<string> Guests { get; }

    public string FirstPicture { get; }

    public string ProgramName { get; }

    public string FirstWords { get; }
}