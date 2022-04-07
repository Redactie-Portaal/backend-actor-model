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
using RedacteurPortaal.DomainModels;

namespace RedacteurPortaal.Grains.GrainServices
{
    public class GrainManagementService<T, TReturnType> : IGrainManagementService<T> where T : IManageableGrain<TReturnType> where TReturnType : IBaseEntity
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

        public async Task<List<T>> GetGrains()
        {
            var grains = new List<T>();
            foreach (var grain in this.DbContext.GrainReferences.Where(x => x.TypeName == typeof(T).Name).ToList())
            {
                var realGrain = this.client.GetGrain<T>(grain.GrainId);
                if (realGrain.Get().Result.Id != Guid.Empty)
                {
                    grains.Add(realGrain);
                }
                else
                {
                    this.DbContext.GrainReferences.Remove(grain);
                    await this.DbContext.SaveChangesAsync();
                }
            }

            return grains;
        }

        public async Task DeleteGrain(Guid id)
        {
            var grain = this.DbContext.GrainReferences.Single(x => x.GrainId == id);
            var realGrain = await this.GetGrain(id);
            
            this.DbContext.GrainReferences.Remove(grain);
        }
    }
}
