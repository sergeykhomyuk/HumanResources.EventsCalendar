using System;
using HumanResources.EventsCalendar.Services.EventsCalendarSubscriber;
using Microsoft.SharePoint;

namespace HumanResources.EventsCalendar.Subscription.Views.SubscribeView
{
    public class SubscribePresenter
    {
        private readonly ISubscribeView _view;
        private readonly IEventsCalendarSubscriberService _service;

        public SubscribePresenter(ISubscribeView view, IEventsCalendarSubscriberService service)
        {
            if (view == null) throw new ArgumentNullException("view");
            if (service == null) throw new ArgumentNullException("service");

            _view = view;
            _service = service;
        }

        public SubscribePresenter(ISubscribeView view)
        {
            if (view == null) throw new ArgumentNullException("view");

            _view = view;
            _service = new EventsCalendarSubscriberService();
        }

        public void Initialize(SPWeb web, SPUser currentUser)
        {
            var subsription = _service.GetByAuthor(web, currentUser);
            if(subsription == null)
            {
                _view.Email = currentUser.Email;
                _view.IsAlreadySubscribed = false;
                return;
            }
                
            _view.Email = subsription.Email;
            _view.Targets = subsription.TargetAudience;
            _view.NotifyOfNewEvents = subsription.NotifyOfNewEvents;
            _view.IsAlreadySubscribed = true;
        }

        public void SaveChanges(SPWeb web, SPUser currentUser)
        {
            _service.Subscribe(web, _view.Email, _view.Targets, _view.NotifyOfNewEvents, currentUser);
            _view.IsAlreadySubscribed = true;
        }

        public void Unsubscribe(SPWeb web, SPUser currentUser)
        {
            _service.Unsubscribe(web, currentUser);
            _view.IsAlreadySubscribed = false;
        }
    }
}