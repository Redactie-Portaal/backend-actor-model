using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.DomainModels.NewsItem;

namespace RedacteurPortaal.Api.DTOs
{
    public class ArchiveDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Label { get; set; }

        public List<MediaPhotoItem>? MediaPhotoItems { get; set; }

        public List<MediaVideoItem>? MediaVideoItems { get; set; }

        public List<MediaAudioItem>? MediaAudioItems { get; set; }

        public List<NewsItemModel>? NewsItems { get; set; }

        public DateTime ArchivedDate { get; set; }

        public List<string>? Scripts { get; set; }
    }
}