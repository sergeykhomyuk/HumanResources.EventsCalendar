using System;
using System.Collections.Generic;
using CamlexNET;
using Microsoft.SharePoint;

namespace HumanResources.EventsCalendar.Services.Repository.EventsCalendar
{
    public interface IEventsCalendarRepository
    {
        IEnumerable<EventsCalendarEntity> GetEventsInRange(SPWeb web, DateTime fromDate, DateTime toDate);
        IEnumerable<EventsCalendarEntity> GetWeekEvents(SPWeb web, DateTime weekDate);
        IEnumerable<EventsCalendarEntity> GetEventsCreatedInRange(SPWeb web, DateTime fromDate, DateTime toDate);
    }
}