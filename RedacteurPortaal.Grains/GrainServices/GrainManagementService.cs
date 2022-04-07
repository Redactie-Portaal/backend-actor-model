using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Core;
using Orleans.Runtime;
using RedacteurPortaal.Grains.GrainInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedacteurPortaal.Data.Context;

namespace RedacteurPortaal.Grains.GrainServices
{
    public class GrainManagementService<T> : IGrainManagementService<T> where T : IManageableGrain
    {
        private readonly IClusterClient client;

        public DataContext DbContext { get; }

        public GrainManagementService(DataContext dbContext, IClusterClient client)
        {
            this.DbContext = dbContext;
            this.client = client;
        }

        public async Task<T> GetGrain(Guid id)
        {
            var grain = await Task.FromResult(this.client.GetGrain<T>(id));

            if (!this.DbContext.GrainReferences.Any(x=> x.GrainId == id))
            {
                this.DbContext.GrainReferences.Add(new Data.Models.GrainReference() { GrainId = id, TypeName = typeof(T).Name });
                await this.DbContext.SaveChangesAsync();
            }
            return grain;
        }

        public IEnumerable<T> GetGrains()
        {
            foreach (var grain in this.DbContext.GrainReferences.Where(x => x.TypeName == typeof(T).Name).ToList())
            {
                yield return this.client.GetGrain<T>(grain.GrainId);
            }
        }

        public async Task DeleteGrain(Guid id)
        {
            var grain = this.DbContext.GrainReferences.Single(x => x.GrainId == id);
            await (await this.GetGrain(id)).Delete();
            this.DbContext.GrainReferences.Remove(grain);
        }
    }
}
