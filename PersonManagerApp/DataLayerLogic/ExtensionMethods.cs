using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DataLayerLogic
{
    public static class ExtensionMethods
    {
        public static Collection<T> ToCollection<T>(this IEnumerable<T> enumerable)
        {
            Collection<T> collection = new Collection<T>();
            foreach (T item in enumerable)
            {
                collection.Add(item);
            }
            return collection;
        }
    }
}
