using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.Grains.GrainInterfaces;

namespace RedacteurPortaal.Grains.Grains;

public class NewsItemDescriptionGrain : Grain, INewsItemDescriptionGrain
{
    private readonly IPersistentState<ItemBody> description;

    public Task<bool> HasState()
    {
        return Task.FromResult(this.description.RecordExists);
    }

    public NewsItemDescriptionGrain(
    [PersistentState("newsItemDescription", "OrleansStorage")]
    IPersistentState<ItemBody> description)
    {
        this.description = description;
    }

    public async Task<ItemBody> Get()
    {
        return await Task.FromResult(this.description.State);
    }

    public async Task Delete()
    {
        await this.description.ClearStateAsync();
    }

    public async Task Update(ItemBody item)
    {
        this.description.State = item;
        await this.description.WriteStateAsync();
    }
}