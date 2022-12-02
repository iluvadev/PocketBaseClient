
// This file was generated automatically on 2/12/2022 22:42:28(UTC) from the PocketBase schema for Application orm-csharp-test (https://orm-csharp-test.pockethost.io)
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
    public partial class TestForTypes : ItemBase
    {
        private string? _TextNoRestrictions = null;
        [JsonPropertyName("text_no_restrictions")]
        [PocketBaseField("kkyx3zk2", "text_no_restrictions", false, false, false, "text")]
        public string? TextNoRestrictions { get => Get(() => _TextNoRestrictions); set => Set(value, ref _TextNoRestrictions); }

        private string? _TextRestrictions = null;
        [JsonPropertyName("text_restrictions")]
        [PocketBaseField("sgd9bm7z", "text_restrictions", false, false, false, "text")]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "Minimum 5, Maximum 15 characters")]
        [RegularExpression(@"^\w+$", ErrorMessage = @"Pattern '^\w+$' not match")]
        public string? TextRestrictions { get => Get(() => _TextRestrictions); set => Set(value, ref _TextRestrictions); }

        private int? _NumberNoRestrictions = null;
        [JsonPropertyName("number_no_restrictions")]
        [PocketBaseField("zasulmy0", "number_no_restrictions", false, false, false, "number")]
        public int? NumberNoRestrictions { get => Get(() => _NumberNoRestrictions); set => Set(value, ref _NumberNoRestrictions); }

        private int? _NumberRestrrictions = null;
        [JsonPropertyName("number_restrrictions")]
        [PocketBaseField("xsz2augn", "number_restrrictions", false, false, false, "number")]
        [Range(0, 10, ErrorMessage = "Minimum 0, Maximum 10")]
        public int? NumberRestrrictions { get => Get(() => _NumberRestrrictions); set => Set(value, ref _NumberRestrrictions); }

        private bool? _Bool = null;
        [JsonPropertyName("bool")]
        [PocketBaseField("vo8jeqfr", "bool", false, false, false, "bool")]
        public bool? Bool { get => Get(() => _Bool); set => Set(value, ref _Bool); }

        private MailAddress? _EmailNoRestrictions = null;
        [JsonPropertyName("email_no_restrictions")]
        [PocketBaseField("dfc3hzbp", "email_no_restrictions", false, false, false, "email")]
        [JsonConverter(typeof(EmailConverter))]
        public MailAddress? EmailNoRestrictions { get => Get(() => _EmailNoRestrictions); set => Set(value, ref _EmailNoRestrictions); }

        private MailAddress? _EmailRestrictionsExcept = null;
        [JsonPropertyName("email_restrictions_except")]
        [PocketBaseField("hea5cksh", "email_restrictions_except", false, false, false, "email")]
        [JsonConverter(typeof(EmailConverter))]
        [ExceptDomains("gmail.com,hotmail.com", ErrorMessage = "Except domains accepted: 'gmail.com,hotmail.com'")]
        public MailAddress? EmailRestrictionsExcept { get => Get(() => _EmailRestrictionsExcept); set => Set(value, ref _EmailRestrictionsExcept); }

        private MailAddress? _EmailRestrictionsOnly = null;
        [JsonPropertyName("email_restrictions_only")]
        [PocketBaseField("ddvywcfc", "email_restrictions_only", false, false, false, "email")]
        [JsonConverter(typeof(EmailConverter))]
        [OnlyDomains("pockethost.io", ErrorMessage = "Only domains accepted: 'pockethost.io'")]
        public MailAddress? EmailRestrictionsOnly { get => Get(() => _EmailRestrictionsOnly); set => Set(value, ref _EmailRestrictionsOnly); }

        private Uri? _UrlNoRestrictions = null;
        [JsonPropertyName("url_no_restrictions")]
        [PocketBaseField("n11oh0zk", "url_no_restrictions", false, false, false, "url")]
        [JsonConverter(typeof(UrlConverter))]
        public Uri? UrlNoRestrictions { get => Get(() => _UrlNoRestrictions); set => Set(value, ref _UrlNoRestrictions); }

        private Uri? _UrlRestrictionsExcept = null;
        [JsonPropertyName("url_restrictions_except")]
        [PocketBaseField("gimszuxa", "url_restrictions_except", false, false, false, "url")]
        [JsonConverter(typeof(UrlConverter))]
        [ExceptDomains("google.com", ErrorMessage = "Except domains accepted: 'google.com'")]
        public Uri? UrlRestrictionsExcept { get => Get(() => _UrlRestrictionsExcept); set => Set(value, ref _UrlRestrictionsExcept); }

        private Uri? _UrlRestrictionsOnly = null;
        [JsonPropertyName("url_restrictions_only")]
        [PocketBaseField("pntohkfm", "url_restrictions_only", false, false, false, "url")]
        [JsonConverter(typeof(UrlConverter))]
        [OnlyDomains("pockethost.io", ErrorMessage = "Only domains accepted: 'pockethost.io'")]
        public Uri? UrlRestrictionsOnly { get => Get(() => _UrlRestrictionsOnly); set => Set(value, ref _UrlRestrictionsOnly); }

        private DateTime? _DatetimeNoRestrictions = null;
        [JsonPropertyName("datetime_no_restrictions")]
        [PocketBaseField("why1vezh", "datetime_no_restrictions", false, false, false, "date")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? DatetimeNoRestrictions { get => Get(() => _DatetimeNoRestrictions); set => Set(value, ref _DatetimeNoRestrictions); }

        private DateTime? _DatetimeRestrictions = null;
        [JsonPropertyName("datetime_restrictions")]
        [PocketBaseField("xsnujvoc", "datetime_restrictions", false, false, false, "date")]
        [JsonConverter(typeof(DateTimeConverter))]
        [Range(typeof(DateTime), "1/11/2022 13:00:00", "31/12/9999 22:59:59", ErrorMessage = "Minimum '1/11/2022 13:00:00', Maximum '31/12/9999 22:59:59'")]
        public DateTime? DatetimeRestrictions { get => Get(() => _DatetimeRestrictions); set => Set(value, ref _DatetimeRestrictions); }

        private SelectSingleEnum? _SelectSingle = null;
        [JsonPropertyName("select_single")]
        [PocketBaseField("uiv0j5vw", "select_single", false, false, false, "select")]
        [JsonConverter(typeof(EnumConverter<SelectSingleEnum>))]
        public SelectSingleEnum? SelectSingle { get => Get(() => _SelectSingle); set => Set(value, ref _SelectSingle); }

        private object? _SelectMultiple = null;
        [JsonPropertyName("select_multiple")]
        [PocketBaseField("8dks1xfy", "select_multiple", false, false, false, "select")]
        public object? SelectMultiple { get => Get(() => _SelectMultiple); set => Set(value, ref _SelectMultiple); }

        private dynamic? _Json = null;
        [JsonPropertyName("json")]
        [PocketBaseField("hmr5iih4", "json", false, false, false, "json")]
        public dynamic? Json { get => Get(() => _Json); set => Set(value, ref _Json); }

        private object? _FileSingleNoRestriction = null;
        [JsonPropertyName("file_single_no_restriction")]
        [PocketBaseField("mpnfu1ph", "file_single_no_restriction", false, false, false, "file")]
        public object? FileSingleNoRestriction { get => Get(() => _FileSingleNoRestriction); set => Set(value, ref _FileSingleNoRestriction); }

        private object? _FileSingleRestriction = null;
        [JsonPropertyName("file_single_restriction")]
        [PocketBaseField("cn4tglcr", "file_single_restriction", false, false, false, "file")]
        public object? FileSingleRestriction { get => Get(() => _FileSingleRestriction); set => Set(value, ref _FileSingleRestriction); }

        private object? _FileMultipleNoRestrictions = null;
        [JsonPropertyName("file_multiple_no_restrictions")]
        [PocketBaseField("mqokykua", "file_multiple_no_restrictions", false, false, false, "file")]
        public object? FileMultipleNoRestrictions { get => Get(() => _FileMultipleNoRestrictions); set => Set(value, ref _FileMultipleNoRestrictions); }

        private object? _FileMultipleRestrictions = null;
        [JsonPropertyName("file_multiple_restrictions")]
        [PocketBaseField("o4hs5o8n", "file_multiple_restrictions", false, false, false, "file")]
        public object? FileMultipleRestrictions { get => Get(() => _FileMultipleRestrictions); set => Set(value, ref _FileMultipleRestrictions); }

        private object? _ReationSingle = null;
        [JsonPropertyName("reation_single")]
        [PocketBaseField("7q0qviac", "reation_single", false, false, false, "relation")]
        public object? ReationSingle { get => Get(() => _ReationSingle); set => Set(value, ref _ReationSingle); }

        private object? _RelationMultipleNoLimit = null;
        [JsonPropertyName("relation_multiple_no_limit")]
        [PocketBaseField("a4chtr6c", "relation_multiple_no_limit", false, false, false, "relation")]
        public object? RelationMultipleNoLimit { get => Get(() => _RelationMultipleNoLimit); set => Set(value, ref _RelationMultipleNoLimit); }

        private object? _RelationMultipleLimit = null;
        [JsonPropertyName("relation_multiple_limit")]
        [PocketBaseField("otxwaoam", "relation_multiple_limit", false, false, false, "relation")]
        public object? RelationMultipleLimit { get => Get(() => _RelationMultipleLimit); set => Set(value, ref _RelationMultipleLimit); }


        public override string ToString()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            return JsonSerializer.Serialize(this, options);
        }
    }
}
