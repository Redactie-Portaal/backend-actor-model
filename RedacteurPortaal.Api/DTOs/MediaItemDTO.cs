using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.DomainModels.Shared;

namespace RedacteurPortaal.Api.DTOs
{
    public abstract class MediaItemDto
    { 
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Folder { get; set; }

        public DateTime RepublishDate { get; set; }

        public string Rights { get; set; }

        public string Camera { get; set; }

        public string LastWords { get; set; }

        public string ProxyFile { get; set; }

        public string Presentation { get; set; }

        public LocationDto Location { get; set; }

        public string Format { get; set; }

        public Uri MediaLocation { get; set; }
    }
}
