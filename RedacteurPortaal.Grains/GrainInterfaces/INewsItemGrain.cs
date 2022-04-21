using Orleans;
using RedacteurPortaal.DomainModels.NewsItem;

namespace RedacteurPortaal.Grains.GrainInterfaces;

public interface INewsItemGrain : IManageableGrain<NewsItemModel>
{ 
    Task Update(NewsItemModel update);
    
    Task AddNewsItem(NewsItemModel newsitem);
}