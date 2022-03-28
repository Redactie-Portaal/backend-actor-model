namespace RedacteurPortaal.DomainModels.Media
{
    public class MediaVideoItem : MediaItem
    {
        string Reporter { get; }
        string Sound { get; }
        string Editor { get; }
        string LastPicture { get; }
        List<string> Keywords { get; }
        string VoiceOver { get; }
        string Description { get; }
        DateTime ProgramDate { get; }
        string ItemName { get; }
        string EPG { get; }
        string Presentation { get; }
        TimeSpan Duration { get; }
        string ArchiveMaterial { get; }
        Weather Weather { get; }
        string Producer { get; }
        string Director { get; }
        List<string> Guests { get; }
        string FirstPicture { get; }
        string LastWords { get; }
        string ProgramName { get; }
        string FirstWords { get; }

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
            string firstWords
            )
         : base(
               guid,
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
               format
               )
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
}