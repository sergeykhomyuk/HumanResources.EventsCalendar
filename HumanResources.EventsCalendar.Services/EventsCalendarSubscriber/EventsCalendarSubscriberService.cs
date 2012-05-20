using System;
using System.Collections.Generic;
using HumanResources.EventsCalendar.Services.Repository.EventsCalendar;
using HumanResources.EventsCalendar.Services.Repository.EventsCalendarSubscriber;
using Microsoft.SharePoint;
using System.Linq;

namespace HumanResources.EventsCalendar.Services.EventsCalendarSubscriber
{
    public class EventsCalendarSubscriberService : IEventsCalendarSubscriberService
    {
        private readonly IEventsCalendarSubscriberRepository _repository;

        public EventsCalendarSubscriberService(IEventsCalendarSubscriberRepository repository)
        {
            if (repository == null) throw new ArgumentNullException("repository");
            _repository = repository;
        }

        public EventsCalendarSubscriberService()
        {
            _repository = new EventsCalendarSubscriberRepository();
        }

        #region Implementation of IEventsCalendarSubscriberService

        public EventsCalendarSubscriberEntity GetByAuthor(SPWeb web, SPUser author)
        {
            return _repository.GetByAuthor(web, author);
        }

        public void Subscribe(SPWeb web, string email, IEnumerable<string> targets, bool notifyOfNewEvents, SPUser author)
        {
            var subscriber = _repository.GetByAuthor(web, author);

            var isAlreadySubscribed = subscriber != null;
            if (isAlreadySubscribed)
            {
                Update(web, subscriber, email, targets, notifyOfNewEvents, author);
                return;
            }

            subscriber = MapToEntity(email, targets, notifyOfNewEvents, author);
            _repository.Create(web, subscriber);
        }

        public void Update(SPWeb web, string email, IEnumerable<string> targets, bool notifyOfNewEvents, SPUser author)
        {
            var subscriber = GetByAuthor(web, author);
            if (subscriber == null)
                throw new InvalidOperationException(string.Format("Cannot find subscription for user '{0} ({1})'.",
                                                                  author.Name, email));

            Update(web, subscriber, email, targets, notifyOfNewEvents, author);
        }

        public void Unsubscribe(SPWeb web, SPUser user)
        {
            var isAlreadySubscribed = _repository.GetByAuthor(web, user) != null;
            if(!isAlreadySubscribed)
                return;

            _repository.DeleteByAuthor(web, user);
        }

        public IEnumerable<EventsCalendarSubscriberEntity> GetSubscribers(SPWeb web)
        {
            var subscribers = _repository.GetAll(web);
            return subscribers;
        }

        public IEnumerable<EventsCalendarSubscriberEntity> GetNewEventsSubscribers(SPWeb web)
        {
            var subscribers = _repository.GetNewEventsSubscribers(web);
            return subscribers;
        }

        #endregion

        private void Update(SPWeb web, EventsCalendarSubscriberEntity subscriber, string email, IEnumerable<string> targets, bool notifyOfNewEvents, SPUser author)
        {
            MapToEntity(subscriber, email, targets, notifyOfNewEvents, author);

            _repository.Update(web, subscriber);
        }

        private EventsCalendarSubscriberEntity MapToEntity(string email, IEnumerable<string> targets, bool notifyOfNewEvents, SPUser author)
        {
            var subscriber = new EventsCalendarSubscriberEntity();
            
            MapToEntity(subscriber, email, targets, notifyOfNewEvents, author);
            
            return subscriber;
        }

        private void MapToEntity(EventsCalendarSubscriberEntity subscriber, string email, IEnumerable<string> targets, bool notifyOfNewEvents, SPUser author)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException("email");

            var unknownTargets = targets.Where(item => !EventTargetAudience.All.Contains(item));
            if (unknownTargets.Any())
                throw new ArgumentOutOfRangeException("targets", unknownTargets.First());
            
            if (string.IsNullOrEmpty(subscriber.Title))
            {
                var title = string.Format("{0} subscription", author.Name);
                subscriber.Title = title;
            }

            subscriber.Email = email;
            subscriber.TargetAudience = targets;
            subscriber.NotifyOfNewEvents = notifyOfNewEvents;
        }
    }
}