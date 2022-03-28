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

    public async Task<NewsItemModel> GetNewsItem(Guid guid)
    {
        var grain = this.GrainFactory.GetGrain<INewsItemDescriptionGrain>(guid);
        var description = await grain.GetDescription();
        var item = await Task.FromResult(newsItem.State);
        item.Body = description;
        return item;
    }

    public async Task AddNewsItem(NewsItemModel newsitem)
    {
        var grain = this.GrainFactory.GetGrain<INewsItemDescriptionGrain>(newsitem.Id);
        await grain.AddDescription(newsitem.Id, newsitem.Body);
        newsItem.State = newsitem;
        await newsItem.WriteStateAsync();
    }

    public async Task DeleteNewsItem(Guid guid)
    {
        var grain = GrainFactory.GetGrain<INewsItemDescriptionGrain>(guid);
        await grain.DeleteDescription();
        await newsItem.ClearStateAsync();
    }

    public async Task UpdateNewsItem(string name, Guid guid)
    {
        newsItem.State.Title = name;
        newsItem.State.Id = guid;
        await newsItem.WriteStateAsync();
    }
}