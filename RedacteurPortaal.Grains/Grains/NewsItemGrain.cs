﻿using Orleans;
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
        var grain = this.GrainFactory.GetGrain<INewsItemDescriptionGrain>(this.newsItem.State.Id);
        var description = await grain.GetDescription();
        var item = await Task.FromResult(this.newsItem.State);

        // TODO: Merge description
        return item;
    }

    public async Task AddNewsItem(NewsItemModel newsitem)
    {
        var grain = this.GrainFactory.GetGrain<INewsItemDescriptionGrain>(newsitem.Id);
        await grain.AddDescription(newsitem.Id, newsitem.Body);
        this.newsItem.State = newsitem;
        await this.newsItem.WriteStateAsync();
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