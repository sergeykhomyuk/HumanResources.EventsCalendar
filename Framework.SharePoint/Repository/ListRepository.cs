using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using CamlexNET;
using CamlexNET.Interfaces;
using Microsoft.SharePoint;

namespace Framework.SharePoint.Repository
{
    public abstract class ListRepository
    {
        protected ListDefinition Definition { get; set; }

        protected ListRepository(ListDefinition definition)
        {
            Definition = definition;
        }

        protected ListRepository()
        {
        }

        protected virtual SPListItemCollection Query(SPWeb web, SPQuery query)
        {
            AssertValidWeb(web);
            AssertListExistence(web);
            AssertListDefinitionNotNull();

            var watch = new Stopwatch();
            var list = Definition.GetList(web);

            Debug.WriteLine(
                string.Format("About to run query against list '{0}': {1}", list, query.Query));

            watch.Start();
            var result = list.GetItems(query);
            watch.Stop();

            Debug.WriteLine(
                string.Format("Query against '{0}' returned {1} rows in {2} ms",
                              list, result.Count, watch.ElapsedMilliseconds));

            return result;
        }

        protected virtual SPListItemCollection Query(SPWeb web, string caml)
        {
            AssertValidCaml(caml);
            var query = new SPQuery { Query = caml };

            return Query(web, query);
        }

        protected virtual SPListItem Query(SPWeb web, int id)
        {
            AssertValidWeb(web);
            AssertListExistence(web);
            AssertListDefinitionNotNull();

            var list = Definition.GetList(web);
            var item = list.GetItemById(id);
            return item;
        }

        public virtual SPListItemCollection Query(SPWeb web, Expression<Func<SPListItem, bool>> expression)
        {
            var camlExpression = Camlex.Query().Where(expression).ToString();
            return Query(web, camlExpression);
        }

        public virtual SPListItemCollection Query(SPWeb web, Expression<Func<SPListItem, bool>> expression, Expression<Func<SPListItem, object>> orderBy)
        {
            var camlExpression = Camlex.Query().Where(expression).OrderBy(orderBy).ToString();
            return Query(web, camlExpression);
        }

        public virtual SPListItemCollection Query(SPWeb web, IQuery query)
        {
            var camlExpression = query.ToString();
            return Query(web, camlExpression);
        }

        protected virtual SPListItemCollection Query(SPWeb web)
        {
            AssertValidWeb(web);
            AssertListExistence(web);
            AssertListDefinitionNotNull();

            var watch = new Stopwatch();

            var list = Definition.GetList(web);
            watch.Start();
            var result = list.Items;
            watch.Stop();

            Debug.WriteLine(
                string.Format("Query against '{0}' returned {1} rows in {2} ms",
                              list, result.Count, watch.ElapsedMilliseconds));

            return result;
        }

        protected virtual SPListItemCollection Query(SPWeb web, string caml, uint top)
        {
            AssertValidWeb(web);
            AssertValidCaml(caml);
            AssertListExistence(web);
            AssertListDefinitionNotNull();

            var watch = new Stopwatch();
            var list = Definition.GetList(web);
            var query = new SPQuery { Query = caml, RowLimit = top };

            Debug.WriteLine(
                string.Format("About to run query against list '{0}': {1}", list, caml));

            watch.Start();
            var result = list.GetItems(query);
            watch.Stop();

            Debug.WriteLine(
                string.Format("Query against '{0}' returned {1} rows in {2} ms",
                              list, result.Count, watch.ElapsedMilliseconds));

            return result;
        }

        public virtual void DeleteAll(SPWeb web)
        {
            AssertValidWeb(web);
            AssertListExistence(web);
            AssertListDefinitionNotNull();

            var list = Definition.GetList(web);
            while (list.Items.Count > 0)
            {
                list.Items[0].Delete();
            }
        }

        public virtual void Delete(SPWeb web, int id)
        {
            AssertValidWeb(web);
            AssertListExistence(web);
            AssertListDefinitionNotNull();

            var list = Definition.GetList(web);
            var item = list.GetItemById(id);
            if (item != null)
                item.Delete();
        }

        protected void AssertListExistence(SPWeb web)
        {
            if (!Definition.IsExists(web))
                throw new ArgumentException(
                    string.Format("Cannot find list '{0}' on web site '{1}'.", Definition.ListName, web.Url));
        }

        protected void AssertListDefinitionNotNull()
        {
            if (Definition == null)
                throw new NullReferenceException(string.Format("Cannot find definition for list '{0}'.",
                                                               Definition.ListName));
        }

        protected void AssertValidCaml(string query)
        {
            if (string.IsNullOrEmpty(query))
                throw new NullReferenceException("Query string cannot be empty.");
        }

        protected void AssertValidWeb(SPWeb web)
        {
            if (web == null)
                throw new NullReferenceException("Specified web is null.");
        }


        protected IEnumerable<string> MapMultiChoiceValueToCollection(string rawValue)
        {
            var values = new SPFieldMultiChoiceValue(rawValue);
            for (int index = 0; index < values.Count; index++)
            {
                yield return values[index];
            }
        }

        protected SPFieldMultiChoiceValue MapCollectionToMultiChoiceValue(IEnumerable<string> collection)
        {
            var values = new SPFieldMultiChoiceValue();
            foreach (var item in collection)
            {
                values.Add(item);
            }

            return values;
        }
    }
}