using System;
using System.Collections.Generic;

namespace developwithpassion.bdddoc.utility
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> one_at_a_time<T>(this IEnumerable<T> items)
        {
            foreach (var t in items) yield return t;
        }

        public static void each<T>(this IEnumerable<T> items, Action<T> work)
        {
            foreach (var t in items) work(t);
        }

        public static IEnumerable<TypeOfOutput> for_each<TypeOfInput,TypeOfOutput>(this IEnumerable<TypeOfInput> items, Func<TypeOfInput,TypeOfOutput> work)
        {
            foreach (var item in items) yield return work(item);
        }

        public static TypeOfItem first<TypeOfItem>(this IEnumerable<TypeOfItem> items) where TypeOfItem : class
        {
            foreach (var item in items)
            {
                return item;
            }
            return null;
        }
    }
}
