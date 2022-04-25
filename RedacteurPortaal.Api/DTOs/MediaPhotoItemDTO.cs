using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.DomainModels.Shared;

namespace RedacteurPortaal.Api.DTOs
{
    public class MediaPhotoItemDto : MediaItemDto
    {
        public string Image { get; set; }
    }
}
