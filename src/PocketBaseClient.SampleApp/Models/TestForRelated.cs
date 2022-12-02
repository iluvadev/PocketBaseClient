
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
    public partial class TestForRelated : ItemBase
    {
        private int? _NumberUnique = null;
        [JsonPropertyName("number_unique")]
        [PocketBaseField("s10g39sb", "number_unique", false, false, true, "number")]
        public int? NumberUnique { get => Get(() => _NumberUnique); set => Set(value, ref _NumberUnique); }

        private int? _NumberNonempty = null;
        [JsonPropertyName("number_nonempty")]
        [PocketBaseField("iy8rrkm2", "number_nonempty", true, false, false, "number")]
        [Required(ErrorMessage = @"number_nonempty is required")]
        public int? NumberNonempty { get => Get(() => _NumberNonempty); set => Set(value, ref _NumberNonempty); }

        private int? _NumberNonemptyUnique = null;
        [JsonPropertyName("number_nonempty_unique")]
        [PocketBaseField("mmzxqln4", "number_nonempty_unique", true, false, true, "number")]
        [Required(ErrorMessage = @"number_nonempty_unique is required")]
        public int? NumberNonemptyUnique { get => Get(() => _NumberNonemptyUnique); set => Set(value, ref _NumberNonemptyUnique); }


        public override string ToString()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            return JsonSerializer.Serialize(this, options);
        }
    }
}
