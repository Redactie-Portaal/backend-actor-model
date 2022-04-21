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
        return await Task.FromResult(this.newsItem.State);
    }

    public async Task Delete()
    {
        var grain = this.GrainFactory.GetGrain<INewsItemDescriptionGrain>(this.newsItem.State.Id);
        await grain.Delete();
        await this.newsItem.ClearStateAsync();
    }

    public async Task Update(NewsItemModel newsItem)
    {
        this.newsItem.State = newsItem;
        await this.newsItem.WriteStateAsync();
    }
}