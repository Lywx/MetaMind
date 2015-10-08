namespace MetaMind.Session.Extensions
{
    using System.Collections.Generic;

    public static class HashSetExtension
    {
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
        {
            return new HashSet<T>(source);
        }
    }
}
