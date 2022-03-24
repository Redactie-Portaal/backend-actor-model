using Orleans;
using RedacteurPortaal.ClassLibrary.NewsItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.Grains.GrainInterfaces
{
    public interface INewsItemDescriptionGrain : IGrainWithGuidKey
    {
        Task<Description> GetDescription();
        Task AddDescription(Guid guid, Description des);
    }
}
