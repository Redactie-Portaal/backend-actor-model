using ClassLibrary;
using GrainInterfaces;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime;

namespace Grains
{
    public class NewsItemGrain : Grain, INewsItemGrain
    {
        private readonly ILogger logger;


        private readonly IPersistentState<NewsItem> _newsItem;


        public NewsItemGrain(ILogger<NewsItemGrain> logger,
            [PersistentState("newsitem", "OrleansStorage")] IPersistentState<NewsItem> newsItem)
        {
            this.logger = logger;
            _newsItem = newsItem;
        }

        public async Task<NewsItem> GetNewsItem()
        {
            return await Task.FromResult(_newsItem.State);
        }

        public async Task AddNewsItem(NewsItem newsitem)
        {
            _newsItem.State = newsitem;
            await _newsItem.WriteStateAsync();
        }



        public async Task DeleteNewsItem(Guid guid)
        {
            await _newsItem.ClearStateAsync();
        }
    }
}
