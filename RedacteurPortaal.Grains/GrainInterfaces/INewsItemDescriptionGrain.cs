using Orleans;
using RedacteurPortaal.DomainModels.NewsItem;

namespace RedacteurPortaal.Grains.GrainInterfaces
{
    public interface INewsItemDescriptionGrain : IGrainWithGuidKey
    {
        Task<ItemBody> GetDescription();
        Task AddDescription(Guid guid, ItemBody des);
        Task DeleteDescription();
    }
}
