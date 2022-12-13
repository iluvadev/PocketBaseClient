// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using System.ComponentModel;
using System.Reflection;

namespace PocketBaseClient
{
    /// <summary>
    /// Extensions for Enums
    /// </summary>
    internal static class EnumExtensions
    {
        /// <summary>
        /// Gets the Description defined in a <seealso cref="DescriptionAttribute"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string? GetDescription<T>(this T enumValue) where T : struct, IConvertible
        {
            string? output = null;
            FieldInfo? fi = enumValue.GetType().GetField(enumValue.ToString() ?? "");
            var attrs = fi?.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
            if (attrs?.Length > 0)
                output = attrs[0].Description;
            return output;
        }

        /// <summary>
        /// Gets the enum values with their Descriptions defined in a <seealso cref="DescriptionAttribute"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static IDictionary<T, string?> GetEnumValuesWithDescription<T>(this Type type) where T : struct, IConvertible
        {
            if (!type.IsEnum) throw new ArgumentException("T must be an enumerated type");

            return type.GetEnumValues().OfType<T>().ToDictionary(key => key, val => val.GetDescription());
        }
    }
}
