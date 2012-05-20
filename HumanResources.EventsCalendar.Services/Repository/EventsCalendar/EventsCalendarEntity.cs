using System;
using System.Collections.Generic;
using Framework.SharePoint.Repository.Item;

namespace HumanResources.EventsCalendar.Services.Repository.EventsCalendar
{
    public class EventsCalendarEntity : ItemEntity
    {
        public DateTime EventDate { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> TargetAudience { get; set; }
        public string Category { get; set; }
        public string Link { get; set; }
        public string Location { get; set; }
    }
}