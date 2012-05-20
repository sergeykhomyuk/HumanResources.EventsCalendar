using System.Collections.Generic;
using Microsoft.SharePoint;

namespace HumanResources.EventsCalendar.Services.Repository.EventsCalendarSubscriber
{
    public interface IEventsCalendarSubscriberRepository
    {
        IEnumerable<EventsCalendarSubscriberEntity> GetAll(SPWeb web);

        IEnumerable<EventsCalendarSubscriberEntity> GetNewEventsSubscribers(SPWeb web);
            
        EventsCalendarSubscriberEntity GetByAuthor(SPWeb web, SPUser author);

        void Create(SPWeb web, EventsCalendarSubscriberEntity subscriber);

        void Update(SPWeb web, EventsCalendarSubscriberEntity subscriber);

        void DeleteByAuthor(SPWeb web, SPUser author);        
    }
}