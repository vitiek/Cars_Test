using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Threading.Tasks;

namespace WebSite.Extensions
{
    //Extension for datatables.js
    public class DataTableParamHelper
    {
        public static int? GetSearchValueInt(string name, DataTableParamModel param)
        {
            int result;

            var p = GetSearchValueString(name, param);

            if (!String.IsNullOrEmpty(p))
            {
                if (int.TryParse(p, out result))
                {
                    return result;
                }
            }

            return null;
        }

        public static string GetSearchValueString(string name, DataTableParamModel param)
        {
            var s = param.Columns.FirstOrDefault(c => String.Equals(c.Data, name, StringComparison.InvariantCultureIgnoreCase)).Search.Value;
            return String.IsNullOrEmpty(s) ? null : s;
        }

        public static IQueryable<T> OrderBys<T>(IQueryable<T> query, Type type, DataTableParamModel param)
        {
            if (param.Order != null && param.Order.Any())
            {
                var propertyInfos = type.GetProperties();

                var column = param.Order.First();
                query = query.OrderBy(propertyInfos[column.Column].Name + " " + column.Dir);
            }

            return query;
        }
    }
}
