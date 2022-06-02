using RedacteurPortaal.Api.DTOs;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.DomainModels.NewsItem;

namespace RedacteurPortaal.Api.Models.Request
{
    public class UpdateArchiveRequest
    {
        public string Title { get; set; }

        public string Label { get; set; }

        public List<MediaPhotoItemDto>? MediaPhotoItems { get; set; }

        public List<MediaVideoItemDto>? MediaVideoItems { get; set; }

        public List<MediaAudioItemDto>? MediaAudioItems { get; set; }

        public List<NewsItemDto>? NewsItems { get; set; }

        public List<string> Scripts { get; set; }
    }
}