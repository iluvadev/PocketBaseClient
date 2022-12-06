
// This file was generated automatically on 6/12/2022 16:12:34(UTC) from the PocketBase schema for Application orm-csharp-test (https://orm-csharp-test.pockethost.io)
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
    public partial class TestForTypes : ItemBase
    {
        private static CollectionBase? _Collection = null;
        [JsonIgnore]
        public override CollectionBase Collection => _Collection ??= DataServiceBase.GetCollection<TestForTypes>()!;

        private string? _TextNoRestrictions = null;
        [JsonPropertyName("text_no_restrictions")]
        [PocketBaseField(id: "kkyx3zk2", name: "text_no_restrictions", required: false, system: false, unique: false, type: "text")]
        public string? TextNoRestrictions
        {
           get => Get(() => _TextNoRestrictions);
           set => Set(value, ref _TextNoRestrictions);
        }

        private string? _TextRestrictions = null;
        [JsonPropertyName("text_restrictions")]
        [PocketBaseField(id: "sgd9bm7z", name: "text_restrictions", required: false, system: false, unique: false, type: "text")]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "Minimum 5, Maximum 15 characters")]
        [RegularExpression(@"^\w+$", ErrorMessage = @"Pattern '^\w+$' not match")]
        public string? TextRestrictions
        {
           get => Get(() => _TextRestrictions);
           set => Set(value, ref _TextRestrictions);
        }

        private int? _NumberNoRestrictions = null;
        [JsonPropertyName("number_no_restrictions")]
        [PocketBaseField(id: "zasulmy0", name: "number_no_restrictions", required: false, system: false, unique: false, type: "number")]
        public int? NumberNoRestrictions
        {
           get => Get(() => _NumberNoRestrictions);
           set => Set(value, ref _NumberNoRestrictions);
        }

        private int? _NumberRestrrictions = null;
        [JsonPropertyName("number_restrrictions")]
        [PocketBaseField(id: "xsz2augn", name: "number_restrrictions", required: false, system: false, unique: false, type: "number")]
        [Range(0, 10, ErrorMessage = "Minimum 0, Maximum 10")]
        public int? NumberRestrrictions
        {
           get => Get(() => _NumberRestrrictions);
           set => Set(value, ref _NumberRestrrictions);
        }

        private bool? _Bool = null;
        [JsonPropertyName("bool")]
        [PocketBaseField(id: "vo8jeqfr", name: "bool", required: false, system: false, unique: false, type: "bool")]
        public bool? Bool
        {
           get => Get(() => _Bool);
           set => Set(value, ref _Bool);
        }

        private MailAddress? _EmailNoRestrictions = null;
        [JsonPropertyName("email_no_restrictions")]
        [PocketBaseField(id: "dfc3hzbp", name: "email_no_restrictions", required: false, system: false, unique: false, type: "email")]
        [JsonConverter(typeof(EmailConverter))]
        public MailAddress? EmailNoRestrictions
        {
           get => Get(() => _EmailNoRestrictions);
           set => Set(value, ref _EmailNoRestrictions);
        }

        private MailAddress? _EmailRestrictionsExcept = null;
        [JsonPropertyName("email_restrictions_except")]
        [PocketBaseField(id: "hea5cksh", name: "email_restrictions_except", required: false, system: false, unique: false, type: "email")]
        [JsonConverter(typeof(EmailConverter))]
        [ExceptDomains("gmail.com,hotmail.com", ErrorMessage = "Except domains accepted: 'gmail.com,hotmail.com'")]
        public MailAddress? EmailRestrictionsExcept
        {
           get => Get(() => _EmailRestrictionsExcept);
           set => Set(value, ref _EmailRestrictionsExcept);
        }

        private MailAddress? _EmailRestrictionsOnly = null;
        [JsonPropertyName("email_restrictions_only")]
        [PocketBaseField(id: "ddvywcfc", name: "email_restrictions_only", required: false, system: false, unique: false, type: "email")]
        [JsonConverter(typeof(EmailConverter))]
        [OnlyDomains("pockethost.io", ErrorMessage = "Only domains accepted: 'pockethost.io'")]
        public MailAddress? EmailRestrictionsOnly
        {
           get => Get(() => _EmailRestrictionsOnly);
           set => Set(value, ref _EmailRestrictionsOnly);
        }

        private Uri? _UrlNoRestrictions = null;
        [JsonPropertyName("url_no_restrictions")]
        [PocketBaseField(id: "n11oh0zk", name: "url_no_restrictions", required: false, system: false, unique: false, type: "url")]
        [JsonConverter(typeof(UrlConverter))]
        public Uri? UrlNoRestrictions
        {
           get => Get(() => _UrlNoRestrictions);
           set => Set(value, ref _UrlNoRestrictions);
        }

        private Uri? _UrlRestrictionsExcept = null;
        [JsonPropertyName("url_restrictions_except")]
        [PocketBaseField(id: "gimszuxa", name: "url_restrictions_except", required: false, system: false, unique: false, type: "url")]
        [JsonConverter(typeof(UrlConverter))]
        [ExceptDomains("google.com", ErrorMessage = "Except domains accepted: 'google.com'")]
        public Uri? UrlRestrictionsExcept
        {
           get => Get(() => _UrlRestrictionsExcept);
           set => Set(value, ref _UrlRestrictionsExcept);
        }

        private Uri? _UrlRestrictionsOnly = null;
        [JsonPropertyName("url_restrictions_only")]
        [PocketBaseField(id: "pntohkfm", name: "url_restrictions_only", required: false, system: false, unique: false, type: "url")]
        [JsonConverter(typeof(UrlConverter))]
        [OnlyDomains("pockethost.io", ErrorMessage = "Only domains accepted: 'pockethost.io'")]
        public Uri? UrlRestrictionsOnly
        {
           get => Get(() => _UrlRestrictionsOnly);
           set => Set(value, ref _UrlRestrictionsOnly);
        }

        private DateTime? _DatetimeNoRestrictions = null;
        [JsonPropertyName("datetime_no_restrictions")]
        [PocketBaseField(id: "why1vezh", name: "datetime_no_restrictions", required: false, system: false, unique: false, type: "date")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? DatetimeNoRestrictions
        {
           get => Get(() => _DatetimeNoRestrictions);
           set => Set(value, ref _DatetimeNoRestrictions);
        }

        private DateTime? _DatetimeRestrictions = null;
        [JsonPropertyName("datetime_restrictions")]
        [PocketBaseField(id: "xsnujvoc", name: "datetime_restrictions", required: false, system: false, unique: false, type: "date")]
        [JsonConverter(typeof(DateTimeConverter))]
        [Range(typeof(DateTime), "1/11/2022 13:00:00", "31/12/9999 22:59:59", ErrorMessage = "Minimum '1/11/2022 13:00:00', Maximum '31/12/9999 22:59:59'")]
        public DateTime? DatetimeRestrictions
        {
           get => Get(() => _DatetimeRestrictions);
           set => Set(value, ref _DatetimeRestrictions);
        }

        private SelectSingleEnum? _SelectSingle = null;
        [JsonPropertyName("select_single")]
        [PocketBaseField(id: "uiv0j5vw", name: "select_single", required: false, system: false, unique: false, type: "select")]
        [JsonConverter(typeof(EnumConverter<SelectSingleEnum>))]
        public SelectSingleEnum? SelectSingle
        {
           get => Get(() => _SelectSingle);
           set => Set(value, ref _SelectSingle);
        }

        private SelectMultipleList _SelectMultiple = new SelectMultipleList();
        [JsonPropertyName("select_multiple")]
        [PocketBaseField(id: "8dks1xfy", name: "select_multiple", required: false, system: false, unique: false, type: "select")]
        [JsonConverter(typeof(EnumListConverter<SelectMultipleList, SelectMultipleEnum>))]
        public SelectMultipleList SelectMultiple
        {
           get => Get(() => _SelectMultiple ??= new SelectMultipleList());
           private set => Set(value, ref _SelectMultiple);
        }

        private dynamic? _Json = null;
        [JsonPropertyName("json")]
        [PocketBaseField(id: "hmr5iih4", name: "json", required: false, system: false, unique: false, type: "json")]
        public dynamic? Json
        {
           get => Get(() => _Json);
           set => Set(value, ref _Json);
        }

        private object? _FileSingleNoRestriction = null;
        [JsonPropertyName("file_single_no_restriction")]
        [PocketBaseField(id: "mpnfu1ph", name: "file_single_no_restriction", required: false, system: false, unique: false, type: "file")]
        public object? FileSingleNoRestriction
        {
           get => Get(() => _FileSingleNoRestriction);
           set => Set(value, ref _FileSingleNoRestriction);
        }

        private object? _FileSingleRestriction = null;
        [JsonPropertyName("file_single_restriction")]
        [PocketBaseField(id: "cn4tglcr", name: "file_single_restriction", required: false, system: false, unique: false, type: "file")]
        public object? FileSingleRestriction
        {
           get => Get(() => _FileSingleRestriction);
           set => Set(value, ref _FileSingleRestriction);
        }

        private object? _FileMultipleNoRestrictions = null;
        [JsonPropertyName("file_multiple_no_restrictions")]
        [PocketBaseField(id: "mqokykua", name: "file_multiple_no_restrictions", required: false, system: false, unique: false, type: "file")]
        public object? FileMultipleNoRestrictions
        {
           get => Get(() => _FileMultipleNoRestrictions);
           set => Set(value, ref _FileMultipleNoRestrictions);
        }

        private object? _FileMultipleRestrictions = null;
        [JsonPropertyName("file_multiple_restrictions")]
        [PocketBaseField(id: "o4hs5o8n", name: "file_multiple_restrictions", required: false, system: false, unique: false, type: "file")]
        public object? FileMultipleRestrictions
        {
           get => Get(() => _FileMultipleRestrictions);
           set => Set(value, ref _FileMultipleRestrictions);
        }

        private TestForRelated? _ReationSingle = null;
        [JsonPropertyName("reation_single")]
        [PocketBaseField(id: "7q0qviac", name: "reation_single", required: false, system: false, unique: false, type: "relation")]
        [JsonConverter(typeof(RelationConverter<TestForRelated>))]
        public TestForRelated? ReationSingle
        {
           get => Get(() => _ReationSingle);
           set => Set(value, ref _ReationSingle);
        }

        private List<TestForRelated> _RelationMultipleNoLimit = new List<TestForRelated>();
        [JsonPropertyName("relation_multiple_no_limit")]
        [PocketBaseField(id: "a4chtr6c", name: "relation_multiple_no_limit", required: false, system: false, unique: false, type: "relation")]
        [JsonConverter(typeof(RelationListConverter<List<TestForRelated>, TestForRelated>))]
        public List<TestForRelated> RelationMultipleNoLimit
        {
           get => Get(() => _RelationMultipleNoLimit ??= new List<TestForRelated>());
           private set => Set(value, ref _RelationMultipleNoLimit);
        }

        private RelationMultipleLimitList _RelationMultipleLimit = new RelationMultipleLimitList();
        [JsonPropertyName("relation_multiple_limit")]
        [PocketBaseField(id: "otxwaoam", name: "relation_multiple_limit", required: false, system: false, unique: false, type: "relation")]
        [JsonConverter(typeof(RelationListConverter<RelationMultipleLimitList, TestForRelated>))]
        public RelationMultipleLimitList RelationMultipleLimit
        {
           get => Get(() => _RelationMultipleLimit ??= new RelationMultipleLimitList());
           private set => Set(value, ref _RelationMultipleLimit);
        }


        public override void UpdateWith(ItemBase itemBase)
        {
            base.UpdateWith(itemBase);

            if (itemBase is TestForTypes item)
            {
                TextNoRestrictions = item.TextNoRestrictions;
                TextRestrictions = item.TextRestrictions;
                NumberNoRestrictions = item.NumberNoRestrictions;
                NumberRestrrictions = item.NumberRestrrictions;
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

        public override string ToString()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            return JsonSerializer.Serialize(this, options);
        }

        public static TestForTypes? GetById(string id, bool forceLoad = false) 
            => DataServiceBase.GetCollection<TestForTypes>()!.GetById(id, forceLoad);
    }
}
