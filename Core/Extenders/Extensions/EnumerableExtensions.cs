using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TMDb.Core
{
    public static class EnumerableExtensions
    {
        public static void AddRange<T>(this ObservableCollection<T> self, IEnumerable<T> itemsToAdd)
        {
            foreach (var item in itemsToAdd)
            {
                self.Add(item);
            }
        }
    }
}
