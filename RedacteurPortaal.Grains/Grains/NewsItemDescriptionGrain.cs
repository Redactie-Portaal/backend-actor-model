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

        public async Task AddDescription(Guid guid, ItemBody des)
        {
            _description.State.Guid = guid;
            _description.State.Description = des.Description;
            _description.State.ShortDescription = des.ShortDescription;
            await _description.WriteStateAsync();
        }

        public async Task<ItemBody> GetDescription()
        {
            _logger.LogInformation($"Got description from {_description.State}");
            return await Task.FromResult(_description.State);
        }

        public async Task DeleteDescription()
        {
            await _description.ClearStateAsync();
        }
    }
}
