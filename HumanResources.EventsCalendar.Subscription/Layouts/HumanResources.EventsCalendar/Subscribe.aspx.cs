using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using HumanResources.EventsCalendar.Services.Repository.EventsCalendar;
using HumanResources.EventsCalendar.Subscription.Views.SubscribeView;
using Microsoft.SharePoint.WebControls;

namespace HumanResources.EventsCalendar.Subscription.Layouts.HumanResources.EventsCalendar
{
    public partial class Subscribe : LayoutsPageBase, ISubscribeView
    {
        private SubscribePresenter _presenter;

        #region Implementation of ISubscribeView

        public string Email
        {
            get { return email.Text; }
            set { email.Text = value; }
        }

        public IEnumerable<string> Targets
        {
            get
            {
                foreach (ListItem item in target.Items)
                {
                    if (item.Selected)
                        yield return item.Value;
                }
            }
            set
            {
                foreach (ListItem item in target.Items)
                {
                    var isSelected = value.Contains(item.Value);
                    item.Selected = isSelected;
                }
            }
        }

        public bool NotifyOfNewEvents
        {
            get { return notifyOfNewEvents.Checked; }
            set { notifyOfNewEvents.Checked = value; }
        }

        public bool IsAlreadySubscribed
        {
            get { return unsubscribeButton.Visible; }
            set
            {
                if (value)
                {
                    subscribeButton.Visible = false;
                    saveButton.Visible = true;
                    unsubscribeButton.Visible = true;
                }
                else
                {
                    subscribeButton.Visible = true;
                    saveButton.Visible = false;
                    unsubscribeButton.Visible = false;
                }
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            _presenter = new SubscribePresenter(this);
            if (!IsPostBack)
                _presenter.Initialize(Web, Web.CurrentUser);
        }

        protected override void OnInit(EventArgs e)
        {
            if (IsPostBack)
                return;

            var items = EventTargetAudience.All.Select(item => new ListItem(item, item)).ToArray();
            target.Items.AddRange(items);
        }

        protected void Unsubscribe_Click(object sender, EventArgs e)
        {
            _presenter.Unsubscribe(Web, Web.CurrentUser);
        }

        protected void Subscribe_Click(object sender, EventArgs e)
        {
            if (IsValid)
                _presenter.SaveChanges(Web, Web.CurrentUser);
        }
    }
}
