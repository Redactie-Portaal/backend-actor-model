namespace RedacteurPortaal.DomainModels.Media
{
    public class MediaAudioItem : MediaItem
    {
        TimeSpan Duration { get; }
        Weather Weather { get; }
        Location Location { get; }
        string FirstWords { get; }
        string ProgramName { get; }
        string Presentation { get; }

        public MediaAudioItem(
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
            TimeSpan duration,
            Weather weather,
            string firstWords,
            string programName
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
            Duration = duration;
            Weather = weather;
            Location = location;
            FirstWords = firstWords;
            ProgramName = programName;
            Presentation = presentation;
        }
    }
}