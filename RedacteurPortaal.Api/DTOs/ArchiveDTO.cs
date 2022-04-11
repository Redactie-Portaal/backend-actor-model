using RedacteurPortaal.DomainModels.Media;

namespace RedacteurPortaal.Api.DTOs
{
    public class ArchiveDTO
    {
        public Guid Guid { get; set; }

        public string? Title { get; set; }

        public string? Label { get; set; }

        public List<MediaPhotoItem>? MediaPhotoItems { get; set; }

        public List<MediaVideoItem>? MediaVideoItems { get; set; }

        public List<MediaAudioItem>? MediaAudioItems { get; set; }

        public DateTime ArchivedDate { get; set; }

        public List<string>? Scripts { get; set; }
    }
}