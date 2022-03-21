using Orleans;
using RedacteurPortaal.ClassLibrary;

namespace RedacteurPortaal.Grains.GrainInterfaces
{
    public interface INewsItemGrain : IGrainWithGuidKey
    {
        Task<NewsItem> GetNewsItem();
        Task AddNewsItem(NewsItem newsitem);

        Task DeleteNewsItem(Guid guid);

        Task UpdateNewsItem(string name, Guid guid);
    }
}
