using Microsoft.EntityFrameworkCore;
using RedacteurPortaal.Data.Models;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace RedacteurPortaal.Data.Context
{
    public class DataContext : DbContext
    {
        virtual public DbSet<PluginSettings> PluginSettings { get; set; }

        virtual public DbSet<GrainReference> GrainReferences { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DataContext()
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GrainReference>().Property(x => x.GrainId).HasConversion(v => v.ToString(), v => Guid.Parse(v));

            modelBuilder.Entity<PluginSettings>().Property(x => x.PluginId).HasConversion(v => v.ToString(), v => Guid.Parse(v));
            base.OnModelCreating(modelBuilder);
        }
    }
}