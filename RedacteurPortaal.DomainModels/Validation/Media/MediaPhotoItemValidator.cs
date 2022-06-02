using FluentValidation;
using RedacteurPortaal.DomainModels.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.DomainModels.Validation.Media
{
    public class MediaPhotoItemValidator : AbstractValidator<MediaPhotoItem>
    {
        public MediaPhotoItemValidator()
        {
            this.RuleFor(x => x.Image).NotEmpty().WithMessage("Image is required");
        }
    }
}
