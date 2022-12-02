
// This file was generated automatically on 2/12/2022 23:04:23(UTC) from the PocketBase schema for Application orm-csharp-test (https://orm-csharp-test.pockethost.io)
//
// PocketBaseClient-csharp project: https://github.com/iluvadev/PocketBaseClient-csharp
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using PocketBaseClient.Orm;
using PocketBaseClient.Orm.Attributes;
using PocketBaseClient.Orm.Json;
using PocketBaseClient.Orm.Validators;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PocketBaseClient.SampleApp.Models
{
    public partial class User : ItemBase
    {
        private string? _Name = null;
        [JsonPropertyName("name")]
        [PocketBaseField("users_name", "name", false, false, false, "text")]
        public string? Name { get => Get(() => _Name); set => Set(value, ref _Name); }

        private object? _Avatar = null;
        [JsonPropertyName("avatar")]
        [PocketBaseField("users_avatar", "avatar", false, false, false, "file")]
        public object? Avatar { get => Get(() => _Avatar); set => Set(value, ref _Avatar); }

        private Uri? _Url = null;
        [JsonPropertyName("url")]
        [PocketBaseField("3wsfdiz3", "url", false, false, false, "url")]
        [JsonConverter(typeof(UrlConverter))]
        public Uri? Url { get => Get(() => _Url); set => Set(value, ref _Url); }


        public override string ToString()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            return JsonSerializer.Serialize(this, options);
        }
    }
}
