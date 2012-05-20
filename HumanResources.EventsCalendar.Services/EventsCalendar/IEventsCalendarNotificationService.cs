using System.Collections.Generic;
using Microsoft.SharePoint;

namespace HumanResources.EventsCalendar.Services.EventsCalendar
{
    public interface IEventsCalendarNotificationService
    {
        void SendUpcomingEventsNotifications(SPWeb web, IEnumerable<EventsCalendarReport> reports);
        void SendNewEventsNotifications(SPWeb web, IEnumerable<EventsCalendarReport> reports);
    }
}