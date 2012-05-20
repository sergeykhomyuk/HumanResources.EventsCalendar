using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CamlexNET;
using Framework.SharePoint.Extensions;
using Framework.SharePoint.Repository;
using Framework.SharePoint.Repository.Item;
using HumanResources.EventsCalendar.Services.Repository.EventsCalendar;
using Microsoft.SharePoint;

namespace HumanResources.EventsCalendar.Services.Repository.EventsCalendarSubscriber
{
    public class EventsCalendarSubscriberRepository : ListRepository, IEventsCalendarSubscriberRepository
    {
        public EventsCalendarSubscriberRepository()
        {
            Definition = new EventsCalendarSubscriberDefinition();
        }

        #region Implementation of IEventsCalendarSubscriberRepository

        public IEnumerable<EventsCalendarSubscriberEntity> GetAll(SPWeb web)
        {
            var items = Query(web);
            var entities = from SPListItem item in items select MapItemToEntity(item);

            return entities;
        }

        public IEnumerable<EventsCalendarSubscriberEntity> GetNewEventsSubscribers(SPWeb web)
        {
            Expression<Func<SPListItem, bool>> condition =
                item => (bool) item[EventsCalendarSubscriberFields.NotifyOfNewEvents];

            var items = Query(web, condition);
            var entities = from SPListItem item in items select MapItemToEntity(item);

            return entities;
        }

        public EventsCalendarSubscriberEntity GetByAuthor(SPWeb web, SPUser author)
        {
            var item = GetItemByAuthor(web, author);
            if (item == null) 
                return null;

            var subscriber = MapItemToEntity(item);
            return subscriber;
        }

        private SPListItem GetItemByAuthor(SPWeb web, SPUser author)
        {
            Expression<Func<SPListItem, bool>> authorCondition =
                item => item[EventsCalendarSubscriberFields.Author] == (DataTypes.UserId) (author.ID.ToString());
            var items = Query(web, authorCondition);

            if (items.Count == 0)
                return null;

            return items[0];
        }

        public void Create(SPWeb web, EventsCalendarSubscriberEntity subscriber)
        {
            var list = Definition.GetList(web);
            var item = list.AddItem();

            MapEntityToItem(subscriber, item);

            item.Update();
        }

        public void Update(SPWeb web, EventsCalendarSubscriberEntity subscriber)
        {
            var item = Query(web, subscriber.Id);
            if(item == null)
                return;
            
            MapEntityToItem(subscriber, item);

            item.Update();
        }

        public void DeleteByAuthor(SPWeb web, SPUser author)
        {
            var item = GetItemByAuthor(web, author);
            if(item == null)
                return;

            item.Delete();
        }

        #endregion

        private EventsCalendarSubscriberEntity MapItemToEntity(SPListItem item)
        {
            var entity = new EventsCalendarSubscriberEntity
                             {
                                 Id = item.ID,
                                 Title = item.Title,
                                 Email = item[EventsCalendarSubscriberFields.Email] as string,
                                 NotifyOfNewEvents = (bool)item[EventsCalendarSubscriberFields.NotifyOfNewEvents],
                                 Author = ListItemHelper.GetSPUser(item, EventsCalendarSubscriberFields.Author),                                 
                                 TargetAudience = MapMultiChoiceValueToCollection(item[EventsCalendarFields.TargetAudience].ToString())                                 
                             };

            return entity;
        }


        private void MapEntityToItem(EventsCalendarSubscriberEntity entity, SPListItem item)
        {
            item[ItemFields.Title] = entity.Title;
            item[EventsCalendarSubscriberFields.Email] = entity.Email;
            item[EventsCalendarSubscriberFields.NotifyOfNewEvents] = entity.NotifyOfNewEvents;
            item[EventsCalendarFields.TargetAudience] = MapCollectionToMultiChoiceValue(entity.TargetAudience);
        }
    }
}