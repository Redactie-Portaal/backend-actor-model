using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.Grains.GrainInterfaces;

namespace RedacteurPortaal.Grains.Grains
{
    public class MediaVideoGrain : Grain, IMediaVideoGrain
    {
        private readonly ILogger logger;

        private readonly IPersistentState<MediaVideoItem> _videoItem;

        public MediaVideoGrain(ILogger<MediaPhotoGrain> logger,
            [PersistentState("videoItem", "OrleansStorage")] IPersistentState<MediaVideoItem> videoItem)
        {
            this.logger = logger;
            _videoItem = videoItem;
        }

        public void PlaceHolder()
        {
            throw new NotImplementedException();
        }
    }
}
