namespace MetaMind.Testimony.Extensions
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;

    public static class ExtObservableCollection
    {
        public static void Sort<T>(this ObservableCollection<T> collection) where T : IComparable<T>, IEquatable<T>
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