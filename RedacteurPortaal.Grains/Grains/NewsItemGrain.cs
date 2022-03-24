using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.ClassLibrary.NewsItem;
using RedacteurPortaal.Grains.GrainInterfaces;

namespace RedacteurPortaal.Grains.Grains
{
    public class NewsItemGrain : Grain, INewsItemGrain
    {
        private readonly ILogger logger;


        private readonly IPersistentState<NewsItemModel> _newsItem;

        public NewsItemGrain(ILogger<NewsItemGrain> logger,
            [PersistentState("newsitem", "OrleansStorage")] IPersistentState<NewsItemModel> newsItem)
        {
            this.logger = logger;
            _newsItem = newsItem;
        }

        public async Task<NewsItemRequest> GetNewsItem(Guid guid)
        {
            var grain = GrainFactory.GetGrain<INewsItemDescriptionGrain>(guid);
            var description = await grain.GetDescription();
            var item = await Task.FromResult(_newsItem.State);
            var newsItem = new NewsItemRequest()
            {
                Id = item.Id,
                Description = description,
                Category = item.Category,
                ContactDetails = item.ContactDetails,
                EndDate = item.EndDate, 
                Idea = item.Idea,   
                ProdutionDate = item.ProdutionDate, 
                Location = item.LocationDetails,
                Region = item.Region,
                Title = item.Title
            };
            return newsItem;
        }

        public async Task AddNewsItem(NewsItemRequest newsitem)
        {
            var grain = GrainFactory.GetGrain<INewsItemDescriptionGrain>(newsitem.Id);
            await grain.AddDescription(newsitem.Id, newsitem.Description);
            
            _newsItem.State.Id = newsitem.Id;
            _newsItem.State.EndDate = newsitem.EndDate;
            _newsItem.State.ProdutionDate = newsitem.ProdutionDate;
            _newsItem.State.ContactDetails = newsitem.ContactDetails;
            _newsItem.State.Title = newsitem.Title;
            _newsItem.State.Idea = newsitem.Idea;
            _newsItem.State.Category = newsitem.Category;
            _newsItem.State.Region = newsitem.Region;
            _newsItem.State.LocationDetails = newsitem.Location;
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
