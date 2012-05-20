using Framework.SharePoint.Repository;

namespace HumanResources.EventsCalendar.Services.Repository.EventsCalendarSubscriber
{
    public class EventsCalendarSubscriberDefinition : ListDefinition
    {
        public const string EventsCalendarSubscriberListName = "Events Calendar Subscriber";

        public EventsCalendarSubscriberDefinition()
            : base(EventsCalendarSubscriberListName)
        {
        }
    }
}