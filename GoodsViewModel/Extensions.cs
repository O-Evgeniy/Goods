using System;
using System.Collections.ObjectModel;

namespace GoodsViewModel
{
    public static class ObservableCollectionExtension
    {
        public static void ForEach<T>(this ObservableCollection<T> collection,Action<T> action)
        {
            foreach(var item in collection)
                action(item);
        }
    }
}
