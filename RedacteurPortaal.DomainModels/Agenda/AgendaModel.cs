namespace RedacteurPortaal.DomainModels.Agenda
{
    public class Agenda : IBaseEntity
    {
        public Agenda()
        {
        }

        public Agenda(Guid id, DateTime date, string title, string description, string userId)
        {
            this.Id = id;
            this.Date = date;
            this.Title = title;
            this.Description = description;
            this.UserId = userId;
        }

        public Guid Id { get; set; }
        
        public DateTime Date { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public string UserId { get; set; }
    }
}