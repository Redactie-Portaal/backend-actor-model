using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.Data.Models
{
    public class PluginSettings
    {
        [Key]
        public Guid PluginId { get; set; }

        public string? ApiKey { get; set; }
    }
}
