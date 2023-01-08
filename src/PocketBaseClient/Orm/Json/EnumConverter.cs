// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using System.Text.Json;
using System.Text.Json.Serialization;

namespace PocketBaseClient.Orm.Json
{
    /// <summary>
    /// Converter for select types (enums)
    /// </summary>
    /// <typeparam name="T">The enum type</typeparam>
    public class EnumConverter<T> : JsonConverter<T?> where T : struct, IConvertible
    {
        /// <inheritdoc />
        public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            var valuesDesc = typeof(T).GetEnumValuesWithDescription<T>();

            return valuesDesc?.FirstOrDefault(kvp => kvp.Value == value).Key;
        }

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, T? value, JsonSerializerOptions options)
        {
            if (value is null)
                writer.WriteNullValue();
            else
                writer.WriteStringValue(value.Value.GetDescription());
        }
    }
}
