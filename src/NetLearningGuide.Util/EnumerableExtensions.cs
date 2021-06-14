using System;
using System.Collections.Generic;

namespace NetLearningGuide.Util
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> callback)
        {
            foreach (T obj in items)
                callback(obj);
        } 
    }
}
