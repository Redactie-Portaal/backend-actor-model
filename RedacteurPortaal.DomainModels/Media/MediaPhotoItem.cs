using FluentValidation;
using RedacteurPortaal.DomainModels.Shared;
using RedacteurPortaal.DomainModels.Validation.Media;
using RedacteurPortaal.DomainModels.Validation.Shared;

namespace RedacteurPortaal.DomainModels.Media;

public class MediaPhotoItem : MediaItem
{
    public MediaPhotoItem()
    {
    }

    public MediaPhotoItem(
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
        this.Image = image ?? throw new ArgumentNullException(nameof(image));
        new MediaPhotoItemValidator().ValidateAndThrow(this);
        new LocationValidator().ValidateAndThrow(this.Location);
    }

    public string Image { get; private set; }
}