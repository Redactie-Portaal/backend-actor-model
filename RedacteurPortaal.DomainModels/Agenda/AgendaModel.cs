using FluentValidation;
using RedacteurPortaal.DomainModels.Validation.Agenda;
using RedacteurPortaal.DomainModels.Validation.Archive;

namespace RedacteurPortaal.DomainModels.Agenda
{
    public class AgendaModel : IBaseEntity
    {
        public AgendaModel()
        {
        }

        public AgendaModel(Guid id, DateTime startDate, DateTime endDate,string title, string description, Guid userId)
        {
            this.Id = id;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Title = title ?? throw new ArgumentNullException(nameof(title));
            this.Description = description ?? throw new ArgumentNullException(nameof(description));
            this.UserId = userId;

            new AgendaValidator().ValidateAndThrow(this);
        }

        public Guid Id { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Guid UserId { get; set; } = Guid.NewGuid();
    }
}