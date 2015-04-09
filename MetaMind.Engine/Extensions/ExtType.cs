// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExtType.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Extensions
{
    using System;
    using System.Linq;
    using System.Reflection;

    public static class ExtType
    {
        public static bool HasProperty(this Type type, string name)
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Any(p => p.Name == name);
        }
    }
}