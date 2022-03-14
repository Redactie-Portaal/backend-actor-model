using Orleans;

namespace GrainInterfaces
{
    public interface IGuidGrain1 : IGrainWithGuidKey
    {
        Task<string> DoGrain(string greeting, Guid guid);

    }
}
