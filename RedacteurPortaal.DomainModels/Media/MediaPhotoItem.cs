namespace RedacteurPortaal.DomainModels.Media
{
    public class MediaPhotoItem : MediaItem
    {
        string Image { get; }

        public MediaPhotoItem(
            Guid guid,
            Guid newsItemGuid,
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
            string image
            )
            : base(
               guid,
               newsItemGuid,
               title,
               folder,
               republishDate,
               rights,
               camera,
               lastWords,
               proxyFile,
               presentation,
               location,
               format
             )
        {
            Image = image;
        }
    }
}