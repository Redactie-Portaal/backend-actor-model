using System.Data;
using Orleans;
using RedacteurPortaal.Grains.GrainInterfaces;
using RedacteurPortaal.Data.Context;
using RedacteurPortaal.DomainModels;

namespace RedacteurPortaal.Grains.GrainServices
{
    public class GrainManagementService<T, TReturnType> : IGrainManagementService<T> where T : class,  IManageableGrain<TReturnType>  where TReturnType : IBaseEntity
    {
        private readonly IClusterClient client;

        public DataContext DbContext { get; }

        public GrainManagementService(DataContext dbContext, IClusterClient client)
        {
            this.DbContext = dbContext;
            this.client = client;
        }

        public async Task<T> CreateGrain(Guid id)
        {
            if (this.DbContext.GrainReferences.Any(x => x.GrainId == id))
            {
                throw new DuplicateNameException($"Grain with id {id} already exists!");
            }

            var grain = await Task.FromResult(this.client.GetGrain<T>(id));
            this.DbContext.GrainReferences.Add(new Data.Models.GrainReference() { GrainId = id, TypeName = typeof(T).Name });
            await this.DbContext.SaveChangesAsync();
            return grain;
        }

        public async Task<T> GetGrain(Guid id)
        {
            var grain = await Task.FromResult(this.client.GetGrain<T>(id));

            if (!this.DbContext.GrainReferences.Any(x => x.GrainId == id))
            {
                throw new KeyNotFoundException($"Grain with id {id} not found!");
            }

            return grain;
        }

        public async Task<List<T>> GetGrains()
        {
            var grains = new List<T>();
            foreach (var grain in this.DbContext.GrainReferences.Where(x => x.TypeName == typeof(T).Name).ToList())
            {
                var realGrain = this.client.GetGrain<T>(grain.GrainId);
                if (realGrain.HasState().Result)
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
            if (this.DbContext.GrainReferences.Any(x => x.GrainId == id && x.TypeName == typeof(T).Name))
            {
                var grain = this.DbContext.GrainReferences.Single(x => x.GrainId == id);
                var realGrain = await this.GetGrain(id);
                await realGrain.Delete();
                this.DbContext.GrainReferences.Remove(grain);
                await this.DbContext.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Grain {id} not found");
            }
        }
    }
}