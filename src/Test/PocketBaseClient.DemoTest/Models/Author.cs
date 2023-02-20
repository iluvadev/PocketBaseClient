
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
    public partial class Author : ItemBase
    {
        #region Collection
        private static CollectionBase? _Collection = null;
        /// <inheritdoc />
        [JsonIgnore]
        public override CollectionBase Collection => _Collection ??= DataServiceBase.GetCollection<Author>()!;
        #endregion Collection

        #region Field Properties
        private string? _Name = null;
        /// <summary> Maps to 'name' field in PocketBase </summary>
        [JsonPropertyName("name")]
        [PocketBaseField(id: "vxfcwb67", name: "name", required: true, system: false, unique: false, type: "text")]
        [Display(Name = "Name")]
        [Required(ErrorMessage = @"Name is required")]
        public string Name { get => Get(() => _Name ??= string.Empty); set => Set(value, ref _Name); }

        private MailAddress? _Email = null;
        /// <summary> Maps to 'email' field in PocketBase </summary>
        [JsonPropertyName("email")]
        [PocketBaseField(id: "47aw4yhp", name: "email", required: false, system: false, unique: false, type: "email")]
        [Display(Name = "Email")]
        [JsonConverter(typeof(EmailConverter))]
        public MailAddress? Email { get => Get(() => _Email); set => Set(value, ref _Email); }

        private Uri? _Url = null;
        /// <summary> Maps to 'url' field in PocketBase </summary>
        [JsonPropertyName("url")]
        [PocketBaseField(id: "pm9srne2", name: "url", required: false, system: false, unique: false, type: "url")]
        [Display(Name = "Url")]
        [JsonConverter(typeof(UrlConverter))]
        public Uri? Url { get => Get(() => _Url); set => Set(value, ref _Url); }

        private string? _Profile = null;
        /// <summary> Maps to 'profile' field in PocketBase </summary>
        [JsonPropertyName("profile")]
        [PocketBaseField(id: "oiphi3xd", name: "profile", required: false, system: false, unique: false, type: "text")]
        [Display(Name = "Profile")]
        public string? Profile { get => Get(() => _Profile); set => Set(value, ref _Profile); }

        #endregion Field Properties

        /// <inheritdoc />
        public override void UpdateWith(ItemBase itemBase)
        {
            // Do not Update with this instance
            if (ReferenceEquals(this, itemBase)) return;

            base.UpdateWith(itemBase);

            if (itemBase is Author item)
            {
                Name = item.Name;
                Email = item.Email;
                Url = item.Url;
                Profile = item.Profile;

            }
        }

        #region Constructors

        public Author() : base()
        {
        }

        [JsonConstructor]
        public Author(string? id, DateTime? created, DateTime? updated, string @name, MailAddress? @email, Uri? @url, string? @profile)
            : base(id, created, updated)
        {
            this.Name = @name;
            this.Email = @email;
            this.Url = @url;
            this.Profile = @profile;

            AddInternal(this);
        }
        #endregion

        #region Collection
        public static CollectionAuthors GetCollection() 
            => (CollectionAuthors)DataServiceBase.GetCollection<Author>()!;
        #endregion Collection

        public static async Task<Author?> GetByIdAsync(string id, bool reload = false)
            => await GetCollection().GetByIdAsync(id, reload);

        public static Author? GetById(string id, bool reload = false)
            => GetCollection().GetById(id, reload);
    }
}
