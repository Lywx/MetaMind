namespace MetaMind.Engine.Extensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public static class ExtList
    {
        public static void AddRange<T>(this IList<T> list, IEnumerable<T> collection)
        {
            foreach (var item in collection.Where(item => !list.Contains(item)).ToArray())
            {
                list.Add(item);
            }
        }

        public static void RemoveRange<T>(this IList<T> list, IEnumerable<T> collection)
        {
            foreach (var item in collection.Where(list.Contains).ToArray())
            {
                list.Remove(item);
            }
        }

        public static void Swap<T>(this IList<T> list, int firstIndex, int secondIndex)
        {
            var temp = list[firstIndex];

            list[firstIndex] = list[secondIndex];
            list[secondIndex] = temp;
        }

        public static void SwapWith<T>(this IList<T> firstList, IList<T> secondList, int firstIndex, int secondIndex)
        {
            var first = firstList[firstIndex];
            var second = secondList[secondIndex];

            firstList[firstIndex] = second;
            secondList[firstIndex] = first;
        }

        // http://stackoverflow.com/questions/15486/sorting-an-ilist-in-c-sharp 
        public static void Sort<T>(this IList<T> list, Comparison<T> comparison)
        {
            ArrayList.Adapter((IList)list).Sort(new ComparisonComparer<T>(comparison));
        }

        // Convenience method on IEnumerable<T> to allow passing of a
        // Comparison<T> delegate to the OrderBy method.
        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> list, Comparison<T> comparison)
        {
            return list.OrderBy(t => t, new ComparisonComparer<T>(comparison));
        }

        // Wraps a generic Comparison<T> delegate in an IComparer to make it easy
        // to use a lambda expression for methods that take an IComparer or IComparer<T>
        private class ComparisonComparer<T> : IComparer<T>, IComparer
        {
            private readonly Comparison<T> comparison;

            public ComparisonComparer(Comparison<T> comparison)
            {
                this.comparison = comparison;
            }

            public int Compare(object o1, object o2)
            {
                return this.comparison((T)o1, (T)o2);
            }

            public int Compare(T x, T y)
            {
                return this.comparison(x, y);
            }
        }
    }
}
