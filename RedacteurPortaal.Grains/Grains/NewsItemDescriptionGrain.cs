using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.Grains.GrainInterfaces;

namespace RedacteurPortaal.Grains.Grains
{
    public class NewsItemDescriptionGrain : Grain, INewsItemDescriptionGrain
    {
        private readonly ILogger _logger;

        private readonly IPersistentState<ItemBody> _description;

        public NewsItemDescriptionGrain(ILogger<NewsItemDescriptionGrain> logger,
           [PersistentState("newsitem", "OrleansStorage")] IPersistentState<ItemBody> description)
        {
            _logger = logger;
            _description = description;
        }

        public async Task AddDescription(Guid guid, ItemBody description)
        {
            _description.State.Guid = guid;
            _description.State.Description = description.Description;
            _description.State.ShortDescription = description.ShortDescription;
            await _description.WriteStateAsync();
        }

        public async Task<ItemBody> GetDescription()
        {
            return await Task.FromResult(_description.State);
        }

        public async Task DeleteDescription()
        {
            await _description.ClearStateAsync();
        }
    }
}
