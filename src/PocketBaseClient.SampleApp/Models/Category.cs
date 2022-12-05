
// This file was generated automatically on 5/12/2022 21:47:57(UTC) from the PocketBase schema for Application orm-csharp-test (https://orm-csharp-test.pockethost.io)
//
// PocketBaseClient-csharp project: https://github.com/iluvadev/PocketBaseClient-csharp
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using pocketbase_csharp_sdk.Json;
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
    public partial class Category : ItemBase
    {
        private string? _Name = null;
        [JsonPropertyName("name")]
        [PocketBaseField(id: "p221scv1", name: "name", required: false, system: false, unique: false, type: "text")]
        public string? Name
        {
           get => Get(() => _Name);
           set => Set(value, ref _Name);
        }


        public override void UpdateWith(ItemBase itemBase)
        {
            StartUpdate(itemBase);

            if (itemBase is Category item)
            {
                Name = item.Name;

            }

            EndUpdate();
        }

        public override string ToString()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            return JsonSerializer.Serialize(this, options);
        }
    }
}
