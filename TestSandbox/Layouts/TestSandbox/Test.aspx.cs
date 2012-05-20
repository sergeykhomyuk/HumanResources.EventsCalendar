using System;
using System.Diagnostics;
using HumanResources.EventsCalendar.Services.Repository.EventsCalendar;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Linq;

namespace TestSandbox.Layouts.TestSandbox
{
    public partial class Test : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Test_Click(object sender, EventArgs e)
        {
            var repository = new EventsCalendarRepository();
           // var result = repository.GetEventsInRange(Web, new DateTime(2012, 5, 1), new DateTime(2012, 5, 2, 23, 59, 59));
            var result = repository.GetWeekEvents(Web, new DateTime(2012, 5, 9));
            uxOutput.Text = string.Empty;
            uxOutput.Text = "Event: " + Environment.NewLine;
            foreach (var eventsCalendarEntity in result)
            {
                var log = "Title: " + eventsCalendarEntity.Title +
                          ", EventDate: " + eventsCalendarEntity.EventDate.ToString() +
                          ", Category: " + eventsCalendarEntity.Category +
                          ", Description: " + eventsCalendarEntity.Description +
                          ", Link: " + eventsCalendarEntity.Link +
                          ", Location: " + eventsCalendarEntity.Location +
                          ", Target: " + string.Join(", ", eventsCalendarEntity.TargetAudience.ToArray());

                Debug.Write(log);
                uxOutput.Text += log + Environment.NewLine;
            }
        }

        protected void Created_Click(object sender, EventArgs e)
        {
            var repository = new EventsCalendarRepository();
            var result = repository.GetEventsCreatedInRange(Web, new DateTime(2012, 4, 22, 12, 20, 0), new DateTime(2012, 4, 22, 23, 59, 59));
            uxOutput.Text = string.Empty;
            uxOutput.Text = "Event: " + Environment.NewLine;
            foreach (var eventsCalendarEntity in result)
            {
                var log = "Title: " + eventsCalendarEntity.Title +
                          ", EventDate: " + eventsCalendarEntity.EventDate.ToString() +
                          ", Category: " + eventsCalendarEntity.Category +
                          ", Description: " + eventsCalendarEntity.Description +
                          ", Link: " + eventsCalendarEntity.Link +
                          ", Location: " + eventsCalendarEntity.Location +
                          ", Target: " + string.Join(", ", eventsCalendarEntity.TargetAudience.ToArray());

                Debug.Write(log);
                uxOutput.Text += log + Environment.NewLine;
            }
        }
    }
}
