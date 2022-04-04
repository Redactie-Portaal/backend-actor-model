using Export.Base;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Core;
using Orleans.Runtime;
using RedacteurPortaal.Data.Context;
using RedacteurPortaal.Grains.GrainInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.Api
{
    public class ExportPluginService : IExportPluginService
    {
        private readonly DataContext context;
        private readonly List<IExportPlugin> plugins;

        public ExportPluginService(DataContext context)
        {
            this.plugins = this.SetupExportPlugins();
            this.context = context;
        }

        public Task<List<IExportPlugin>> GetPlugins()
        {
            return Task.FromResult(this.plugins);
        }

        private List<IExportPlugin> SetupExportPlugins()
        {
            var pl = new List<IExportPlugin>();
            string pluginPath = AppContext.BaseDirectory + "/ExportPlugins";
            foreach (var dll in Directory.GetFiles(pluginPath, "*.dll"))
            {
                var assemblyContext = new AssemblyLoadContext(dll);
                var assembly = assemblyContext.LoadFromAssemblyPath(dll);
                var types = assembly.GetTypes();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
                IExportPlugin? plugin = Activator.CreateInstance(types.Single(x => x.FullName.Contains(assembly.ManifestModule.Name.Replace(".dll", "")))) as IExportPlugin;
#pragma warning restore CS8602 // Dereference of a possibly null reference.

                if (plugin is not null)
                {
                    pl.Add(plugin);
                }
            }

            SetupApiKeys();
            return pl;
        }

        private void SetupApiKeys()
        {
            var dbPlguins = context.PluginSettings.ToList();
            foreach (var plugin in plugins)
            {
                if (!dbPlguins.Any(x => x.PluginId == plugin.Id))
                {
                    dbPlguins.Add(new Data.Models.PluginSettings()
                    {
                        PluginId = plugin.Id,
                        ApiKey = ""
                    });
                }
            }

            context.SaveChanges();
        }
    }
}
