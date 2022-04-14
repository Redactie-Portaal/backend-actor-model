using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.DomainModels.Shared;

namespace RedacteurPortaal.Api.DTOs
{
    public class MediaPhotoItemDTO : MediaItemDTO
    {
        public string Image { get; set; }
    }
}
