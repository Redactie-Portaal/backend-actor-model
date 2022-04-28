using Export.Base;
using RedacteurPortaal.Data.Context;
using System.Runtime.Loader;
using RedacteurPortaal.Helpers;

namespace RedacteurPortaal.Api
{
    public class ExportPluginService : IExportPluginService
    {
        private readonly DataContext context;
        private readonly FileSystemProvider fileSystemProvider;
        private readonly List<IExportPlugin> plugins;

        public ExportPluginService(DataContext context, FileSystemProvider fileSystemProvider)
        {
            this.context = context;
            this.fileSystemProvider = fileSystemProvider;
            this.plugins = this.SetupExportPlugins();
            this.SetupApiKeys();
        }

        public List<IExportPlugin> GetPlugins()
        {
            return this.plugins;
        }

        private List<IExportPlugin> SetupExportPlugins()
        {
            var pl = new List<IExportPlugin>();
            string pluginPath = AppContext.BaseDirectory + "/ExportPlugins";
            foreach (var dll in this.fileSystemProvider.FileSystem.Directory.GetFiles(pluginPath, "*.dll"))
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

            return pl;
        }

        private void SetupApiKeys()
        {
            var dbPlguins = this.context.PluginSettings.ToList();
            foreach (var plugin in this.plugins.Where(plugin => dbPlguins.All(x => x.PluginId != plugin.Id)))
            {
                dbPlguins.Add(new Data.Models.PluginSettings()
                {
                    PluginId = plugin.Id,
                    ApiKey = ""
                });
            }

            this.context.SaveChanges();
        }
    }
}