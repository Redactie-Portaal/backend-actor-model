namespace RedacteurPortaal.DomainModels.Agenda
{
    public class AgendaModel : IBaseEntity
    {
        public AgendaModel()
        {
        }

        public AgendaModel(Guid id, DateTime startDate, DateTime endDate,string title, string description, string userId)
        {
            this.Id = id;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Title = title;
            this.Description = description;
            this.UserId = userId;
        }

        public Guid Id { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }
    }
}