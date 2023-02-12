
// This file was generated automatically for the PocketBase Application demo-test (https://orm-csharp-test.pockethost.io)
//    See CodeGenerationSummary.txt for more details
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

namespace PocketBaseClient.DemoTest.Models
{
    public partial class Tag : ItemBase
    {
        #region Collection
        private static CollectionBase? _Collection = null;
        /// <inheritdoc />
        [JsonIgnore]
        public override CollectionBase Collection => _Collection ??= DataServiceBase.GetCollection<Tag>()!;
        #endregion Collection

        #region Field Properties
        private string? _Name = default;
        /// <summary> Maps to 'name' field in PocketBase </summary>
        [JsonPropertyName("name")]
        [PocketBaseField(id: "jdukbual", name: "name", required: true, system: false, unique: true, type: "text")]
        [Display(Name = "Name")]
        [Required(ErrorMessage = @"Name is required")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "Minimum 2, Maximum 10 characters")]
        public string Name { get => Get(() => _Name ??= string.Empty); set => Set(value, ref _Name); }

        #endregion Field Properties

        /// <inheritdoc />
        public override void UpdateWith(ItemBase itemBase)
        {
            // Do not Update with this instance
            if (ReferenceEquals(this, itemBase)) return;

            base.UpdateWith(itemBase);

            if (itemBase is Tag item)
            {
                Name = item.Name;

            }
        }

        #region Constructors

        public Tag() : base()
        {
        }

        [JsonConstructor]
        public Tag(string? id, DateTime? created, DateTime? updated, string @name)
            : base(id, created, updated)
        {
            this.Name = @name;

            AddInternal(this);
        }
        #endregion

        #region Collection
        public static CollectionTags GetCollection() 
            => (CollectionTags)DataServiceBase.GetCollection<Tag>()!;
        #endregion Collection

        public static async Task<Tag?> GetByIdAsync(string id, bool reload = false)
            => await GetCollection().GetByIdAsync(id, reload);

        public static Tag? GetById(string id, bool reload = false)
            => GetCollection().GetById(id, reload);
    }
}
