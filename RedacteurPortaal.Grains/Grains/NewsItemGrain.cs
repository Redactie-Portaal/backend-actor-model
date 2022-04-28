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
    [PersistentState("newsitem", "OrleansStorage")]
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
        await this.newsItem.ClearStateAsync();
    }

    public async Task<NewsItemModel> Update(NewsItemModel newsItem)
    {
        this.newsItem.State = newsItem;
        await this.newsItem.WriteStateAsync();
        return this.newsItem.State;
    }
}