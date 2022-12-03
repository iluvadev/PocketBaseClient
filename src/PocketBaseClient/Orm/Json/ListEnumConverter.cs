using System.Text.Json;
using System.Text.Json.Serialization;

namespace PocketBaseClient.Orm.Json
{
    public class ListEnumConverter<L,T> : JsonConverter<L?> 
        where L: IList<T>, new ()
        where T : struct, IConvertible
    {
        public override L? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            L valueList = new ();
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
