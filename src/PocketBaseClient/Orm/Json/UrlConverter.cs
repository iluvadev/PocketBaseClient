using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using PocketBaseClient.Services;
using System.Net.Mail;

namespace PocketBaseClient.Orm.Json
{
    public class UrlConverter : JsonConverter<Uri?>
    {
        public override Uri? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            if (value == null) 
                return null;
            return new Uri(value);
        }

        public override void Write(Utf8JsonWriter writer, Uri? value, JsonSerializerOptions options)
        {
            if (value is null)
                writer.WriteNullValue();
            else
                writer.WriteStringValue(value.ToString());
        }
    }
}
