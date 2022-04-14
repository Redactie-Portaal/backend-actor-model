using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.Grains.GrainInterfaces;

namespace RedacteurPortaal.Grains.Grains;

public class NewsItemGrain : Grain, INewsItemGrain
{
    private readonly IPersistentState<NewsItemModel> newsItem;

    public NewsItemGrain(
        [PersistentState("newsitem", "OrleansStorage")]
        IPersistentState<NewsItemModel> newsItem)
    {
        this.newsItem = newsItem;
    }

    public async Task<NewsItemModel> Get()
    {
        //var grain = this.GrainFactory.GetGrain<INewsItemDescriptionGrain>(this.newsItem.State.Id);
        //var description = await grain.GetDescription();
        //var item = Task.FromResult(this.newsItem.State);

        // TODO: Merge description
        //return item;
        //await Task.Delay(10000);
        //await this.newsItem.ReadStateAsync();
        return await Task.FromResult(this.newsItem.State);
    }

    public async Task AddNewsItem(NewsItemModel newsitem)
    {
        this.newsItem.State = newsitem;
        await this.newsItem.WriteStateAsync();
        //var item = await this.Get();
    }

    public async Task DeleteNewsItem(Guid guid)
    {
        var grain = this.GrainFactory.GetGrain<INewsItemDescriptionGrain>(guid);
        await grain.DeleteDescription();
        await this.newsItem.ClearStateAsync();
    }

    public  async Task Delete()
    {
        var grain = this.GrainFactory.GetGrain<INewsItemDescriptionGrain>(this.newsItem.State.Id);
        await grain.DeleteDescription();
        await this.newsItem.ClearStateAsync();
    }

    public async Task Update(NewsItemUpdate update)
    {
        // TODO: Merge title.
        await this.newsItem.WriteStateAsync();
    }
}