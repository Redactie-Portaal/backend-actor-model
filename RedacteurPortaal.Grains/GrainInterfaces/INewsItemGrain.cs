using Orleans;
using RedacteurPortaal.ClassLibrary.NewsItem;

namespace RedacteurPortaal.Grains.GrainInterfaces
{
    public interface INewsItemGrain : IGrainWithGuidKey
    {
        Task<NewsItemModel> GetNewsItem(Guid guid);
        Task AddNewsItem(NewsItemModel newsitem);

        Task DeleteNewsItem(Guid guid);

        Task UpdateNewsItem(string name, Guid guid);
    }
}
