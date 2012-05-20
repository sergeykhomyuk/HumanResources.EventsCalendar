using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.SharePoint;

namespace Framework.SharePoint.Extensions
{
    public static class ListItemHelper
    {
        private static int GetFieldIndex(SPListItem listItem, string name)
        {
            foreach (SPField fld in listItem.Fields)
            {
                if (fld.InternalName == name)
                {
                    var field = typeof(SPField).GetField("m_Index", BindingFlags.Instance | BindingFlags.NonPublic);
                    return (int)field.GetValue(fld);
                }
            }
            return -1;
        }

        public static SPUser GetSPUser(SPListItem listItem, string key)
        {
            object obj = null;
            try
            {
                obj = listItem[key];
            }
            catch (Exception)
            {
                var index = -1;
                switch (key)
                {
                    case "Author":
                        index = GetFieldIndex(listItem, key);
                        obj = listItem[index]; //Ошибка шарепоинта - иногда не ищет поле Author               
                        break;
                    case "Editor":
                        index = GetFieldIndex(listItem, key);
                        obj = listItem[index]; //Ошибка шарепоинта - иногда не ищет поле Editor               
                        break;
                }

            }

            if (obj == null)
                return null;
            try
            {
                var value = new SPFieldUserValue(listItem.Web, obj.ToString());
                return value.User;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static IEnumerable<SPUser> GetSPUsers(SPListItem listItem, string key)
        {
            object obj = null;
            try
            {
                obj = listItem[key];
            }
            catch (Exception)
            {
                var index = -1;
                switch (key)
                {
                    case "Author":
                        index = GetFieldIndex(listItem, key);
                        obj = listItem[index]; //Ошибка шарепоинта - иногда не ищет поле Author               
                        break;
                    case "Editor":
                        index = GetFieldIndex(listItem, key);
                        obj = listItem[index]; //Ошибка шарепоинта - иногда не ищет поле Editor               
                        break;
                }
            }
            if (obj == null)
                return null;
            try
            {
                var value = new SPFieldUserValueCollection(listItem.Web, obj.ToString());
                return value.Select(item => item.User);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}