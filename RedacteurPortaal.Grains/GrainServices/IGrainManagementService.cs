using Orleans;
using Orleans.Services;
using RedacteurPortaal.Grains.GrainInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.Grains.GrainServices
{
    public interface IGrainManagementService<T> : IGrainWithGuidKey where T : IManageableGrain
    {
        public Task<T> GetGrain(Guid id);

        public IEnumerable<T> GetGrains();

        public Task DeleteGrain(Guid id);
    }
}
