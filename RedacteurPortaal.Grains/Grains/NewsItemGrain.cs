using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.Grains.GrainInterfaces;

namespace RedacteurPortaal.Grains.Grains
{
    public class NewsItemGrain : Grain, INewsItemGrain
    {
        private readonly ILogger _logger;


        private readonly IPersistentState<NewsItemModel> _newsItem;

        public NewsItemGrain(ILogger<NewsItemGrain> logger,
            [PersistentState("newsitem", "OrleansStorage")] IPersistentState<NewsItemModel> newsItem)
        {
            _logger = logger;
            _newsItem = newsItem;
        }

        public async Task<NewsItemModel> GetNewsItem(Guid guid)
        {
            var grain = GrainFactory.GetGrain<INewsItemDescriptionGrain>(guid);
            var description = await grain.GetDescription();
            var item = await Task.FromResult(_newsItem.State);
            item.Body = description;
            return item;
        }

        public async Task AddNewsItem(NewsItemModel newsitem)
        {
            var grain = GrainFactory.GetGrain<INewsItemDescriptionGrain>(newsitem.Id);
            await grain.AddDescription(newsitem.Id, newsitem.Body);
            _newsItem.State = newsitem;
            await _newsItem.WriteStateAsync();
        }

        public async Task DeleteNewsItem(Guid guid)
        {
            var grain = GrainFactory.GetGrain<INewsItemDescriptionGrain>(guid);
            await grain.DeleteDescription();
            await _newsItem.ClearStateAsync();
        }

        public async Task UpdateNewsItem(string name, Guid guid)
        {
            _newsItem.State.Title = name;
            _newsItem.State.Id = guid;
            await _newsItem.WriteStateAsync();
        }
    }
}
