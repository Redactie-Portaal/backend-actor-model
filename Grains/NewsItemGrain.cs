using ClassLibrary;
using GrainInterfaces;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime;
using WebApplication3;

namespace Grains
{
    public class NewsItemGrain : Grain, INewsItemGrain
    {
        private readonly ILogger logger;

        private IClusterClient _client;

        private readonly IPersistentState<NewsItem> _newsItem;


        public NewsItemGrain(ILogger<NewsItemGrain> logger, IClusterClient client,
            [PersistentState("newsitem", "newsitemstore")] IPersistentState<NewsItem> newsItem)
        {
            this.logger = logger;
            _client = client;
            _newsItem = newsItem;
        }

        public async Task<NewsItem> GetNewsItem()
        {
           return await Task.FromResult(_newsItem.State);
        }

        public async Task AddNewsItem(string name, Guid guid)
        {
            _newsItem.State.Name = name;
            _newsItem.State.Id = guid;
            await _newsItem.WriteStateAsync();
        }
    }
}
