using System.Data;
using Orleans;
using RedacteurPortaal.Grains.GrainInterfaces;
using RedacteurPortaal.Data.Context;
using RedacteurPortaal.DomainModels;
using Microsoft.Extensions.Logging;

namespace RedacteurPortaal.Grains.GrainServices
{
    public class GrainManagementService<T, TReturnType> : IGrainManagementService<T> where T : class,  IManageableGrain<TReturnType>  where TReturnType : IBaseEntity
    {
        private readonly IClusterClient client;
        private readonly ILogger logger;
            
        public DataContext DbContext { get; }

        public GrainManagementService(DataContext dbContext, IClusterClient client, ILogger<GrainManagementService<T, TReturnType>> logger)
        {
            this.DbContext = dbContext;
            this.client = client;
            this.logger = logger;
        }

        public async Task<T> CreateGrain(Guid id)
        {
            if (this.DbContext.GrainReferences.Any(x => x.GrainId == id))
            {
#pragma warning disable CA2254 // Template should be a static expression
                this.logger.LogCritical($"Grain with ID: {id} already exists!");
#pragma warning restore CA2254 // Template should be a static expression
                throw new DuplicateNameException($"Grain with id {id} already exists!");
            }

            var grain = await Task.FromResult(this.client.GetGrain<T>(id));
            this.DbContext.GrainReferences.Add(new Data.Models.GrainReference() { GrainId = id, TypeName = typeof(T).Name });
            await this.DbContext.SaveChangesAsync();
            return grain;
        }

        public bool GrainExists(Guid id)
        {
            return this.DbContext.GrainReferences.Any(x => x.GrainId == id);
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
#pragma warning disable CA2254 // Template should be a static expression
                    this.logger.LogWarning($"Grain with ID: {grain.GrainId} does not have a state. Removing Grain.");
#pragma warning restore CA2254 // Template should be a static expression
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

        public async Task<T> GetGrainOrCreate(Guid id)
        {
            if (this.GrainExists(id))
            {
                return await this.GetGrain(id);
            }

            return await this.CreateGrain(id);
        }
    }
}