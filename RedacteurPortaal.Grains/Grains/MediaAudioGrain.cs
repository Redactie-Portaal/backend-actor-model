using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.Grains.GrainInterfaces;

namespace RedacteurPortaal.Grains.Grains;

public class MediaAudioGrain : Grain, IMediaAudioGrain
{
    private readonly IPersistentState<MediaAudioItem> audioItem;

    public MediaAudioGrain(
        [PersistentState("audioItem", "OrleansStorage")]
        IPersistentState<MediaAudioItem> audioItem)
    {
        this.audioItem = audioItem;
    }

    public void PlaceHolder()
    {
        _ = this.audioItem;
        throw new NotImplementedException();
    }
}