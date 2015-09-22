namespace MetaMind.Unity.Extensions
{
    using System.Collections.Generic;

    public static class HashSetExt
    {
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
        {
            return new HashSet<T>(source);
        }
    }
}
