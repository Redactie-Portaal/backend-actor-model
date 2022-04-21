using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.Grains.GrainInterfaces;

namespace RedacteurPortaal.Grains.Grains;

public class NewsItemGrain : Grain, INewsItemGrain
{
    private readonly IPersistentState<NewsItemModel> newsItem;

    public bool HasState => this.newsItem.RecordExists;

    public NewsItemGrain(
        [PersistentState("newsitem", "OrleansStorage")]
        IPersistentState<NewsItemModel> newsItem)
    {
        this.newsItem = newsItem;
    }

    public async Task<NewsItemModel> Get()
    {
        var grain = this.GrainFactory.GetGrain<INewsItemDescriptionGrain>(this.newsItem.State.Id);
        var description = await grain.Get();
        var item = await Task.FromResult(this.newsItem.State);

        // TODO: Merge description
        return item;
    }

    public  async Task Delete()
    {
        var grain = this.GrainFactory.GetGrain<INewsItemDescriptionGrain>(this.newsItem.State.Id);
        await grain.Delete();
        await this.newsItem.ClearStateAsync();
    }

    public async Task Update(NewsItemModel update)
    {
        // TODO: Merge title.
        this.newsItem.State = update;
        await this.newsItem.WriteStateAsync();
    }
}