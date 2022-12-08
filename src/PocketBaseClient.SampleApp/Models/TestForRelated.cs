
// This file was generated automatically on 8/12/2022 0:36:23(UTC) from the PocketBase schema for Application orm-csharp-test (https://orm-csharp-test.pockethost.io)
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
    public partial class TestForRelated : ItemBase
    {
        #region Collection
        private static CollectionBase? _Collection = null;
        [JsonIgnore]
        public override CollectionBase Collection => _Collection ??= DataServiceBase.GetCollection<TestForRelated>()!;
        #endregion Collection

        #region Field Properties
        private int? _NumberUnique = null;
        [JsonPropertyName("number_unique")]
        [PocketBaseField(id: "s10g39sb", name: "number_unique", required: false, system: false, unique: true, type: "number")]
        public int? NumberUnique
        {
           get => Get(() => _NumberUnique);
           set => Set(value, ref _NumberUnique);
        }

        private int? _NumberNonempty = null;
        [JsonPropertyName("number_nonempty")]
        [PocketBaseField(id: "iy8rrkm2", name: "number_nonempty", required: true, system: false, unique: false, type: "number")]
        [Required(ErrorMessage = @"number_nonempty is required")]
        public int? NumberNonempty
        {
           get => Get(() => _NumberNonempty);
           set => Set(value, ref _NumberNonempty);
        }

        private int? _NumberNonemptyUnique = null;
        [JsonPropertyName("number_nonempty_unique")]
        [PocketBaseField(id: "mmzxqln4", name: "number_nonempty_unique", required: true, system: false, unique: true, type: "number")]
        [Required(ErrorMessage = @"number_nonempty_unique is required")]
        public int? NumberNonemptyUnique
        {
           get => Get(() => _NumberNonemptyUnique);
           set => Set(value, ref _NumberNonemptyUnique);
        }


        #endregion Field Properties

        public override void UpdateWith(ItemBase itemBase)
        {
            base.UpdateWith(itemBase);

            if (itemBase is TestForRelated item)
            {
                NumberUnique = item.NumberUnique;
                NumberNonempty = item.NumberNonempty;
                NumberNonemptyUnique = item.NumberNonemptyUnique;

            }
        }

        public override string ToString()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            return JsonSerializer.Serialize(this, options);
        }

        #region Collection
        public static CollectionTestForRelated GetCollection() 
            => (CollectionTestForRelated)DataServiceBase.GetCollection<TestForRelated>()!;
        #endregion Collection


        #region GetById
        public static TestForRelated? GetById(string id, bool reload = false) 
            => GetByIdAsync(id, reload).Result;

        public static async Task<TestForRelated?> GetByIdAsync(string id, bool reload = false)
            => await DataServiceBase.GetCollection<TestForRelated>()!.GetByIdAsync(id, reload);
        #endregion GetById
    }
}
