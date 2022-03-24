using Orleans;
using RedacteurPortaal.ClassLibrary.NewsItem;

namespace RedacteurPortaal.Grains.GrainInterfaces
{
    public interface INewsItemDescriptionGrain : IGrainWithGuidKey
    {
        Task<Description> GetDescription();
        Task AddDescription(Guid guid, Description des);
    }
}
