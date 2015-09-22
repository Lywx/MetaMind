namespace MetaMind.Unity.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class EnumExt
    {
        public static Dictionary<T, string> ToDict<T>(this Type enumType)
        {
            if (enumType.IsEnum)
            {
                var enumValues = Enum.GetValues(typeof(T)).Cast<T>().ToList();
                var enumNames = Enum.GetNames(typeof(T)).ToList();

                return enumValues.Zip(enumNames, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v);
            }
            else
            {
                throw new InvalidOperationException("enumType is not an enumeration type.");
            }
        }
         
    }
}