using Microsoft.Extensions.Logging;
using Orleans.Runtime;
using RedacteurPortaal.DomainModels.NewsItem.Media;

namespace RedacteurPortaal.Grains.Grains
{
    public class NewsItemMediaGrain
    {
        private readonly ILogger logger;


        private readonly IPersistentState<MediaAudioItem> _audioItem;
        private readonly IPersistentState<MediaVideoItem> _videoItem;
        private readonly IPersistentState<MediaPhotoItem> _photoItem;

        public NewsItemMediaGrain(ILogger<NewsItemMediaGrain> logger,
            [PersistentState("audioItem", "OrleansStorage")] IPersistentState<MediaAudioItem> audioItem,
            [PersistentState("videoItem", "OrleansStorage")] IPersistentState<MediaVideoItem> videoItem,
            [PersistentState("photoItem", "OrleansStorage")] IPersistentState<MediaPhotoItem> photoItem
            
            )
        {
            this.logger = logger;
            _audioItem = audioItem;
            _videoItem = videoItem;
            _photoItem = photoItem;
        }
    }
}
