namespace RedacteurPortaal.DomainModels.Media;

public class MediaVideoItem : MediaItem
{
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

    public string Presentation { get; }

    public TimeSpan Duration { get; }

    public string ArchiveMaterial { get; }

    public Weather Weather { get; }

    public string Producer { get; }

    public string Director { get; }

    public List<string> Guests { get; }

    public string FirstPicture { get; }

    public string LastWords { get; }

    public string ProgramName { get; }

    public string FirstWords { get; }

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
        string firstWords)
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
           format)
    {
        Reporter = reporter;
        Sound = sound;
        Editor = editor;
        LastPicture = lastPicture;
        Keywords = keywords;
        VoiceOver = voiceOver;
        Description = description;
        ProgramDate = programDate;
        ItemName = itemName;
        EPG = ePG;
        Presentation = presentation;
        Duration = duration;
        ArchiveMaterial = archiveMaterial;
        Weather = weather;
        Producer = producer;
        Director = director;
        Guests = guests;
        FirstPicture = firstPicture;
        LastWords = lastWords;
        ProgramName = programName;
        FirstWords = firstWords;
    }
}