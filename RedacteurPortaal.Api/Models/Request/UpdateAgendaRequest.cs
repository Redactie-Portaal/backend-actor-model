namespace RedacteurPortaal.Api.Models.Request;

public class UpdateAgendaRequest
{
    public DateTime StartDate { get; set; }
        
    public DateTime EndDate { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }
}