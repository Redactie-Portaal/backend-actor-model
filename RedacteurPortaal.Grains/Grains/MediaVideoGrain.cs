using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.Grains.GrainInterfaces;

namespace RedacteurPortaal.Grains.Grains;

public class MediaVideoGrain : Grain, IMediaVideoGrain
{
    private readonly ILogger logger;

    private readonly IPersistentState<MediaVideoItem> videoItem;

    public MediaVideoGrain(
        ILogger logger,
        [PersistentState("videoItem", "OrleansStorage")]
        IPersistentState<MediaVideoItem> videoItem)
    {
        this.logger = logger;
        this.videoItem = videoItem;
    }

    public void PlaceHolder()
    {
        throw new NotImplementedException();
    }
}