using RedacteurPortaal.DomainModels.NewsItem;

namespace RedacteurPortaal.Grains.GrainInterfaces;

public interface ISourceGrain : IManageableGrain<Source>
{
    Task Update(Source source);
}