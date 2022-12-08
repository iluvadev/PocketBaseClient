
// This file was generated automatically on 8/12/2022 19:37:08(UTC) from the PocketBase schema for Application orm-csharp-test (https://orm-csharp-test.pockethost.io)
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
    public partial class User : ItemBase
    {
        #region Collection
        private static CollectionBase? _Collection = null;
        [JsonIgnore]
        public override CollectionBase Collection => _Collection ??= DataServiceBase.GetCollection<User>()!;
        #endregion Collection

        #region Field Properties
        private string? _Name = null;
        [JsonPropertyName("name")]
        [PocketBaseField(id: "users_name", name: "name", required: false, system: false, unique: false, type: "text")]
        public string? Name
        {
           get => Get(() => _Name);
           set => Set(value, ref _Name);
        }

        private object? _Avatar = null;
        [JsonPropertyName("avatar")]
        [PocketBaseField(id: "users_avatar", name: "avatar", required: false, system: false, unique: false, type: "file")]
        public object? Avatar
        {
           get => Get(() => _Avatar);
           set => Set(value, ref _Avatar);
        }

        private Uri? _Url = null;
        [JsonPropertyName("url")]
        [PocketBaseField(id: "3wsfdiz3", name: "url", required: false, system: false, unique: false, type: "url")]
        [JsonConverter(typeof(UrlConverter))]
        public Uri? Url
        {
           get => Get(() => _Url);
           set => Set(value, ref _Url);
        }


        #endregion Field Properties

        public override void UpdateWith(ItemBase itemBase)
        {
            base.UpdateWith(itemBase);

            if (itemBase is User item)
            {
                Name = item.Name;
                Avatar = item.Avatar;
                Url = item.Url;

            }
        }

        public override string ToString()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            return JsonSerializer.Serialize(this, options);
        }

        #region Collection
        public static CollectionUsers GetCollection() 
            => (CollectionUsers)DataServiceBase.GetCollection<User>()!;
        #endregion Collection


        #region GetById
        public static User? GetById(string id, bool reload = false) 
            => GetByIdAsync(id, reload).Result;

        public static async Task<User?> GetByIdAsync(string id, bool reload = false)
            => await DataServiceBase.GetCollection<User>()!.GetByIdAsync(id, reload);
        #endregion GetById
    }
}
