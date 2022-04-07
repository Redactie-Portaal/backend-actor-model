using Orleans;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.DomainModels.NewsItem.Requests;

namespace RedacteurPortaal.Grains.GrainInterfaces;

public interface INewsItemGrain : IManageableGrain<NewsItemModel>
{
    Task<NewsItemModel> Get();

    Task AddNewsItem(NewsItemModel newsitem);

    Task DeleteNewsItem(Guid guid)

    Task Delete();

    Task Update(NewsItemUpdate update)
}