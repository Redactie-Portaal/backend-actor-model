using RedacteurPortaal.DomainModels.NewsItem;

namespace RedacteurPortaal.Grains.GrainInterfaces;

public interface ISourceGrain : IManageableGrain<Source>
{
    Task<Source> Update(Source source);
}