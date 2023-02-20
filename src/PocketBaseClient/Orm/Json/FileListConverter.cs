// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using PocketBaseClient.Orm.Structures;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PocketBaseClient.Orm.Json
{
    /// <summary>
    /// Converter for list of file type
    /// </summary>
    /// <typeparam name="L">The type of the list</typeparam>
    /// <typeparam name="T">The mapped related type</typeparam>
    public class FileListConverter<L, T> : JsonConverter<L?>
        where L : FieldFileList<T>, new()
        where T : FieldFileBase, new()
    {
        /// <inheritdoc />
        public override L? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            L valueList = new();
            var strArray = JsonSerializer.Deserialize<string[]>(ref reader, options) ?? Array.Empty<string>();

            foreach (var str in strArray)
            {
                var value = new T() { FileName = str };
                valueList.Add(value);
                valueList.OriginalFileNames.Add(str);
            }
            return valueList;
        }

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, L? value, JsonSerializerOptions options)
        {
            if (value is null)
                writer.WriteNullValue();
            else
            {
                writer.WriteStartArray();
                foreach (var val in value!)
                    writer.WriteStringValue(val.FileName);
                writer.WriteEndArray();
            }
        }
    }
}
