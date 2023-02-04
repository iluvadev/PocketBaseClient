
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
    public partial class TestForType : ItemBase
    {
        #region Collection
        private static CollectionBase? _Collection = null;
        /// <inheritdoc />
        [JsonIgnore]
        public override CollectionBase Collection => _Collection ??= DataServiceBase.GetCollection<TestForType>()!;
        #endregion Collection

        #region Field Properties
        private string? _TextNoRestrictions = null;
        /// <summary> Maps to 'text_no_restrictions' field in PocketBase </summary>
        [JsonPropertyName("text_no_restrictions")]
        [PocketBaseField(id: "kkyx3zk2", name: "text_no_restrictions", required: false, system: false, unique: false, type: "text")]
        [Display(Name = "Text no restrictions")]
        public string? TextNoRestrictions { get => Get(() => _TextNoRestrictions); set => Set(value, ref _TextNoRestrictions); }

        private string? _TextRestrictions = null;
        /// <summary> Maps to 'text_restrictions' field in PocketBase </summary>
        [JsonPropertyName("text_restrictions")]
        [PocketBaseField(id: "sgd9bm7z", name: "text_restrictions", required: false, system: false, unique: false, type: "text")]
        [Display(Name = "Text restrictions")]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "Minimum 5, Maximum 15 characters")]
        [RegularExpression(@"^\w+$", ErrorMessage = @"Pattern '^\w+$' not match")]
        public string? TextRestrictions { get => Get(() => _TextRestrictions); set => Set(value, ref _TextRestrictions); }

        private float? _NumberNoRestrictions = null;
        /// <summary> Maps to 'number_no_restrictions' field in PocketBase </summary>
        [JsonPropertyName("number_no_restrictions")]
        [PocketBaseField(id: "zasulmy0", name: "number_no_restrictions", required: false, system: false, unique: false, type: "number")]
        [Display(Name = "Number no restrictions")]
        public float? NumberNoRestrictions { get => Get(() => _NumberNoRestrictions); set => Set(value, ref _NumberNoRestrictions); }

        private float? _NumberRestrictions = null;
        /// <summary> Maps to 'number_restrictions' field in PocketBase </summary>
        [JsonPropertyName("number_restrictions")]
        [PocketBaseField(id: "xsz2augn", name: "number_restrictions", required: false, system: false, unique: false, type: "number")]
        [Display(Name = "Number restrictions")]
        [Range(0, 10, ErrorMessage = "Minimum 0, Maximum 10")]
        public float? NumberRestrictions { get => Get(() => _NumberRestrictions); set => Set(value, ref _NumberRestrictions); }

        private bool? _Bool = null;
        /// <summary> Maps to 'bool' field in PocketBase </summary>
        [JsonPropertyName("bool")]
        [PocketBaseField(id: "vo8jeqfr", name: "bool", required: false, system: false, unique: false, type: "bool")]
        [Display(Name = "Bool")]
        public bool? Bool { get => Get(() => _Bool); set => Set(value, ref _Bool); }

        private MailAddress? _EmailNoRestrictions = null;
        /// <summary> Maps to 'email_no_restrictions' field in PocketBase </summary>
        [JsonPropertyName("email_no_restrictions")]
        [PocketBaseField(id: "dfc3hzbp", name: "email_no_restrictions", required: false, system: false, unique: false, type: "email")]
        [Display(Name = "Email no restrictions")]
        [JsonConverter(typeof(EmailConverter))]
        public MailAddress? EmailNoRestrictions { get => Get(() => _EmailNoRestrictions); set => Set(value, ref _EmailNoRestrictions); }

        private MailAddress? _EmailRestrictionsExcept = null;
        /// <summary> Maps to 'email_restrictions_except' field in PocketBase </summary>
        [JsonPropertyName("email_restrictions_except")]
        [PocketBaseField(id: "hea5cksh", name: "email_restrictions_except", required: false, system: false, unique: false, type: "email")]
        [Display(Name = "Email restrictions except")]
        [ExceptDomains("gmail.com,hotmail.com", ErrorMessage = "Except domains accepted: 'gmail.com,hotmail.com'")]
        [JsonConverter(typeof(EmailConverter))]
        public MailAddress? EmailRestrictionsExcept { get => Get(() => _EmailRestrictionsExcept); set => Set(value, ref _EmailRestrictionsExcept); }

        private MailAddress? _EmailRestrictionsOnly = null;
        /// <summary> Maps to 'email_restrictions_only' field in PocketBase </summary>
        [JsonPropertyName("email_restrictions_only")]
        [PocketBaseField(id: "ddvywcfc", name: "email_restrictions_only", required: false, system: false, unique: false, type: "email")]
        [Display(Name = "Email restrictions only")]
        [OnlyDomains("pockethost.io", ErrorMessage = "Only domains accepted: 'pockethost.io'")]
        [JsonConverter(typeof(EmailConverter))]
        public MailAddress? EmailRestrictionsOnly { get => Get(() => _EmailRestrictionsOnly); set => Set(value, ref _EmailRestrictionsOnly); }

        private Uri? _UrlNoRestrictions = null;
        /// <summary> Maps to 'url_no_restrictions' field in PocketBase </summary>
        [JsonPropertyName("url_no_restrictions")]
        [PocketBaseField(id: "n11oh0zk", name: "url_no_restrictions", required: false, system: false, unique: false, type: "url")]
        [Display(Name = "Url no restrictions")]
        [JsonConverter(typeof(UrlConverter))]
        public Uri? UrlNoRestrictions { get => Get(() => _UrlNoRestrictions); set => Set(value, ref _UrlNoRestrictions); }

        private Uri? _UrlRestrictionsExcept = null;
        /// <summary> Maps to 'url_restrictions_except' field in PocketBase </summary>
        [JsonPropertyName("url_restrictions_except")]
        [PocketBaseField(id: "gimszuxa", name: "url_restrictions_except", required: false, system: false, unique: false, type: "url")]
        [Display(Name = "Url restrictions except")]
        [ExceptDomains("google.com", ErrorMessage = "Except domains accepted: 'google.com'")]
        [JsonConverter(typeof(UrlConverter))]
        public Uri? UrlRestrictionsExcept { get => Get(() => _UrlRestrictionsExcept); set => Set(value, ref _UrlRestrictionsExcept); }

        private Uri? _UrlRestrictionsOnly = null;
        /// <summary> Maps to 'url_restrictions_only' field in PocketBase </summary>
        [JsonPropertyName("url_restrictions_only")]
        [PocketBaseField(id: "pntohkfm", name: "url_restrictions_only", required: false, system: false, unique: false, type: "url")]
        [Display(Name = "Url restrictions only")]
        [OnlyDomains("pockethost.io", ErrorMessage = "Only domains accepted: 'pockethost.io'")]
        [JsonConverter(typeof(UrlConverter))]
        public Uri? UrlRestrictionsOnly { get => Get(() => _UrlRestrictionsOnly); set => Set(value, ref _UrlRestrictionsOnly); }

        private DateTime? _DatetimeNoRestrictions = null;
        /// <summary> Maps to 'datetime_no_restrictions' field in PocketBase </summary>
        [JsonPropertyName("datetime_no_restrictions")]
        [PocketBaseField(id: "why1vezh", name: "datetime_no_restrictions", required: false, system: false, unique: false, type: "date")]
        [Display(Name = "Datetime no restrictions")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? DatetimeNoRestrictions { get => Get(() => _DatetimeNoRestrictions); set => Set(value, ref _DatetimeNoRestrictions); }

        private DateTime? _DatetimeRestrictions = null;
        /// <summary> Maps to 'datetime_restrictions' field in PocketBase </summary>
        [JsonPropertyName("datetime_restrictions")]
        [PocketBaseField(id: "xsnujvoc", name: "datetime_restrictions", required: false, system: false, unique: false, type: "date")]
        [Display(Name = "Datetime restrictions")]
        [Range(typeof(DateTime), "1/11/2022 13:00:00", "31/12/9999 22:59:59", ErrorMessage = "Minimum '1/11/2022 13:00:00', Maximum '31/12/9999 22:59:59'")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? DatetimeRestrictions { get => Get(() => _DatetimeRestrictions); set => Set(value, ref _DatetimeRestrictions); }

        private SelectSingleEnum? _SelectSingle = null;
        /// <summary> Maps to 'select_single' field in PocketBase </summary>
        [JsonPropertyName("select_single")]
        [PocketBaseField(id: "uiv0j5vw", name: "select_single", required: false, system: false, unique: false, type: "select")]
        [Display(Name = "Select single")]
        [JsonConverter(typeof(EnumConverter<SelectSingleEnum>))]
        public SelectSingleEnum? SelectSingle { get => Get(() => _SelectSingle); set => Set(value, ref _SelectSingle); }

        private SelectMultipleList _SelectMultiple = new SelectMultipleList();
        /// <summary> Maps to 'select_multiple' field in PocketBase </summary>
        [JsonPropertyName("select_multiple")]
        [PocketBaseField(id: "8dks1xfy", name: "select_multiple", required: false, system: false, unique: false, type: "select")]
        [Display(Name = "Select multiple")]
        [JsonInclude]
        [JsonConverter(typeof(EnumListConverter<SelectMultipleList, SelectMultipleEnum>))]
        public SelectMultipleList SelectMultiple { get => Get(() => _SelectMultiple ??= new SelectMultipleList(this)); private set => Set(value, ref _SelectMultiple); }

        private dynamic? _Json = null;
        /// <summary> Maps to 'json' field in PocketBase </summary>
        [JsonPropertyName("json")]
        [PocketBaseField(id: "hmr5iih4", name: "json", required: false, system: false, unique: false, type: "json")]
        [Display(Name = "Json")]
        public dynamic? Json { get => Get(() => _Json); set => Set(value, ref _Json); }

        private object? _FileSingleNoRestriction = null;
        /// <summary> Maps to 'file_single_no_restriction' field in PocketBase </summary>
        [JsonPropertyName("file_single_no_restriction")]
        [PocketBaseField(id: "mpnfu1ph", name: "file_single_no_restriction", required: false, system: false, unique: false, type: "file")]
        [Display(Name = "File single no restriction")]
        public object? FileSingleNoRestriction { get => Get(() => _FileSingleNoRestriction); set => Set(value, ref _FileSingleNoRestriction); }

        private object? _FileSingleRestriction = null;
        /// <summary> Maps to 'file_single_restriction' field in PocketBase </summary>
        [JsonPropertyName("file_single_restriction")]
        [PocketBaseField(id: "cn4tglcr", name: "file_single_restriction", required: false, system: false, unique: false, type: "file")]
        [Display(Name = "File single restriction")]
        public object? FileSingleRestriction { get => Get(() => _FileSingleRestriction); set => Set(value, ref _FileSingleRestriction); }

        private object? _FileMultipleNoRestrictions = null;
        /// <summary> Maps to 'file_multiple_no_restrictions' field in PocketBase </summary>
        [JsonPropertyName("file_multiple_no_restrictions")]
        [PocketBaseField(id: "mqokykua", name: "file_multiple_no_restrictions", required: false, system: false, unique: false, type: "file")]
        [Display(Name = "File multiple no restrictions")]
        public object? FileMultipleNoRestrictions { get => Get(() => _FileMultipleNoRestrictions); set => Set(value, ref _FileMultipleNoRestrictions); }

        private object? _FileMultipleRestrictions = null;
        /// <summary> Maps to 'file_multiple_restrictions' field in PocketBase </summary>
        [JsonPropertyName("file_multiple_restrictions")]
        [PocketBaseField(id: "o4hs5o8n", name: "file_multiple_restrictions", required: false, system: false, unique: false, type: "file")]
        [Display(Name = "File multiple restrictions")]
        public object? FileMultipleRestrictions { get => Get(() => _FileMultipleRestrictions); set => Set(value, ref _FileMultipleRestrictions); }

        private TestForRelated? _ReationSingle = null;
        /// <summary> Maps to 'reation_single' field in PocketBase </summary>
        [JsonPropertyName("reation_single")]
        [PocketBaseField(id: "7q0qviac", name: "reation_single", required: false, system: false, unique: false, type: "relation")]
        [Display(Name = "Reation single")]
        [JsonConverter(typeof(RelationConverter<TestForRelated>))]
        public TestForRelated? ReationSingle { get => Get(() => _ReationSingle); set => Set(value, ref _ReationSingle); }

        private RelationMultipleNoLimitList _RelationMultipleNoLimit = new RelationMultipleNoLimitList();
        /// <summary> Maps to 'relation_multiple_no_limit' field in PocketBase </summary>
        [JsonPropertyName("relation_multiple_no_limit")]
        [PocketBaseField(id: "a4chtr6c", name: "relation_multiple_no_limit", required: false, system: false, unique: false, type: "relation")]
        [Display(Name = "Relation multiple no limit")]
        [JsonInclude]
        [JsonConverter(typeof(RelationListConverter<RelationMultipleNoLimitList, TestForRelated>))]
        public RelationMultipleNoLimitList RelationMultipleNoLimit { get => Get(() => _RelationMultipleNoLimit ??= new RelationMultipleNoLimitList(this)); private set => Set(value, ref _RelationMultipleNoLimit); }

        private RelationMultipleLimitList _RelationMultipleLimit = new RelationMultipleLimitList();
        /// <summary> Maps to 'relation_multiple_limit' field in PocketBase </summary>
        [JsonPropertyName("relation_multiple_limit")]
        [PocketBaseField(id: "otxwaoam", name: "relation_multiple_limit", required: false, system: false, unique: false, type: "relation")]
        [Display(Name = "Relation multiple limit")]
        [JsonInclude]
        [JsonConverter(typeof(RelationListConverter<RelationMultipleLimitList, TestForRelated>))]
        public RelationMultipleLimitList RelationMultipleLimit { get => Get(() => _RelationMultipleLimit ??= new RelationMultipleLimitList(this)); private set => Set(value, ref _RelationMultipleLimit); }

        #endregion Field Properties

        /// <inheritdoc />
        public override void UpdateWith(ItemBase itemBase)
        {
            base.UpdateWith(itemBase);

            if (itemBase is TestForType item)
            {
                TextNoRestrictions = item.TextNoRestrictions;
                TextRestrictions = item.TextRestrictions;
                NumberNoRestrictions = item.NumberNoRestrictions;
                NumberRestrictions = item.NumberRestrictions;
                Bool = item.Bool;
                EmailNoRestrictions = item.EmailNoRestrictions;
                EmailRestrictionsExcept = item.EmailRestrictionsExcept;
                EmailRestrictionsOnly = item.EmailRestrictionsOnly;
                UrlNoRestrictions = item.UrlNoRestrictions;
                UrlRestrictionsExcept = item.UrlRestrictionsExcept;
                UrlRestrictionsOnly = item.UrlRestrictionsOnly;
                DatetimeNoRestrictions = item.DatetimeNoRestrictions;
                DatetimeRestrictions = item.DatetimeRestrictions;
                SelectSingle = item.SelectSingle;
                SelectMultiple = item.SelectMultiple;
                Json = item.Json;
                FileSingleNoRestriction = item.FileSingleNoRestriction;
                FileSingleRestriction = item.FileSingleRestriction;
                FileMultipleNoRestrictions = item.FileMultipleNoRestrictions;
                FileMultipleRestrictions = item.FileMultipleRestrictions;
                ReationSingle = item.ReationSingle;
                RelationMultipleNoLimit = item.RelationMultipleNoLimit;
                RelationMultipleLimit = item.RelationMultipleLimit;

            }
        }

        /// <inheritdoc />
        protected override IEnumerable<ItemBase?> RelatedItems 
            => base.RelatedItems.Union(new List<ItemBase?>() { ReationSingle }).Union(RelationMultipleNoLimit).Union(RelationMultipleLimit);

        #region Collection
        public static CollectionTestForTypes GetCollection() 
            => (CollectionTestForTypes)DataServiceBase.GetCollection<TestForType>()!;
        #endregion Collection

        public static async Task<TestForType?> GetByIdAsync(string id, bool reload = false)
            => await GetCollection().GetByIdAsync(id, reload);

        public static TestForType? GetById(string id, bool reload = false)
            => GetCollection().GetById(id, reload);
    }
}
