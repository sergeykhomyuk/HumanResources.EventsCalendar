using System.Collections.Generic;
using Framework.SharePoint.Repository.Item;
using Microsoft.SharePoint;

namespace HumanResources.EventsCalendar.Services.Repository.EventsCalendarSubscriber
{
    public class EventsCalendarSubscriberEntity : ItemEntity
    {
        public string Email { get; set; }

        public bool NotifyOfNewEvents { get; set; }

        public IEnumerable<string> TargetAudience { get; set; }

        public SPUser Author { get; set; }
    }
}