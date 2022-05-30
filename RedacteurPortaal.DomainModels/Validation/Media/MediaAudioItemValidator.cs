using FluentValidation;
using RedacteurPortaal.DomainModels.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.DomainModels.Validation.Media
{
    public class MediaAudioItemValidator : AbstractValidator<MediaAudioItem>
    {
        public MediaAudioItemValidator()
        {
            this.RuleFor(x => x.FirstWords).NotEmpty().WithMessage("First words is required");
            this.RuleFor(x => x.ProgramName).NotEmpty().WithMessage("Program name is required");
        }
    }
}
