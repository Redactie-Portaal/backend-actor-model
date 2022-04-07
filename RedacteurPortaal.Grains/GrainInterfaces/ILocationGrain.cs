using Orleans;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.DomainModels.NewsItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.Grains.GrainInterfaces
{
    public interface ILocationGrain : IGrainWithGuidKey
    {
        Location GetLocation(Guid guid);

        Task AddLocation(Location location);

        Task DeleteLocation(Guid guid);

        Task UpdateLocation(Location location);
    }
}
