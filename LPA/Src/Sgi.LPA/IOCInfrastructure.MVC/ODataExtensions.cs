//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace IOCInfrastructure.MVC
//{
//    public static class ODataExtensions
//    {
//        public static IEnumerable<T> ApplyFilter<T>(this IQueryable<T> query, ODataQueryOptions<T> options)
//        {
//            if (options.Filter == null)
//            {
//                return query;
//            }
//            var settings = new ODataQuerySettings();
//            return options.Filter.ApplyTo(query, settings) as IEnumerable<T>;
//        }
//        public static IEnumerable<T> ApplyOrderBy<T>(this IQueryable<T> query, ODataQueryOptions<T> options)
//        {
//            if (options.OrderBy == null)
//            {
//                return query;
//            }
//            return options.OrderBy.ApplyTo(query) as IEnumerable<T>;
//        }
//        public static IEnumerable<T> ApplyTopAndTake<T>(this IEnumerable<T> query, ODataQueryOptions<T> options)
//        {
//            IEnumerable<T> value = query;

//            if (options.Skip != null)
//            {
//                value = value.Skip(options.Skip.Value);
//            }
//            if (options.Top != null)
//            {
//                value = value.Take(options.Top.Value);
//            }
//            return value;
//        }
//    }
//}