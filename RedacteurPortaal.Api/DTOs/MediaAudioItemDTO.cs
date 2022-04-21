using RedacteurPortaal.DomainModels.Media;

namespace RedacteurPortaal.Api.Models
{
    public class MediaAudioItemDto
    {
        public int DurationSeconds { get; set; }

        public Weather Weather { get; set; }

        public string FirstWords { get; set; }

        public string ProgramName { get; set; }
    }
}