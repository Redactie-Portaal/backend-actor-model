using Export.Base;
using Orleans.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.Api
{
    public interface IExportPluginService
    {
        Task<List<IExportPlugin>> GetPlugins();
    }
}
