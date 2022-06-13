using FluentValidation;
using RedacteurPortaal.DomainModels.Archive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.DomainModels.Validation.Archive
{
    public class ArchiveModelValidator : AbstractValidator<ArchiveModel>
    {
        public ArchiveModelValidator()
        {
            this.RuleFor(x => x.Id).NotEmpty();
            this.RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
            this.RuleFor(x => x.Label).NotEmpty().WithMessage("Label is required");
            this.RuleFor(x => x.ArchivedDate).NotEmpty().WithMessage("ArchivedDate is required");
            this.RuleFor(x => x.Scripts).NotEmpty().WithMessage("Scripts is required");
        }
    }
}
