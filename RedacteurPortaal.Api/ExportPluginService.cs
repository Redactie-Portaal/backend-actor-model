using Export.Base;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Core;
using Orleans.Runtime;
using RedacteurPortaal.Grains.GrainInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.Api {
    public class ExportPluginService : IExportPluginService
    {
        private List<IExportPlugin> Plugins;

        public ExportPluginService()
        {
            this.Plugins = this.SetupExportPlugins();
        }

        public Task<List<IExportPlugin>> GetPlugins()
        {
            return Task.FromResult(this.Plugins);
        }
        

        private List<IExportPlugin> SetupExportPlugins()
        {
            var plugins = new List<IExportPlugin>();
            string pluginPath = AppContext.BaseDirectory + "/ExportPlugins";
            foreach (var dll in Directory.GetFiles(pluginPath, "*.dll"))
            {
                var assemblyContext = new AssemblyLoadContext(dll);
                var assembly = assemblyContext.LoadFromAssemblyPath(dll);
                var types = assembly.GetTypes();
                IExportPlugin? plugin = Activator.CreateInstance(types.Single(x=> x.FullName.Contains(assembly.ManifestModule.Name.Replace(".dll", "")))) as IExportPlugin;
                if (plugin is not null)
                {
                    plugins.Add(plugin);
                }
            }

            return plugins;
        }
    }
}
