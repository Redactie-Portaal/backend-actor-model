using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.DomainModels.Calendar
{
    public class CalendarItem
    {
        public CalendarItem(Guid id, DateTime startDate, DateTime endDate, string title, string description, string userId, string calendarId)
        {
            this.Id = id;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Title = title;
            this.Description = description;
            this.UserId = userId;
            this.CalendarId = calendarId;
        }

        public Guid Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }
        public string CalendarId { get; set; }
    }
}
