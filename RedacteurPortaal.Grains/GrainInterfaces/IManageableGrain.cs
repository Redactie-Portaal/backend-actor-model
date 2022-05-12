using Orleans;

namespace RedacteurPortaal.Grains.GrainInterfaces;

public interface IManageableGrain<T> : IGrainWithGuidKey
{
    public Task Delete();

    public Task<T> Get();

    public Task<bool> HasState();
}
