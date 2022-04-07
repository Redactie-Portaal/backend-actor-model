using Orleans;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.DomainModels.NewsItem.Requests;

namespace RedacteurPortaal.Grains.GrainInterfaces;

public interface INewsItemGrain : IGrainWithGuidKey
{
    Task<NewsItemModel> GetNewsItem(Guid guid);

    Task AddNewsItem(NewsItemModel newsitem);

    Task DeleteNewsItem(Guid guid);

    Task UpdateNewsItem(UpdateNewsItemRequest request);
}