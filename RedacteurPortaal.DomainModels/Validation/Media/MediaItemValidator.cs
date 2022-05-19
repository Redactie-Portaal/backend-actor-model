using FluentValidation;
using RedacteurPortaal.DomainModels.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.DomainModels.Validation.Media
{
    public class MediaItemValidator : AbstractValidator<MediaItem>
    {
        public MediaItemValidator()
        {
            this.RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
            this.RuleFor(x => x.Folder).NotEmpty().WithMessage("Folder is required");
            this.RuleFor(x => x.RepublishDate).NotEmpty().WithMessage("Republish date is required").GreaterThan(DateTime.MinValue).WithMessage("Republish date cannot be that far in the past");
            this.RuleFor(x => x.Rights).NotEmpty().WithMessage("Rights is required");
            this.RuleFor(x => x.Camera).NotEmpty().WithMessage("Camera is required");
            this.RuleFor(x => x.LastWords).NotEmpty().WithMessage("Last words is required");
            this.RuleFor(x => x.ProxyFile).NotEmpty().WithMessage("Proxy file is required");
            this.RuleFor(x => x.Presentation).NotEmpty().WithMessage("Presentation is required");
            this.RuleFor(x => x.Location).NotEmpty().WithMessage("Location is required");
            this.RuleFor(x => x.Format).NotEmpty().WithMessage("Format is required");
            this.RuleFor(x => x.MediaLocation).NotEmpty().WithMessage("Media location is required");
        }
    }
}
