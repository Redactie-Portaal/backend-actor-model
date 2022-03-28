using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.Grains.GrainInterfaces;

namespace RedacteurPortaal.Grains.Grains;

public class NewsItemDescriptionGrain : Grain, INewsItemDescriptionGrain
{
    private readonly IPersistentState<ItemBody> description;

    public NewsItemDescriptionGrain(
        [PersistentState("newsitem", "OrleansStorage")]
        IPersistentState<ItemBody> description)
    {
        this.description = description;
    }

    public async Task AddDescription(Guid guid, ItemBody description)
    {
        this.description.State.Guid = guid;
        this.description.State.Description = description.Description;
        this.description.State.ShortDescription = description.ShortDescription;
        await this.description.WriteStateAsync();
    }

    public async Task<ItemBody> GetDescription()
    {
        return await Task.FromResult(this.description.State);
    }

    public async Task DeleteDescription()
    {
        await this.description.ClearStateAsync();
    }
}