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
    public class EnumListConverter<L, T> : JsonConverter<L?>
        where L : IList<T>, new()
        where T : struct, IConvertible
    {
        public override L? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            L valueList = new();
            var strArray = JsonSerializer.Deserialize<string[]>(ref reader, options) ?? Array.Empty<string>();

            var valuesDesc = typeof(T).GetEnumValuesWithDescription<T>();
            foreach (var str in strArray)
            {
                var value = valuesDesc?.FirstOrDefault(kvp => kvp.Value == str).Key;
                if (value != null)
                    valueList.Add(value.Value);
            }
            return valueList;
        }

        public override void Write(Utf8JsonWriter writer, L? value, JsonSerializerOptions options)
        {
            if (value is null)
                writer.WriteNullValue();
            else
            {
                writer.WriteStartArray();
                foreach (var val in value!)
                    writer.WriteStringValue(val.GetDescription());
                writer.WriteEndArray();
            }
        }
    }
}
