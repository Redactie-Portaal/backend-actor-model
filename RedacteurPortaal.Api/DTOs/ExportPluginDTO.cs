namespace RedacteurPortaal.Api.Models
{
    public class ExportPluginDto
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Icon { get; set; }

        public string[]? RequiredInfo { get; set; }

        public string? ApiKeyFormat { get; set; }
    }
}
