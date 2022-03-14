using Orleans;

namespace GrainInterfaces
{
    public interface ITestingGrain : IGrainWithIntegerKey
    {
        Task<string> DoSomething(string greeting);

    }
}
