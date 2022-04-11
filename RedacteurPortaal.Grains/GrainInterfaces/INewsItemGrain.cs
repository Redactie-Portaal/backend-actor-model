﻿using Orleans;
using RedacteurPortaal.DomainModels.NewsItem;

namespace RedacteurPortaal.Grains.GrainInterfaces;

public interface INewsItemGrain : IManageableGrain<NewsItemModel>
{
    Task AddNewsItem(NewsItemModel newsitem);

    Task Update(NewsItemUpdate update);
}