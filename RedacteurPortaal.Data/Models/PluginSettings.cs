using System.ComponentModel.DataAnnotations;

namespace RedacteurPortaal.Data.Models
{
    public class PluginSettings
    {
        [Key]
        public Guid PluginId { get; set; }

        public string? ApiKey { get; set; }
    }
}
