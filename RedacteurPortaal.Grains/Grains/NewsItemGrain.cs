﻿using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.ClassLibrary;
using RedacteurPortaal.Grains.GrainInterfaces;

namespace RedacteurPortaal.Grains.Grains
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

        public async Task UpdateNewsItem(string name, Guid guid)
        {
            _newsItem.State.Title = name;
            _newsItem.State.Id = guid;
            await _newsItem.WriteStateAsync();
        }
    }
}
