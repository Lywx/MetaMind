namespace MetaMind.Engine.Extensions
{
    using System.Collections.Generic;

    public static class ExtList
    {
        public static void Swap<T>(this IList<T> list, int firstIndex, int secondIndex)
        {
            var temp = list[firstIndex];

            list[firstIndex]  = list[secondIndex];
            list[secondIndex] = temp;
        }

        public static void SwapWith<T>(this IList<T> firstList, IList<T> secondList, int firstIndex, int secondIndex)
        {
            var first  = firstList[firstIndex];
            var second = secondList[secondIndex];

            firstList[firstIndex]  = second;
            secondList[firstIndex] = first;
        }
    }
}