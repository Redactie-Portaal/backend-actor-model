using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.DomainModels.Shared;

namespace RedacteurPortaal.Api.DTOs
{
    public class MediaItemPhotoItemDTO : MediaItemDTO
    {
        public MediaItemPhotoItemDTO(
            Guid guid,
            string title,
            string folder,
            DateTime republishDate,
            string rights,
            string camera,
            string lastWords,
            string proxyFile,
            string presentation,
            Location location,
            string format,
            Uri mediaLocation,
            string image)
            : base(
                guid,
                title,
                folder,
                republishDate,
                rights,
                camera,
                lastWords,
                proxyFile,
                presentation,
                location,
                format,
                mediaLocation)
        {
            this.Image = image;
        }

        public string Image { get; set; }
    }
}
