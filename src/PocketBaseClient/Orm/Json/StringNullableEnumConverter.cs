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
    // This class is a custom JSON converter for nullable enum types.
    // It allows for deserialization of JSON strings into nullable enum values,
    // and serialization of nullable enum values into JSON strings.
    public class StringNullableEnumConverter<T> : JsonConverter<T>
    {
        // The underlying type of the nullable enum.
        private readonly Type _underlyingType;

        // Constructor initializes the underlying type.
        public StringNullableEnumConverter()
        {
            _underlyingType = Nullable.GetUnderlyingType(typeof(T));
        }

        // Determines whether the converter can convert the specified type.
        public override bool CanConvert(Type typeToConvert)
        {
            return typeof(T).IsAssignableFrom(typeToConvert);
        }

        // Reads and converts the JSON to the type T.
        public override T Read(ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            // Get the string value from the JSON.
            string value = reader.GetString();
            // If the string is null or empty, return the default value for type T.
            if (String.IsNullOrEmpty(value)) return default;

            try
            {
                // Try to parse the string into an enum value.
                return (T)Enum.Parse(_underlyingType, value);
            }
            catch (ArgumentException ex)
            {
                // If parsing fails, throw a JsonException.
                throw new JsonException("Invalid value.", ex);
            }
        }

        // Writes the specified value as JSON.
        public override void Write(Utf8JsonWriter writer,
            T value,
            JsonSerializerOptions options)
        {
            // Write the enum value as a string.
            writer.WriteStringValue(value?.ToString());
        }
    }
}