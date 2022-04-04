using Microsoft.EntityFrameworkCore;
using RedacteurPortaal.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace RedacteurPortaal.Data.Context
{
    public class DataContext : DbContext
    {
        public DbSet<PluginSettings> PluginSettings { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PluginSettings>().Property(x=> x.PluginId).HasConversion(v => v.ToString(), v => Guid.Parse(v));
            base.OnModelCreating(modelBuilder);
        }
    }
}
