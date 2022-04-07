using Orleans;
using RedacteurPortaal.DomainModels.NewsItem;

namespace RedacteurPortaal.Grains.GrainInterfaces;

public interface INewsItemGrain : IManageableGrain<NewsItemModel>
{
    public Task Update(NewsItemUpdate update);
}