using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

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
