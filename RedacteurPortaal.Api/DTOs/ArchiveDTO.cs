using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.Api.Models;

namespace RedacteurPortaal.Api.DTOs
{
    public class ArchiveDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Label { get; set; }

        public List<MediaPhotoItemDto>? MediaPhotoItems { get; set; }

        public List<MediaVideoItemDto>? MediaVideoItems { get; set; }
    
        public List<MediaAudioItemDto>? MediaAudioItems { get; set; }

        public List<NewsItemDto>? NewsItems { get; set; }

        public DateTime ArchivedDate { get; set; }

        public List<string>? Scripts { get; set; }
    }
}