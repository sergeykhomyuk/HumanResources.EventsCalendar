using System.Collections.Generic;

namespace HumanResources.EventsCalendar.Services.Repository.EventsCalendar
{
    public static class EventTargetAudience
    {
        public const string Developers = "Developers";
        public const string QA = "QA";
        public const string HR = "HR";
        public const string SystemAdministrators = "System Administrators";
        public const string Other = "Other";

        public static IEnumerable<string> All
        {
            get
            {
                yield return Developers;
                yield return QA;
                yield return HR;
                yield return SystemAdministrators;
                yield return Other;
            }
        }
    }
}