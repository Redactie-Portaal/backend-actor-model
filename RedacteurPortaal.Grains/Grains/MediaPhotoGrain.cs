using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.Grains.GrainInterfaces;

namespace RedacteurPortaal.Grains.Grains
{
    public class MediaPhotoGrain : Grain, IMediaPhotoGrain
    {
        private readonly ILogger logger;

        private readonly IPersistentState<MediaPhotoItem> _photoItem;

        public MediaPhotoGrain(ILogger<MediaPhotoGrain> logger,
            [PersistentState("photoItem", "OrleansStorage")] IPersistentState<MediaPhotoItem> photoItem)
        {
            this.logger = logger;
            _photoItem = photoItem;
        }
    }
}
