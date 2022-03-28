using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.Grains.GrainInterfaces;

namespace RedacteurPortaal.Grains.Grains
{
    public class MediaAudioGrain : Grain, IMediaAudioGrain
    {
        private readonly ILogger logger;


        private readonly IPersistentState<MediaAudioItem> _audioItem;

        public MediaAudioGrain(ILogger<MediaPhotoGrain> logger,
            [PersistentState("audioItem", "OrleansStorage")] IPersistentState<MediaAudioItem> audioItem)
        {
            this.logger = logger;
            _audioItem = audioItem;
        }

        public void PlaceHolder()
        {
            throw new NotImplementedException();
        }
    }
}
