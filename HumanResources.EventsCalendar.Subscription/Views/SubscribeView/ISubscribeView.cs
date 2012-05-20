using System.Collections.Generic;

namespace HumanResources.EventsCalendar.Subscription.Views.SubscribeView
{
    public interface ISubscribeView
    {
        string Email { get; set; }

        IEnumerable<string> Targets { get; set; }

        bool NotifyOfNewEvents { get; set; }

        bool IsAlreadySubscribed { get; set; }
    }
}