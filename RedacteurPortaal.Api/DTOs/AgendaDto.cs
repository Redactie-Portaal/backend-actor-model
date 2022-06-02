namespace RedacteurPortaal.Api.DTOs
{
    public class AgendaDto
    {
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}