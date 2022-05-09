using Export.Base;

namespace RedacteurPortaal.Api
{
    public interface IExportPluginService
    {
        List<IExportPlugin> GetPlugins();
    }
}