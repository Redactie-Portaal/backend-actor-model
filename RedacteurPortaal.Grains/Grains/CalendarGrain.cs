using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.DomainModels.Calendar;
using RedacteurPortaal.Grains.GrainInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.Grains.Grains
{
    public class CalendarGrain : Grain , ICalendarGrain
    {
        private readonly IPersistentState<CalendarModel> calendar;

        public CalendarGrain(
        [PersistentState("calendar", "OrleansStorage")]
        IPersistentState<CalendarModel> calendar)
        {
            this.calendar = calendar;
        }

        public async Task Delete()
        {
            await this.calendar.ClearStateAsync();
        }

        public Task<CalendarModel> Get()
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasState()
        {
            throw new NotImplementedException();
        }

        public async Task<CalendarModel> UpdateCalendarItem(CalendarItem item)
        {
            var result = this.calendar.State.CalendarItems.Find(x => x.Id == item.Id);
            if (result == null)
            {
                this.calendar.State.CalendarItems.Add(item);
            }
            else
            {
                int index = this.calendar.State.CalendarItems.IndexOf(item);
                this.calendar.State.CalendarItems[index] = item;
            }

            await this.calendar.WriteStateAsync();

            return await this.Get();
        }
    }
}
