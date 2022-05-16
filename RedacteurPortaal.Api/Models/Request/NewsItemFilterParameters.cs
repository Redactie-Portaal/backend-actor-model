using System.Runtime.InteropServices;

namespace RedacteurPortaal.Api.Models.Request
{
    public class NewsItemFilterParameters
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string? Author { get; set; }

        public string? Status { get; set; }
    }
}