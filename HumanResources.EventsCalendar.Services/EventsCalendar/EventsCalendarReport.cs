using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using HumanResources.EventsCalendar.Services.Repository.EventsCalendar;

namespace HumanResources.EventsCalendar.Services.EventsCalendar
{
    public class EventsCalendarReport
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string EmailTo { get; set; }
        public IEnumerable<TargetEvents> Events { get; set; }
    }
}