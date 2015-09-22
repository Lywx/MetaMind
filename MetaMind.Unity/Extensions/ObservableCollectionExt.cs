namespace MetaMind.Unity.Extensions
{
    using System;
    using System.Linq;

    public static class ObservableCollectionExt
    {
        public static void Sort<T>(this System.Collections.ObjectModel.ObservableCollection<T> collection) where T : IComparable<T>, IEquatable<T>
        {
            var sortedCollection = collection.OrderBy(x => x).ToList();

            var index = 0;
            while (index < sortedCollection.Count)
            {
                if (!collection[index].Equals(sortedCollection[index]))
                {
                    var t = collection[index];
                    
                    collection.RemoveAt(index);
                    collection.Insert(sortedCollection.IndexOf(t), t);
                }
                else
                {
                    index++;
                }
            }
        }
    }
}