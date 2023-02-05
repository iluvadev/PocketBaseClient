
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
        private string? _TextNoRestrictions = default;
        /// <summary> Maps to 'text_no_restrictions' field in PocketBase </summary>
        [JsonPropertyName("text_no_restrictions")]
        [PocketBaseField(id: "kkyx3zk2", name: "text_no_restrictions", required: false, system: false, unique: false, type: "text")]
        [Display(Name = "Text no restrictions")]
        public string? TextNoRestrictions { get => Get(() => _TextNoRestrictions ??= default); set => Set(value, ref _TextNoRestrictions); }

        private string? _TextRestrictions = default;
        /// <summary> Maps to 'text_restrictions' field in PocketBase </summary>
        [JsonPropertyName("text_restrictions")]
        [PocketBaseField(id: "sgd9bm7z", name: "text_restrictions", required: false, system: false, unique: false, type: "text")]
        [Display(Name = "Text restrictions")]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "Minimum 5, Maximum 15 characters")]
        [RegularExpression(@"^\w+$", ErrorMessage = @"Pattern '^\w+$' not match")]
        public string? TextRestrictions { get => Get(() => _TextRestrictions ??= default); set => Set(value, ref _TextRestrictions); }

        private float? _NumberNoRestrictions = default;
        /// <summary> Maps to 'number_no_restrictions' field in PocketBase </summary>
        [JsonPropertyName("number_no_restrictions")]
        [PocketBaseField(id: "zasulmy0", name: "number_no_restrictions", required: false, system: false, unique: false, type: "number")]
        [Display(Name = "Number no restrictions")]
        public float? NumberNoRestrictions { get => Get(() => _NumberNoRestrictions ??= default); set => Set(value, ref _NumberNoRestrictions); }

        private float? _NumberRestrictions = default;
        /// <summary> Maps to 'number_restrictions' field in PocketBase </summary>
        [JsonPropertyName("number_restrictions")]
        [PocketBaseField(id: "xsz2augn", name: "number_restrictions", required: false, system: false, unique: false, type: "number")]
        [Display(Name = "Number restrictions")]
        [Range(0, 10, ErrorMessage = "Minimum 0, Maximum 10")]
        public float? NumberRestrictions { get => Get(() => _NumberRestrictions ??= default); set => Set(value, ref _NumberRestrictions); }

        private bool? _Bool = default;
        /// <summary> Maps to 'bool' field in PocketBase </summary>
        [JsonPropertyName("bool")]
        [PocketBaseField(id: "vo8jeqfr", name: "bool", required: false, system: false, unique: false, type: "bool")]
        [Display(Name = "Bool")]
        public bool? Bool { get => Get(() => _Bool ??= default); set => Set(value, ref _Bool); }

        private MailAddress? _EmailNoRestrictions = default;
        /// <summary> Maps to 'email_no_restrictions' field in PocketBase </summary>
        [JsonPropertyName("email_no_restrictions")]
        [PocketBaseField(id: "dfc3hzbp", name: "email_no_restrictions", required: false, system: false, unique: false, type: "email")]
        [Display(Name = "Email no restrictions")]
        [JsonConverter(typeof(EmailConverter))]
        public MailAddress? EmailNoRestrictions { get => Get(() => _EmailNoRestrictions ??= default); set => Set(value, ref _EmailNoRestrictions); }

        private MailAddress? _EmailRestrictionsExcept = default;
        /// <summary> Maps to 'email_restrictions_except' field in PocketBase </summary>
        [JsonPropertyName("email_restrictions_except")]
        [PocketBaseField(id: "hea5cksh", name: "email_restrictions_except", required: false, system: false, unique: false, type: "email")]
        [Display(Name = "Email restrictions except")]
        [ExceptDomains("gmail.com,hotmail.com", ErrorMessage = "Except domains accepted: 'gmail.com,hotmail.com'")]
        [JsonConverter(typeof(EmailConverter))]
        public MailAddress? EmailRestrictionsExcept { get => Get(() => _EmailRestrictionsExcept ??= default); set => Set(value, ref _EmailRestrictionsExcept); }

        private MailAddress? _EmailRestrictionsOnly = default;
        /// <summary> Maps to 'email_restrictions_only' field in PocketBase </summary>
        [JsonPropertyName("email_restrictions_only")]
        [PocketBaseField(id: "ddvywcfc", name: "email_restrictions_only", required: false, system: false, unique: false, type: "email")]
        [Display(Name = "Email restrictions only")]
        [OnlyDomains("pockethost.io", ErrorMessage = "Only domains accepted: 'pockethost.io'")]
        [JsonConverter(typeof(EmailConverter))]
        public MailAddress? EmailRestrictionsOnly { get => Get(() => _EmailRestrictionsOnly ??= default); set => Set(value, ref _EmailRestrictionsOnly); }

        private Uri? _UrlNoRestrictions = default;
        /// <summary> Maps to 'url_no_restrictions' field in PocketBase </summary>
        [JsonPropertyName("url_no_restrictions")]
        [PocketBaseField(id: "n11oh0zk", name: "url_no_restrictions", required: false, system: false, unique: false, type: "url")]
        [Display(Name = "Url no restrictions")]
        [JsonConverter(typeof(UrlConverter))]
        public Uri? UrlNoRestrictions { get => Get(() => _UrlNoRestrictions ??= default); set => Set(value, ref _UrlNoRestrictions); }

        private Uri? _UrlRestrictionsExcept = default;
        /// <summary> Maps to 'url_restrictions_except' field in PocketBase </summary>
        [JsonPropertyName("url_restrictions_except")]
        [PocketBaseField(id: "gimszuxa", name: "url_restrictions_except", required: false, system: false, unique: false, type: "url")]
        [Display(Name = "Url restrictions except")]
        [ExceptDomains("google.com", ErrorMessage = "Except domains accepted: 'google.com'")]
        [JsonConverter(typeof(UrlConverter))]
        public Uri? UrlRestrictionsExcept { get => Get(() => _UrlRestrictionsExcept ??= default); set => Set(value, ref _UrlRestrictionsExcept); }

        private Uri? _UrlRestrictionsOnly = default;
        /// <summary> Maps to 'url_restrictions_only' field in PocketBase </summary>
        [JsonPropertyName("url_restrictions_only")]
        [PocketBaseField(id: "pntohkfm", name: "url_restrictions_only", required: false, system: false, unique: false, type: "url")]
        [Display(Name = "Url restrictions only")]
        [OnlyDomains("pockethost.io", ErrorMessage = "Only domains accepted: 'pockethost.io'")]
        [JsonConverter(typeof(UrlConverter))]
        public Uri? UrlRestrictionsOnly { get => Get(() => _UrlRestrictionsOnly ??= default); set => Set(value, ref _UrlRestrictionsOnly); }

        private DateTime? _DatetimeNoRestrictions = default;
        /// <summary> Maps to 'datetime_no_restrictions' field in PocketBase </summary>
        [JsonPropertyName("datetime_no_restrictions")]
        [PocketBaseField(id: "why1vezh", name: "datetime_no_restrictions", required: false, system: false, unique: false, type: "date")]
        [Display(Name = "Datetime no restrictions")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? DatetimeNoRestrictions { get => Get(() => _DatetimeNoRestrictions ??= default); set => Set(value, ref _DatetimeNoRestrictions); }

        private DateTime? _DatetimeRestrictions = default;
        /// <summary> Maps to 'datetime_restrictions' field in PocketBase </summary>
        [JsonPropertyName("datetime_restrictions")]
        [PocketBaseField(id: "xsnujvoc", name: "datetime_restrictions", required: false, system: false, unique: false, type: "date")]
        [Display(Name = "Datetime restrictions")]
        [Range(typeof(DateTime), "1/11/2022 13:00:00", "31/12/9999 22:59:59", ErrorMessage = "Minimum '1/11/2022 13:00:00', Maximum '31/12/9999 22:59:59'")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? DatetimeRestrictions { get => Get(() => _DatetimeRestrictions ??= default); set => Set(value, ref _DatetimeRestrictions); }

        private SelectSingleEnum? _SelectSingle = default;
        /// <summary> Maps to 'select_single' field in PocketBase </summary>
        [JsonPropertyName("select_single")]
        [PocketBaseField(id: "uiv0j5vw", name: "select_single", required: false, system: false, unique: false, type: "select")]
        [Display(Name = "Select single")]
        [JsonConverter(typeof(EnumConverter<SelectSingleEnum>))]
        public SelectSingleEnum? SelectSingle { get => Get(() => _SelectSingle ??= default); set => Set(value, ref _SelectSingle); }

        private SelectMultipleList? _SelectMultiple = default;
        /// <summary> Maps to 'select_multiple' field in PocketBase </summary>
        [JsonPropertyName("select_multiple")]
        [PocketBaseField(id: "8dks1xfy", name: "select_multiple", required: false, system: false, unique: false, type: "select")]
        [Display(Name = "Select multiple")]
        [JsonInclude]
        [JsonConverter(typeof(EnumListConverter<SelectMultipleList, SelectMultipleEnum>))]
        public SelectMultipleList SelectMultiple { get => Get(() => _SelectMultiple ??= new SelectMultipleList(this)); private set => Set(value, ref _SelectMultiple); }

        private dynamic? _Json = default;
        /// <summary> Maps to 'json' field in PocketBase </summary>
        [JsonPropertyName("json")]
        [PocketBaseField(id: "hmr5iih4", name: "json", required: false, system: false, unique: false, type: "json")]
        [Display(Name = "Json")]
        public dynamic? Json { get => Get(() => _Json ??= default); set => Set(value, ref _Json); }

        private FileSingleNoRestrictionFile? _FileSingleNoRestriction = default;
        /// <summary> Maps to 'file_single_no_restriction' field in PocketBase </summary>
        [JsonPropertyName("file_single_no_restriction")]
        [PocketBaseField(id: "mpnfu1ph", name: "file_single_no_restriction", required: false, system: false, unique: false, type: "file")]
        [Display(Name = "File single no restriction")]
        [JsonInclude]
        [JsonConverter(typeof(FileConverter<FileSingleNoRestrictionFile>))]
        public FileSingleNoRestrictionFile FileSingleNoRestriction { get => Get(() => _FileSingleNoRestriction ??= new FileSingleNoRestrictionFile(this)); private set => Set(value, ref _FileSingleNoRestriction); }

        private FileSingleRestrictionFile? _FileSingleRestriction = default;
        /// <summary> Maps to 'file_single_restriction' field in PocketBase </summary>
        [JsonPropertyName("file_single_restriction")]
        [PocketBaseField(id: "cn4tglcr", name: "file_single_restriction", required: false, system: false, unique: false, type: "file")]
        [Display(Name = "File single restriction")]
        [JsonInclude]
        [MimeTypes("image/jpg,image/jpeg,image/png,image/svg+xml,image/gif", ErrorMessage = "Only MIME Types accepted: 'image/jpg,image/jpeg,image/png,image/svg+xml,image/gif'")]
        [JsonConverter(typeof(FileConverter<FileSingleRestrictionFile>))]
        public FileSingleRestrictionFile FileSingleRestriction { get => Get(() => _FileSingleRestriction ??= new FileSingleRestrictionFile(this)); private set => Set(value, ref _FileSingleRestriction); }

        private FileMultipleNoRestrictionsList? _FileMultipleNoRestrictions = default;
        /// <summary> Maps to 'file_multiple_no_restrictions' field in PocketBase </summary>
        [JsonPropertyName("file_multiple_no_restrictions")]
        [PocketBaseField(id: "mqokykua", name: "file_multiple_no_restrictions", required: false, system: false, unique: false, type: "file")]
        [Display(Name = "File multiple no restrictions")]
        [JsonInclude]
        [JsonConverter(typeof(FileListConverter<FileMultipleNoRestrictionsList, FileMultipleNoRestrictionsFile>))]
        public FileMultipleNoRestrictionsList FileMultipleNoRestrictions { get => Get(() => _FileMultipleNoRestrictions ??= new FileMultipleNoRestrictionsList(this)); private set => Set(value, ref _FileMultipleNoRestrictions); }

        private FileMultipleRestrictionsList? _FileMultipleRestrictions = default;
        /// <summary> Maps to 'file_multiple_restrictions' field in PocketBase </summary>
        [JsonPropertyName("file_multiple_restrictions")]
        [PocketBaseField(id: "o4hs5o8n", name: "file_multiple_restrictions", required: false, system: false, unique: false, type: "file")]
        [Display(Name = "File multiple restrictions")]
        [JsonInclude]
        [MimeTypes("image/jpg,image/jpeg,image/png,image/svg+xml,image/gif", ErrorMessage = "Only MIME Types accepted: 'image/jpg,image/jpeg,image/png,image/svg+xml,image/gif'")]
        [JsonConverter(typeof(FileListConverter<FileMultipleRestrictionsList, FileMultipleRestrictionsFile>))]
        public FileMultipleRestrictionsList FileMultipleRestrictions { get => Get(() => _FileMultipleRestrictions ??= new FileMultipleRestrictionsList(this)); private set => Set(value, ref _FileMultipleRestrictions); }

        private TestForRelated? _ReationSingle = default;
        /// <summary> Maps to 'reation_single' field in PocketBase </summary>
        [JsonPropertyName("reation_single")]
        [PocketBaseField(id: "7q0qviac", name: "reation_single", required: false, system: false, unique: false, type: "relation")]
        [Display(Name = "Reation single")]
        [JsonConverter(typeof(RelationConverter<TestForRelated>))]
        public TestForRelated? ReationSingle { get => Get(() => _ReationSingle ??= default); set => Set(value, ref _ReationSingle); }

        private RelationMultipleNoLimitList? _RelationMultipleNoLimit = default;
        /// <summary> Maps to 'relation_multiple_no_limit' field in PocketBase </summary>
        [JsonPropertyName("relation_multiple_no_limit")]
        [PocketBaseField(id: "a4chtr6c", name: "relation_multiple_no_limit", required: false, system: false, unique: false, type: "relation")]
        [Display(Name = "Relation multiple no limit")]
        [JsonInclude]
        [JsonConverter(typeof(RelationListConverter<RelationMultipleNoLimitList, TestForRelated>))]
        public RelationMultipleNoLimitList RelationMultipleNoLimit { get => Get(() => _RelationMultipleNoLimit ??= new RelationMultipleNoLimitList(this)); private set => Set(value, ref _RelationMultipleNoLimit); }

        private RelationMultipleLimitList? _RelationMultipleLimit = default;
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
            // Do not Update with this instance
            if (ReferenceEquals(this, itemBase)) return;

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

        #region Constructors

        public TestForType() : base()
        {
        }

        [JsonConstructor]
        public TestForType(string? id, DateTime? created, DateTime? updated, string @textNoRestrictions, string @textRestrictions, float @numberNoRestrictions, float @numberRestrictions, bool @bool, MailAddress @emailNoRestrictions, MailAddress @emailRestrictionsExcept, MailAddress @emailRestrictionsOnly, Uri @urlNoRestrictions, Uri @urlRestrictionsExcept, Uri @urlRestrictionsOnly, DateTime @datetimeNoRestrictions, DateTime @datetimeRestrictions, SelectSingleEnum @selectSingle, SelectMultipleList @selectMultiple, dynamic @json, FileSingleNoRestrictionFile @fileSingleNoRestriction, FileSingleRestrictionFile @fileSingleRestriction, FileMultipleNoRestrictionsList @fileMultipleNoRestrictions, FileMultipleRestrictionsList @fileMultipleRestrictions, TestForRelated @reationSingle, RelationMultipleNoLimitList @relationMultipleNoLimit, RelationMultipleLimitList @relationMultipleLimit)
            : base(id, created, updated)
        {
            TextNoRestrictions = @textNoRestrictions;
            TextRestrictions = @textRestrictions;
            NumberNoRestrictions = @numberNoRestrictions;
            NumberRestrictions = @numberRestrictions;
            Bool = @bool;
            EmailNoRestrictions = @emailNoRestrictions;
            EmailRestrictionsExcept = @emailRestrictionsExcept;
            EmailRestrictionsOnly = @emailRestrictionsOnly;
            UrlNoRestrictions = @urlNoRestrictions;
            UrlRestrictionsExcept = @urlRestrictionsExcept;
            UrlRestrictionsOnly = @urlRestrictionsOnly;
            DatetimeNoRestrictions = @datetimeNoRestrictions;
            DatetimeRestrictions = @datetimeRestrictions;
            SelectSingle = @selectSingle;
            SelectMultiple = @selectMultiple;
            Json = @json;
            FileSingleNoRestriction = @fileSingleNoRestriction;
            FileSingleRestriction = @fileSingleRestriction;
            FileMultipleNoRestrictions = @fileMultipleNoRestrictions;
            FileMultipleRestrictions = @fileMultipleRestrictions;
            ReationSingle = @reationSingle;
            RelationMultipleNoLimit = @relationMultipleNoLimit;
            RelationMultipleLimit = @relationMultipleLimit;

        }
        #endregion

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
