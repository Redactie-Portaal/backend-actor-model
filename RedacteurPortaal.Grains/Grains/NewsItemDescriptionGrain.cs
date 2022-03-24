using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.ClassLibrary.NewsItem;
using RedacteurPortaal.Grains.GrainInterfaces;

namespace RedacteurPortaal.Grains.Grains
{
    public class NewsItemDescriptionGrain : Grain, INewsItemDescriptionGrain
    {
        private readonly ILogger logger;

        private readonly IPersistentState<Description> _description;

        public NewsItemDescriptionGrain(ILogger<NewsItemDescriptionGrain> logger,
           [PersistentState("newsitem", "OrleansStorage")] IPersistentState<Description> description)
        {
            this.logger = logger;
            _description = description;
        }

        public async Task AddDescription(Guid guid, Description des)
        {
            _description.State.guid = guid;
            _description.State.Des = des.Des;
            _description.State.Short = des.Short;
            await _description.WriteStateAsync();
        }

        public async Task<Description> GetDescription()
        {
            return await Task.FromResult(_description.State);
        }
    }
}
