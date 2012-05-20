using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Framework.SharePoint.Repository;
using Framework.SharePoint.Repository.Item;
using Microsoft.SharePoint;
using System.Linq;
using CamlexNET;

namespace HumanResources.EventsCalendar.Services.Repository.EventsCalendar
{
    public class EventsCalendarRepository : ListRepository, IEventsCalendarRepository
    {
        public EventsCalendarRepository()
        {
            Definition = new EventsCalendarDefinition();
        }

        #region Implementation of IEventsCalendarRepository

        public IEnumerable<EventsCalendarEntity> GetEventsInRange(SPWeb web, DateTime fromDate, DateTime toDate)
        {
            return GetEventsInRangeByCustomField(web, EventsCalendarFields.EventDate, fromDate, toDate);
        }

        public IEnumerable<EventsCalendarEntity> GetWeekEvents(SPWeb web, DateTime weekDate)
        {
            var recurrenceEntities = GetRecurrenceWeekEvents(web, weekDate);

            var nonRecurrenceEntities = GetEventsInRangeByCustomField(web, EventsCalendarFields.EventDate, weekDate,
                                                                      weekDate.AddDays(7));

            var entities = new List<EventsCalendarEntity>();
            entities.AddRange(recurrenceEntities);
            entities.AddRange(nonRecurrenceEntities);

            return entities;
        }

        public IEnumerable<EventsCalendarEntity> GetEventsCreatedInRange(SPWeb web, DateTime fromDate, DateTime toDate)
        {
            return GetEventsInRangeByCustomField(web, ItemFields.Created, fromDate, toDate);
        }

        #endregion

        private EventsCalendarEntity MapItemToEntity(SPListItem item)
        {
            var entity =
                new EventsCalendarEntity
                    {
                        Id = item.ID,
                        Title = item.Title,
                        Category = item[EventsCalendarFields.Category] as string,
                        Description = item[EventsCalendarFields.Description] as string,
                        TargetAudience =
                            MapMultiChoiceValueToCollection(item[EventsCalendarFields.TargetAudience].ToString()).
                            ToArray(),
                        Location = item[EventsCalendarFields.Location] as string,
                        EventDate = (DateTime) item[EventsCalendarFields.EventDate],
                        Link =
                            item[EventsCalendarFields.Link] != null &&
                            !string.IsNullOrEmpty(item[EventsCalendarFields.Link].ToString())
                                ? new SPFieldUrlValue(item[EventsCalendarFields.Link].ToString()).Url
                                : string.Empty
                    };
            return entity;
        }

        private IEnumerable<EventsCalendarEntity> GetEventsInRangeByCustomField(SPWeb web, string dateFieldName, DateTime fromDate, DateTime toDate)
        {
            Expression<Func<SPListItem, bool>> condition =
                item =>
                (DateTime)item[dateFieldName] >= fromDate.IncludeTimeValue() &&
                (DateTime)item[dateFieldName] <= toDate.IncludeTimeValue();

            var items = Query(web, condition);

            var entities = from SPListItem item in items select MapItemToEntity(item);

            return entities;
        }

        private IEnumerable<EventsCalendarEntity> GetRecurrenceWeekEvents(SPWeb web, DateTime weekDate)
        {
            var query = new SPQuery();
            query.Query =
                @"<Where>
                    <DateRangesOverlap>
                        <FieldRef Name='EventDate' />
                        <FieldRef Name='EndDate' />
                        <FieldRef Name='RecurrenceID' />
                        <Value Type='DateTime'><Week /></Value>
                    </DateRangesOverlap>
                </Where>";
            query.ExpandRecurrence = true;
            query.CalendarDate = weekDate;
            var recurrenceItems = Query(web, query);

            var entities = from SPListItem item in recurrenceItems select MapItemToEntity(item);
            return entities;
        }
    }
}