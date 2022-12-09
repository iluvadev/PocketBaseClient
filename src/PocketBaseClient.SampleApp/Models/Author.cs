
// This file was generated automatically on 8/12/2022 21:42:16(UTC) from the PocketBase schema for Application orm-csharp-test (https://orm-csharp-test.pockethost.io)
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
using PocketBaseClient.Services;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PocketBaseClient.SampleApp.Models
{
    public partial class Author : ItemBase
    {
        #region Collection
        private static CollectionBase? _Collection = null;
        [JsonIgnore]
        public override CollectionBase Collection => _Collection ??= DataServiceBase.GetCollection<Author>()!;
        #endregion Collection

        #region Field Properties
        private string? _Name = null;
        [JsonPropertyName("name")]
        [PocketBaseField(id: "vxfcwb67", name: "name", required: true, system: false, unique: false, type: "text")]
        [Required(ErrorMessage = @"name is required")]
        public string? Name
        {
           get => Get(() => _Name);
           set => Set(value, ref _Name);
        }

        private MailAddress? _Email = null;
        [JsonPropertyName("email")]
        [PocketBaseField(id: "47aw4yhp", name: "email", required: false, system: false, unique: false, type: "email")]
        [JsonConverter(typeof(EmailConverter))]
        public MailAddress? Email
        {
           get => Get(() => _Email);
           set => Set(value, ref _Email);
        }

        private Uri? _Url = null;
        [JsonPropertyName("url")]
        [PocketBaseField(id: "pm9srne2", name: "url", required: false, system: false, unique: false, type: "url")]
        [JsonConverter(typeof(UrlConverter))]
        public Uri? Url
        {
           get => Get(() => _Url);
           set => Set(value, ref _Url);
        }

        private string? _Profile = null;
        [JsonPropertyName("profile")]
        [PocketBaseField(id: "oiphi3xd", name: "profile", required: false, system: false, unique: false, type: "text")]
        public string? Profile
        {
           get => Get(() => _Profile);
           set => Set(value, ref _Profile);
        }


        #endregion Field Properties

        public override void UpdateWith(ItemBase itemBase)
        {
            base.UpdateWith(itemBase);

            if (itemBase is Author item)
            {
                Name = item.Name;
                Email = item.Email;
                Url = item.Url;
                Profile = item.Profile;

            }
        }

        public override string ToString()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            return JsonSerializer.Serialize(this, options);
        }

        #region Collection
        public static CollectionAuthors GetCollection() 
            => (CollectionAuthors)DataServiceBase.GetCollection<Author>()!;
        #endregion Collection


        #region GetById
        public static Author? GetById(string id, bool reload = false) 
            => GetByIdAsync(id, reload).Result;

        public static async Task<Author?> GetByIdAsync(string id, bool reload = false)
            => await DataServiceBase.GetCollection<Author>()!.GetByIdAsync(id, reload);
        #endregion GetById
    }
}
