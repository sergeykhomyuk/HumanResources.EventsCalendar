using System.Collections.Generic;
using HumanResources.EventsCalendar.Services.Repository.EventsCalendarSubscriber;
using Microsoft.SharePoint;

namespace HumanResources.EventsCalendar.Services.EventsCalendarSubscriber
{
    public interface IEventsCalendarSubscriberService
    {
        EventsCalendarSubscriberEntity GetByAuthor(SPWeb web, SPUser author);
        void Subscribe(SPWeb web, string email, IEnumerable<string> targets, bool notifyOfNewEvents, SPUser author);
        void Update(SPWeb web, string email, IEnumerable<string> targets, bool notifyOfNewEvents, SPUser author);
        void Unsubscribe(SPWeb web, SPUser user);
        IEnumerable<EventsCalendarSubscriberEntity> GetSubscribers(SPWeb web);
        IEnumerable<EventsCalendarSubscriberEntity> GetNewEventsSubscribers(SPWeb web);
    }
}