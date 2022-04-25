using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.DomainModels.NewsItem;

namespace RedacteurPortaal.Api.Models.Request
{
    public class UpdateArchiveRequest
    {
        public string Title { get; set; }

        public string Label { get; set; }

        public List<MediaPhotoItem> MediaPhotoItems { get; set; }

        public List<MediaVideoItem> MediaVideoItems { get; set; }

        public List<MediaAudioItem> MediaAudioItems { get; set; }

        public List<NewsItemModel>? NewsItems { get; set; }

        public List<NewsItemModel> Scripts { get; set; }
    }
}
}
