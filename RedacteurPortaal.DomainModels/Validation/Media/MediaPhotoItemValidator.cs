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
            this.RuleFor(x => x.Location).NotNull().DependentRules(() =>
            {
                this.RuleFor(x => x.Location.Latitude).NotNull().WithMessage("Latitude is required");
                this.RuleFor(x => x.Location.Longitude).NotNull().WithMessage("Longitude is required");
                this.RuleFor(x => x.Location.Name).NotEmpty().WithMessage("LocationName is required");
                this.RuleFor(x => x.Location.Street).NotEmpty().WithMessage("Street is required");
                this.RuleFor(x => x.Location.City).NotEmpty().WithMessage("City is required");
                this.RuleFor(x => x.Location.Province).NotEmpty().WithMessage("Country is required");
                this.RuleFor(x => x.Location.Zip).NotEmpty().WithMessage("PostalCode is required");
            });            
        }
    }
}
