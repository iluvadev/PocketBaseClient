// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using System.Net.Mail;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PocketBaseClient.Orm.Json
{
    public class EmailConverter : JsonConverter<MailAddress?>
    {
        public override MailAddress? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            if (value == null)
                return null;
            return new MailAddress(value);
        }

        public override void Write(Utf8JsonWriter writer, MailAddress? value, JsonSerializerOptions options)
        {
            if (value is null)
                writer.WriteNullValue();
            else
                writer.WriteStringValue(value.Address);
        }
    }
}
