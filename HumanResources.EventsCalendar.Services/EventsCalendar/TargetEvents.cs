using System.Collections.Generic;
using HumanResources.EventsCalendar.Services.Repository.EventsCalendar;

namespace HumanResources.EventsCalendar.Services.EventsCalendar
{
    public class TargetEvents
    {
        public string Target { get; set; }
        public IEnumerable<EventsCalendarEntity> Events { get; set; }
    }
}