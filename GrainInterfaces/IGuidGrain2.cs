using Orleans;

namespace GrainInterfaces
{
    public interface IGuidGrain2 : IGrainWithGuidKey
    {
        Task<string> DoGrain2(string greeting);

    }
}
