using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HH.Extensions.Collections.Generic
{
    public static class GenericCollectionExtensions
    {
        public static string Flatten<T>(this IEnumerable<T> vector, string separator)
        {
            if (vector == null)
                return string.Empty;

            var result = string.Empty;
            foreach (var element in vector)
            {
                result = result + separator + element;
            }
            result = result.Remove(0, separator.Length);
            return result;
        }

        public static void ForEachOfType<TResult>(this IEnumerable items, Action<TResult> action)
        {
            foreach (var item in items.OfType<TResult>())
            {
                action.Invoke(item);
            }
        }
    }
}
