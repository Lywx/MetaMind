// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExtType.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace System.Reflection
{
    using System.Linq;

    public static class TypeExtension
    {
        public static bool HasProperty(this Type type, string name)
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Any(p => p.Name == name);
        }
    }
}