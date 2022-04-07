using Orleans;
using RedacteurPortaal.DomainModels.NewsItem;

namespace RedacteurPortaal.Grains.GrainInterfaces;

public interface INewsItemGrain : IGrainWithGuidKey, IManageableGrain
{
    Task<NewsItemModel> GetNewsItem(Guid guid);

    Task AddNewsItem(NewsItemModel newsitem);

    Task DeleteNewsItem(Guid guid);

    Task UpdateNewsItem(string name, Guid guid);
}