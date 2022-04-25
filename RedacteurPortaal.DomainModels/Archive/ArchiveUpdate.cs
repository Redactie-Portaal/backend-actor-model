using RedacteurPortaal.DomainModels.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedacteurPortaal.DomainModels.NewsItem;

namespace RedacteurPortaal.DomainModels.Archive
{
    public class ArchiveUpdate
    {
        public ArchiveUpdate()
        {
        }

        public ArchiveUpdate(string title, string label, List<MediaPhotoItem> mediaPhotoItems, List<MediaVideoItem> mediaVideoItems, List<MediaAudioItem> mediaAudioItems, List<NewsItemModel> newsItems,List<string> scripts)
        {
            this.Title = title ?? throw new ArgumentNullException(nameof(title));
            this.Label = label ?? throw new ArgumentNullException(nameof(label));
            this.MediaPhotoItems = mediaPhotoItems ?? throw new ArgumentNullException(nameof(mediaPhotoItems));
            this.MediaVideoItems = mediaVideoItems ?? throw new ArgumentNullException(nameof(mediaVideoItems));
            this.MediaAudioItems = mediaAudioItems ?? throw new ArgumentNullException(nameof(mediaAudioItems));
            this.NewsItems = newsItems ?? throw new ArgumentNullException(nameof(newsItems));
            this.Scripts = scripts ?? throw new ArgumentNullException(nameof(scripts));
        }

        public string Title { get; }

        public string Label { get; }

        public List<MediaPhotoItem> MediaPhotoItems { get; }

        public List<MediaVideoItem> MediaVideoItems { get; }

        public List<MediaAudioItem> MediaAudioItems { get; }

        public List<NewsItemModel> NewsItems { get; }

        public List<string> Scripts { get; }
    }
}
