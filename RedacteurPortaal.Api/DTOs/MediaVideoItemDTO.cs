using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.DomainModels.Shared;

namespace RedacteurPortaal.Api.DTOs
{
    public class MediaVideoItemDTO : MediaItemDTO
    {
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
