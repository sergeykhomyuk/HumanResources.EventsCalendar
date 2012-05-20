using Framework.SharePoint.Repository;

namespace HumanResources.EventsCalendar.Services.Repository.EventsCalendar
{
    public class EventsCalendarDefinition : ListDefinition
    {
        public const string EventsCalendarListName = "Events Calendar";

        public EventsCalendarDefinition() : base(EventsCalendarListName)
        {
        }
    }
}