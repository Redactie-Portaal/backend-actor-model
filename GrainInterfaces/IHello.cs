using Orleans;

namespace GrainInterfaces
{
    public interface IHello : IGrainWithIntegerKey
    {
        Task<string> SayHello(string greeting);

    }
}
