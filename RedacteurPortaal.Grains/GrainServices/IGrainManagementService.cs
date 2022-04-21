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
    public interface IGrainManagementService<T>
    {
        public Task<T> GetGrain(Guid id);

        public Task<List<T>> GetGrains();

        public Task<T> CreateGrain(Guid id);

        public Task DeleteGrain(Guid id);
    }
}
