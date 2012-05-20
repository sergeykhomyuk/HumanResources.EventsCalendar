using System;
using Microsoft.SharePoint;

namespace Framework.SharePoint.Extensions
{
    public static class SPListCollectionExtension
    {
        public static bool Exists(this SPListCollection lists, string listName)
        {
            if (string.IsNullOrEmpty(listName))
                throw new ArgumentNullException("listName");

            SPList list;
            try
            {
                list = lists.TryGetList(listName);

            }
            catch (ArgumentException)
            {
                list = null;
            }

            return list != null ? true : false;
        }
    }
}