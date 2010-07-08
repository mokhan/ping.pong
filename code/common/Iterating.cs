using System;
using System.Collections.Generic;
using System.Linq;

namespace common
{
    static public class Iterating
    {
        static public void each<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items ?? Enumerable.Empty<T>()) action(item);
        }
    }
}