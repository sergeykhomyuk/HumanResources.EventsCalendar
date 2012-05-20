using System;
using System.Collections.Generic;
using Microsoft.SharePoint;

namespace HumanResources.EventsCalendar.Services.EventsCalendar
{
    public interface IEventsCalendarReportService
    {
        IEnumerable<EventsCalendarReport> GenerateUpcomingEventsReport(SPWeb web, DateTime fromDate, DateTime toDate);
        IEnumerable<EventsCalendarReport> GenerateNewEventsReport(SPWeb web, DateTime fromDate, DateTime toDate);
    }
}