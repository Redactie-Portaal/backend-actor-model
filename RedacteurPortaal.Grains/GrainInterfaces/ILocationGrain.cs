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
    public interface ILocationGrain : IManageableGrain<Location>
    {
        Task Update(Location location);
    }
}
