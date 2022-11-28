using System.ComponentModel;
using System.Reflection;

namespace PocketBaseClient
{
    public static class EnumExtensions
    {
        public static string? GetDescription<T>(this T enumValue) where T : struct, IConvertible
        {
            string? output = null;
            FieldInfo? fi = enumValue.GetType().GetField(enumValue.ToString() ?? "");
            var attrs = fi?.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
            if (attrs?.Length > 0)
                output = attrs[0].Description;
            return output;
        }

        public static IDictionary<T, string?> GetEnumValuesWithDescription<T>(this Type type) where T : struct, IConvertible
        {
            if (!type.IsEnum) throw new ArgumentException("T must be an enumerated type");

            return type.GetEnumValues().OfType<T>().ToDictionary(key => key, val => val.GetDescription());
        }
    }
}
