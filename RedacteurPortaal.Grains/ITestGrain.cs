using Orleans;

namespace RedacteurPortaal.Grains
{
    public interface ITestGrain : IGrainWithGuidKey
    {
        Task<string> Test();
    }
}
