using System.Text.Json;
using System.Text.Json.Serialization;

namespace PocketBaseClient.Orm.Json
{
    public class EnumConverter<T> : JsonConverter<T?> where T : struct, IConvertible
    {
        public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            var valuesDesc = typeof(T).GetEnumValuesWithDescription<T>();

            return valuesDesc?.FirstOrDefault(kvp => kvp.Value == value).Key;
        }

        public override void Write(Utf8JsonWriter writer, T? value, JsonSerializerOptions options)
        {
            if (value is null)
                writer.WriteNullValue();
            else
                writer.WriteStringValue(value.Value.GetDescription());
        }
    }
}
