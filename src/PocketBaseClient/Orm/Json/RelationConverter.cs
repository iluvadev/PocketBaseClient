using PocketBaseClient.Services;
using System.Net.Mail;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PocketBaseClient.Orm.Json
{
    public class RelationConverter<T> : JsonConverter<T?>
        where T : ItemBase, new()
    {
        public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            if (value == null)
                return null;
            return DataServiceBase.GetCollection<T>()!.AddOrGetById(value);
        }

        public override void Write(Utf8JsonWriter writer, T? value, JsonSerializerOptions options)
        {
            if (value is null)
                writer.WriteNullValue();
            else
                writer.WriteStringValue(value.Id);
        }
    }
}
