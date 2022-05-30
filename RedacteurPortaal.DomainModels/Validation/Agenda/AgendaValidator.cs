using FluentValidation;
using RedacteurPortaal.DomainModels.Agenda;

namespace RedacteurPortaal.DomainModels.Validation.Agenda
{
    public class AgendaValidator : AbstractValidator<AgendaModel>
    {
        public AgendaValidator()
        {
            this.RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
            this.RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
            this.RuleFor(x => x.StartDate).NotEmpty().WithMessage("Start date is required");
            this.RuleFor(x => x.EndDate).NotEmpty().WithMessage("End date is required");
            this.RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
        }
    }
}