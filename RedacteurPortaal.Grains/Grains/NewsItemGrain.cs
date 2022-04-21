using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.Grains.GrainInterfaces;

namespace RedacteurPortaal.Grains.Grains;

public class NewsItemGrain : Grain, INewsItemGrain
{
    private readonly IPersistentState<NewsItemModel> newsItem;

    public Task<bool> HasState()
    {
        return Task.FromResult(this.newsItem.RecordExists);
    }

    public NewsItemGrain(

#if DEBUG
        // This works in testing.
        [PersistentState("newsitem")]
#else
        // This doesn't work in testing, but I don't know why.
        [PersistentState("newsitem", "OrleansStorage")]
#endif
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
        //this.newsItem.State.Title = "abc";
        await this.newsItem.WriteStateAsync();

        //var item = await this.Get();
    }

    public async Task Delete()
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