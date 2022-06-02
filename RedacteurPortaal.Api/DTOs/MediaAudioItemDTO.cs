using RedacteurPortaal.Api.DTOs;
using RedacteurPortaal.DomainModels.Media;

namespace RedacteurPortaal.Api.DTOs
{
    public class MediaAudioItemDto : MediaItemDto
    {
        public TimeSpan DurationSeconds { get; set; }

        public Weather Weather { get; set; }

        public string FirstWords { get; set; }

        public string ProgramName { get; set; }
    }
}