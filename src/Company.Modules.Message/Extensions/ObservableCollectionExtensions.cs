using System;
using System.Collections.ObjectModel;

namespace Company.Modules.Message.Extensions
{
    public static class ObservableCollectionExtensions
    {
        public static ReadOnlyObservableCollection<T> ToReadOnly<T>(this ObservableCollection<T> collection)
        {
            return new ReadOnlyObservableCollection<T>(collection);
        }
    }
}
