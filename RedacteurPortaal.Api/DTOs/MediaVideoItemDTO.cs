using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.DomainModels.NewsItem;

namespace RedacteurPortaal.Api.DTOs
{
    public class MediaVideoItemDTO : MediaItemDTO
    {
        public MediaVideoItemDTO()
        {
        }
        
        public MediaVideoItemDTO(
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
            int durationseconds,
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
            this.DurationSeconds = durationseconds;
            this.ArchiveMaterial = archiveMaterial;
            this.Weather = weather;
            this.Producer = producer;
            this.Director = director;
            this.Guests = guests;
            this.FirstPicture = firstPicture;
            this.ProgramName = programName;
            this.FirstWords = firstWords;
        }

        public string Reporter { get; set; }

        public string Sound { get; set; }

        public string Editor { get; set; }

        public string LastPicture { get; set; }

        public List<string> Keywords { get; set; }

        public string VoiceOver { get; set; }

        public string Description { get; set; }

        public DateTime ProgramDate { get; set; }

        public string ItemName { get; set; }

        public string EPG { get; set; }

        public int DurationSeconds { get; set; }

        public string ArchiveMaterial { get; set; }

        public Weather Weather { get; set; }

        public string Producer { get; set; }

        public string Director { get; set; }

        public List<string> Guests { get; set; }

        public string FirstPicture { get; set; }

        public string ProgramName { get; set; }

        public string FirstWords { get; set; }
    }
}
