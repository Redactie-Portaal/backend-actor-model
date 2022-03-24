using Orleans;
using RedacteurPortaal.ClassLibrary.NewsItem;

namespace RedacteurPortaal.Grains.GrainInterfaces
{
    public interface INewsItemGrain : IGrainWithGuidKey
    {
        Task<NewsItemRequest> GetNewsItem(Guid guid);
        Task AddNewsItem(NewsItemRequest newsitem);

        Task DeleteNewsItem(Guid guid);

        Task UpdateNewsItem(string name, Guid guid);
    }
}
