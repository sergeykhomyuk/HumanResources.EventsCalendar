using System;
using Framework.SharePoint.Extensions;
using Microsoft.SharePoint;

namespace Framework.SharePoint.Repository
{
    public abstract class ListDefinition
    {
        private const string TitleField = "Title";

        public string ListName { get; private set; }

        protected ListDefinition(string listName)
        {
            ListName = listName;
        }

        public SPList GetList(SPWeb web)
        {
            var list = web.Lists.TryGetList(ListName);
            if (list == null)
                throw new ArgumentException(
                    string.Format("Cannot find list '{0}' on web site '{1}'.", ListName, web.Url));

            return list;
        }

        public bool IsExists(SPWeb web)
        {
            return web.Lists.Exists(ListName);
        }

        public void HideAndMakeTitleFieldNotRequiredOnItemNewAndEditPage(SPWeb web)
        {
            var list = GetList(web);
            var title = list.Fields.GetField(TitleField);
            title.Required = false;
            title.Hidden = true;
            title.Update();
        }
    }
}