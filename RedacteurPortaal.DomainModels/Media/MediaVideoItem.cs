using FluentValidation;
using RedacteurPortaal.DomainModels.Shared;
using RedacteurPortaal.DomainModels.Validation.Media;
using RedacteurPortaal.DomainModels.Validation.Shared;

namespace RedacteurPortaal.DomainModels.Media;

public class MediaVideoItem : MediaItem
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
        this.Reporter = reporter ?? throw new ArgumentNullException(nameof(reporter));
        this.Sound = sound ?? throw new ArgumentNullException(nameof(sound));
        this.Editor = editor ?? throw new ArgumentNullException(nameof(editor));
        this.LastPicture = lastPicture ?? throw new ArgumentNullException(nameof(lastPicture));
        this.Keywords = keywords ?? throw new ArgumentNullException(nameof(keywords));
        this.VoiceOver = voiceOver ?? throw new ArgumentNullException(nameof(voiceOver));
        this.Description = description ?? throw new ArgumentNullException(nameof(description));
        this.ProgramDate = programDate;
        this.ItemName = itemName ?? throw new ArgumentNullException(nameof(itemName));
        this.EPG = ePG ?? throw new ArgumentNullException(nameof(ePG));
        this.Duration = duration;
        this.ArchiveMaterial = archiveMaterial ?? throw new ArgumentNullException(nameof(archiveMaterial));
        this.Weather = weather;
        this.Producer = producer ?? throw new ArgumentNullException(nameof(producer));
        this.Director = director ?? throw new ArgumentNullException(nameof(director));
        this.Guests = guests ?? throw new ArgumentNullException(nameof(guests));
        this.FirstPicture = firstPicture ?? throw new ArgumentNullException(nameof(firstPicture));
        this.ProgramName = programName ?? throw new ArgumentNullException(nameof(programName));
        this.FirstWords = firstWords ?? throw new ArgumentNullException(nameof(firstWords));

        new MediaVideoItemValidator().ValidateAndThrow(this);
        new LocationValidator().ValidateAndThrow(this.Location);
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