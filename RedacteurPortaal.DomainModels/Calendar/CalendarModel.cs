using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.DomainModels.Calendar
{
    public class CalendarModel : IBaseEntity
    {
        public Guid Id { get; set; }  

        public List<CalendarItem> CalendarItems { get; set; }

        public CalendarModel(Guid id, string name, List<CalendarItem> calendarItems)
        {
            this.Id = id;   
            this.CalendarItems = calendarItems;
        }
    }
}
