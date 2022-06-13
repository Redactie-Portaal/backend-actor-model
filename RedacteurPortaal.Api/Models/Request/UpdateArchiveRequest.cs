using RedacteurPortaal.Api.DTOs;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.DomainModels.NewsItem;

namespace RedacteurPortaal.Api.Models.Request
{
    public class UpdateArchiveRequest
    {
        public string Title { get; set; }

        public string Label { get; set; }

        public List<Guid>? MediaPhotoItems { get; set; }

        public List<Guid>? MediaVideoItems { get; set; }

        public List<Guid>? MediaAudioItems { get; set; }

        public List<Guid>? NewsItems { get; set; }

        public List<string> Scripts { get; set; }
    }
}