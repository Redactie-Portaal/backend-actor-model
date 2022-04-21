using Orleans;
using RedacteurPortaal.DomainModels.NewsItem;

namespace RedacteurPortaal.Grains.GrainInterfaces;

public interface INewsItemDescriptionGrain : IManageableGrain<ItemBody>
{
   // TODO: Add update description
}