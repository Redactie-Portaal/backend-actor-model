using FluentValidation;
using RedacteurPortaal.DomainModels.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.DomainModels.Validation.Media
{
    public class MediaVideoItemValidator : AbstractValidator<MediaVideoItem>
    {
        public MediaVideoItemValidator()
        {
            this.RuleFor(x => x.Reporter).NotEmpty().WithMessage("Reporter is required");
            this.RuleFor(x => x.Sound).NotEmpty().WithMessage("Sound is required");
            this.RuleFor(x => x.Editor).NotEmpty().WithMessage("Editor is required");
            this.RuleFor(x => x.LastPicture).NotEmpty().WithMessage("LastPicture is required");
            this.RuleFor(x => x.Keywords).NotEmpty().WithMessage("Keywords is required");
            this.RuleFor(x => x.VoiceOver).NotEmpty().WithMessage("VoiceOver is required");
            this.RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
            this.RuleFor(x => x.ProgramDate).NotEmpty().WithMessage("ProgramDate is required");
            this.RuleFor(x => x.ItemName).NotEmpty().WithMessage("ItemName is required");
            this.RuleFor(x => x.EPG).NotEmpty().WithMessage("EPG is required");
            this.RuleFor(x => x.Duration).NotEmpty().WithMessage("Duration is required");
            this.RuleFor(x => x.ArchiveMaterial).NotEmpty().WithMessage("ArchiveMaterial is required");
            this.RuleFor(x => x.Producer).NotEmpty().WithMessage("Producer is required");
            this.RuleFor(x => x.Director).NotEmpty().WithMessage("Director is required");
            this.RuleFor(x => x.Guests).NotEmpty().WithMessage("Guests is required");
            this.RuleFor(x => x.FirstWords).NotEmpty().WithMessage("First words is required");
            this.RuleFor(x => x.ProgramName).NotEmpty().WithMessage("Program name is required");
            this.RuleFor(x => x.Location).NotNull().DependentRules(() => {
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
