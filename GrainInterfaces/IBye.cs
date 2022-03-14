using Orleans;

namespace GrainInterfaces
{
    public interface IBye : IGrainWithIntegerKey
    {
        Task<string> SayBye(string greeting);
    }
}
