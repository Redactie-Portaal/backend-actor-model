using RedacteurPortaal.DomainModels.Calendar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.Grains.GrainInterfaces
{
    public interface ICalendarGrain : IManageableGrain<CalendarModel>
    {
        Task<CalendarModel> UpdateCalendarItem(CalendarItem item)
    }
}
