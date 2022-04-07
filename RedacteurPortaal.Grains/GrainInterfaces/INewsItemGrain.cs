using Orleans;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.DomainModels.NewsItem.Requests;

namespace RedacteurPortaal.Grains.GrainInterfaces;

public interface INewsItemGrain : IManageableGrain<NewsItemModel>
{
    new Task<NewsItemModel> Get();

    Task AddNewsItem(NewsItemModel newsitem);

    Task DeleteNewsItem(Guid guid);

    new Task Delete();

    Task Update(NewsItemUpdate update);
}