using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using PocketBaseClient.Services;

namespace PocketBaseClient.Orm.Json
{
    public class CollectionIdConverter : JsonConverter<CollectionBase?>
    {
        public override CollectionBase? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return DataServiceBase.GetCollectionById(value);
        }

        public override void Write(Utf8JsonWriter writer, CollectionBase? value, JsonSerializerOptions options)
        {
            if (value is null)
                writer.WriteNullValue();
            else
                writer.WriteStringValue(value.Id);
        }
    }
}
