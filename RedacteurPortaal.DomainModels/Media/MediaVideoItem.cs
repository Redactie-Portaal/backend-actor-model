using RedacteurPortaal.DomainModels.Shared;

namespace RedacteurPortaal.DomainModels.Media;

[Serializable]
public class MediaVideoItem : MediaItem, IBaseEntity
{
    public MediaVideoItem()
    {
    }

    public MediaVideoItem(
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
        this.Duration = duration;
        this.ArchiveMaterial = archiveMaterial;
        this.Weather = weather;
        this.Producer = producer;
        this.Director = director;
        this.Guests = guests;
        this.FirstPicture = firstPicture;
        this.ProgramName = programName;
        this.FirstWords = firstWords;
    }

    public string Reporter { get; private set; }

    public string Sound { get; private set; }

    public string Editor { get; private set; }

    public string LastPicture { get; private set; }

    public List<string> Keywords { get; private set; }

    public string VoiceOver { get; private set; }

    public string Description { get; private set; }

    public DateTime ProgramDate { get; private set; }

    public string ItemName { get; private set; }

    public string EPG { get; private set; }

    public TimeSpan Duration { get; private set; }

    public string ArchiveMaterial { get; private set; }

    public Weather Weather { get; private set; }

    public string Producer { get; private set; }

    public string Director { get; private set; }

    public List<string> Guests { get; private set; }

    public string FirstPicture { get; private set; }

    public string ProgramName { get; private set; }

    public string FirstWords { get; private set; }
}