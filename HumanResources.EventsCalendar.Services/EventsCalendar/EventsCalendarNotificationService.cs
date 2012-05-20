using System;
using System.Collections.Generic;
using Framework.SharePoint.Services.Email;
using Microsoft.SharePoint;

namespace HumanResources.EventsCalendar.Services.EventsCalendar
{
    public class EventsCalendarNotificationService : IEventsCalendarNotificationService
    {
        private readonly IEmailService _emailService;

        public EventsCalendarNotificationService(IEmailService emailService)
        {
            if (emailService == null) throw new ArgumentNullException("emailService");

            _emailService = emailService;
        }

        public EventsCalendarNotificationService()
        {
            _emailService = new EmailService();
        }

        #region Implementation of IEventsCalendarNotificationService

        public void SendUpcomingEventsNotifications(SPWeb web, IEnumerable<EventsCalendarReport> reports)
        {
            throw new System.NotImplementedException();
        }

        public void SendNewEventsNotifications(SPWeb web, IEnumerable<EventsCalendarReport> reports)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}