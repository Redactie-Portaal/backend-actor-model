using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.ClassLibrary.NewsItem;
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
            _description.State.guid = guid;
            _description.State.Des = des.Des;
            _description.State.Short = des.Short;
            await _description.WriteStateAsync();
        }

        public async Task<ItemBody> GetDescription()
        {
            _logger.LogInformation($"Got description from {_description.State.guid}");
            return await Task.FromResult(_description.State);
        }

        public async Task DeleteDescription()
        {
            await _description.ClearStateAsync();
        }
    }
}
