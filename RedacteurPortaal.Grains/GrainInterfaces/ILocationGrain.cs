using Orleans;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.DomainModels.NewsItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedacteurPortaal.DomainModels.Shared;
using RedacteurPortaal.DomainModels.Validation.Shared;

namespace RedacteurPortaal.Grains.GrainInterfaces
{
    public interface ILocationGrain : IManageableGrain<Location>
    {
        Task AddLocation(Location location);

        Task Update(Location location);
    }
}
