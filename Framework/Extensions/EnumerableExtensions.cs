using System.Collections.Generic;
using System.Linq;

namespace Framework.Extensions
{
    public static class EnumerableExtensions
    {
         public static bool HasIntersections<T>(this IEnumerable<T> first, IEnumerable<T> second)
         {
             return first.Intersect(second).Any();
         }

         public static bool HasIntersections<T>(this IEnumerable<T> first, IEnumerable<T> second, IEqualityComparer<T> comparer)
         {
             return first.Intersect(second, comparer).Any();
         }
    }
}