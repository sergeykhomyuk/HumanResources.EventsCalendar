using System;
using System.Collections.Generic;
using Framework.Extensions;
using Framework.SharePoint.Services.Email;
using HumanResources.EventsCalendar.Services.EventsCalendarSubscriber;
using HumanResources.EventsCalendar.Services.Repository.EventsCalendar;
using HumanResources.EventsCalendar.Services.Repository.EventsCalendarSubscriber;
using Microsoft.SharePoint;
using System.Linq;

namespace HumanResources.EventsCalendar.Services.EventsCalendar
{
    public class EventsCalendarReportService : IEventsCalendarReportService
    {
        private readonly IEventsCalendarRepository _repository;
        private readonly IEventsCalendarSubscriberService _subscriberService;

        public EventsCalendarReportService(IEventsCalendarRepository repository, IEventsCalendarSubscriberService subscriberService)
        {
            if (repository == null) throw new ArgumentNullException("repository");
            if (subscriberService == null) throw new ArgumentNullException("subscriberService");
            
            _repository = repository;
            _subscriberService = subscriberService;
        }

        public EventsCalendarReportService()
        {
            _repository = new EventsCalendarRepository();
            _subscriberService = new EventsCalendarSubscriberService();
        }

        #region Implementation of IEventsCalendarReportService

        public IEnumerable<EventsCalendarReport> GenerateUpcomingEventsReport(SPWeb web, DateTime fromDate, DateTime toDate)
        {
            var events = _repository.GetEventsInRange(web, fromDate, toDate).OrderBy(item => item.EventDate).ToArray();
            if (!events.Any())
                return new EventsCalendarReport[0];

            var subscribers = _subscriberService.GetSubscribers(web).ToArray();
            if (!subscribers.Any())
               return new EventsCalendarReport[0];

            return GetEventsCalendarReport(web, events, subscribers, fromDate, toDate);
        }


        public IEnumerable<EventsCalendarReport> GenerateNewEventsReport(SPWeb web, DateTime fromDate, DateTime toDate)
        {
            var events = _repository.GetEventsCreatedInRange(web, fromDate, toDate).OrderBy(item => item.EventDate).ToArray();
            if (!events.Any())
                return new EventsCalendarReport[0];

            var subscribers = _subscriberService.GetNewEventsSubscribers(web).ToArray();
            if (!subscribers.Any())
                return new EventsCalendarReport[0];

            return GetEventsCalendarReport(web, events, subscribers, fromDate, toDate);
        }

        #endregion

        private IEnumerable<EventsCalendarReport> GetEventsCalendarReport(SPWeb web, EventsCalendarEntity[] events, IEnumerable<EventsCalendarSubscriberEntity> subscribers, DateTime fromDate, DateTime toDate)
        {
            foreach (var subscriber in subscribers)
            {
                var subscriberEvents =
                    events.Where(item => item.TargetAudience.HasIntersections(subscriber.TargetAudience)).ToArray();
                if (!subscriberEvents.Any())
                    continue;

                var reportEvents = GetSubscriberTargetEvents(subscriber.TargetAudience, subscriberEvents);
                var subscriberReport = new EventsCalendarReport
                {
                    EmailTo = subscriber.Email,
                    FromDate = fromDate,
                    ToDate = toDate,
                    Events = reportEvents
                };

                yield return subscriberReport;
            }
        }

        private static IEnumerable<TargetEvents> GetSubscriberTargetEvents(IEnumerable<string> subscriberTargetAudience, EventsCalendarEntity[] subscriberEvents)
        {
            foreach (var targetAudience in subscriberTargetAudience)
            {
                var targetEvents = subscriberEvents.Where(item => item.TargetAudience.Contains(targetAudience));
                if (!targetEvents.Any())
                    continue;

                var reportEventsEntry = new TargetEvents { Target = targetAudience, Events = targetEvents };
                yield return reportEventsEntry;
            }
        }
    }
}