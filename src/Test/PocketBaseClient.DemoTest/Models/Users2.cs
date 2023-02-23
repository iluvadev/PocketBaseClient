
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
    public partial class Users2 : ItemAuthBase
    {
        #region Collection
        private static CollectionBase? _Collection = null;
        /// <inheritdoc />
        [JsonIgnore]
        public override CollectionBase Collection => _Collection ??= DataServiceBase.GetCollection<Users2>()!;
        #endregion Collection

        #region Field Properties
        private string? _PublicName = null;
        /// <summary> Maps to 'public_name' field in PocketBase </summary>
        [JsonPropertyName("public_name")]
        [PocketBaseField(id: "2caltbcc", name: "public_name", required: false, system: false, unique: false, type: "text")]
        [Display(Name = "Public name")]
        public string? PublicName { get => Get(() => _PublicName); set => Set(value, ref _PublicName); }

        #endregion Field Properties

        /// <inheritdoc />
        public override void UpdateWith(ItemBase itemBase)
        {
            // Do not Update with this instance
            if (ReferenceEquals(this, itemBase)) return;

            base.UpdateWith(itemBase);

            if (itemBase is Users2 item)
            {
                PublicName = item.PublicName;

            }
        }

        #region Constructors

        public Users2() : base()
        {
        }

        [JsonConstructor]
        public Users2(string? id, DateTime? created, DateTime? updated, MailAddress? email, bool? emailVisibility, string? username, bool? verified, string? @publicName)
            : base(id, created, updated, email, emailVisibility, username, verified)
        {
            this.PublicName = @publicName;

            AddInternal(this);
        }
        #endregion

        #region Collection
        public static CollectionUsers2 GetCollection() 
            => (CollectionUsers2)DataServiceBase.GetCollection<Users2>()!;
        #endregion Collection

        public static async Task<Users2?> GetByIdAsync(string id, bool reload = false)
            => await GetCollection().GetByIdAsync(id, reload);

        public static Users2? GetById(string id, bool reload = false)
            => GetCollection().GetById(id, reload);
    }
}
