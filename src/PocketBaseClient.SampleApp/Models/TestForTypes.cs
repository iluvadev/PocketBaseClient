
// This file was generated automatically on 27/11/2022 9:33:50 from the PocketBase schema for Application orm-csharp-test (https://orm-csharp-test.pockethost.io)
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
using System.Text.Json.Serialization;

namespace PocketBaseClient.SampleApp.Models
{
    public partial class TestForTypes : ItemBase
    {
        [JsonPropertyName("text_no_restrictions")]
        [PocketBaseField("kkyx3zk2", "text_no_restrictions", false, false, false, "text")]
        public string? TextNoRestrictions { get; set; }

        [JsonPropertyName("text_restrictions")]
        [PocketBaseField("sgd9bm7z", "text_restrictions", false, false, false, "text")]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "Minimum 5, Maximum 15 characters")]
        [RegularExpression(@"^\w+$", ErrorMessage = @"Pattern '^\w+$' not match")]
        public string? TextRestrictions { get; set; }

        [JsonPropertyName("number_no_restrictions")]
        [PocketBaseField("zasulmy0", "number_no_restrictions", false, false, false, "number")]
        public int? NumberNoRestrictions { get; set; }

        [JsonPropertyName("number_restrrictions")]
        [PocketBaseField("xsz2augn", "number_restrrictions", false, false, false, "number")]
        [Range(0, 10, ErrorMessage = "Minimum 0, Maximum 10")]
        public int? NumberRestrrictions { get; set; }

        [JsonPropertyName("bool")]
        [PocketBaseField("vo8jeqfr", "bool", false, false, false, "bool")]
        public bool? Bool { get; set; }

        [JsonPropertyName("email_no_restrictions")]
        [PocketBaseField("dfc3hzbp", "email_no_restrictions", false, false, false, "email")]
        [JsonConverter(typeof(EmailConverter))]
        public MailAddress? EmailNoRestrictions { get; set; }

        [JsonPropertyName("email_restrictions_except")]
        [PocketBaseField("hea5cksh", "email_restrictions_except", false, false, false, "email")]
        [JsonConverter(typeof(EmailConverter))]
        [ExceptDomains("gmail.com,hotmail.com", ErrorMessage = "Except domains accepted: 'gmail.com,hotmail.com'")]
        public MailAddress? EmailRestrictionsExcept { get; set; }

        [JsonPropertyName("email_restrictions_only")]
        [PocketBaseField("ddvywcfc", "email_restrictions_only", false, false, false, "email")]
        [JsonConverter(typeof(EmailConverter))]
        [OnlyDomains("pockethost.io", ErrorMessage = "Only domains accepted: 'pockethost.io'")]
        public MailAddress? EmailRestrictionsOnly { get; set; }

        [JsonPropertyName("url_no_restrictions")]
        [PocketBaseField("n11oh0zk", "url_no_restrictions", false, false, false, "url")]
        [JsonConverter(typeof(UrlConverter))]
        public Uri? UrlNoRestrictions { get; set; }

        [JsonPropertyName("url_restrictions_except")]
        [PocketBaseField("gimszuxa", "url_restrictions_except", false, false, false, "url")]
        [JsonConverter(typeof(UrlConverter))]
        [ExceptDomains("google.com", ErrorMessage = "Except domains accepted: 'google.com'")]
        public Uri? UrlRestrictionsExcept { get; set; }

        [JsonPropertyName("url_restrictions_only")]
        [PocketBaseField("pntohkfm", "url_restrictions_only", false, false, false, "url")]
        [JsonConverter(typeof(UrlConverter))]
        [OnlyDomains("pockethost.io", ErrorMessage = "Only domains accepted: 'pockethost.io'")]
        public Uri? UrlRestrictionsOnly { get; set; }

        [JsonPropertyName("datetime_no_restrictions")]
        [PocketBaseField("why1vezh", "datetime_no_restrictions", false, false, false, "date")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? DatetimeNoRestrictions { get; set; }

        [JsonPropertyName("datetime_restrictions")]
        [PocketBaseField("xsnujvoc", "datetime_restrictions", false, false, false, "date")]
        [JsonConverter(typeof(DateTimeConverter))]
        [Range(typeof(DateTime), "1/11/2022 13:00:00", "31/12/9999 22:59:59", ErrorMessage = "Minimum '1/11/2022 13:00:00', Maximum '31/12/9999 22:59:59'")]
        public DateTime? DatetimeRestrictions { get; set; }

        [JsonPropertyName("select_single")]
        [PocketBaseField("uiv0j5vw", "select_single", false, false, false, "select")]
        public object? SelectSingle { get; set; }

        [JsonPropertyName("select_multiple")]
        [PocketBaseField("8dks1xfy", "select_multiple", false, false, false, "select")]
        public object? SelectMultiple { get; set; }

        [JsonPropertyName("json")]
        [PocketBaseField("hmr5iih4", "json", false, false, false, "json")]
        public object? Json { get; set; }

        [JsonPropertyName("file_single_no_restriction")]
        [PocketBaseField("mpnfu1ph", "file_single_no_restriction", false, false, false, "file")]
        public object? FileSingleNoRestriction { get; set; }

        [JsonPropertyName("file_single_restriction")]
        [PocketBaseField("cn4tglcr", "file_single_restriction", false, false, false, "file")]
        public object? FileSingleRestriction { get; set; }

        [JsonPropertyName("file_multiple_no_restrictions")]
        [PocketBaseField("mqokykua", "file_multiple_no_restrictions", false, false, false, "file")]
        public object? FileMultipleNoRestrictions { get; set; }

        [JsonPropertyName("file_multiple_restrictions")]
        [PocketBaseField("o4hs5o8n", "file_multiple_restrictions", false, false, false, "file")]
        public object? FileMultipleRestrictions { get; set; }

        [JsonPropertyName("reation_single")]
        [PocketBaseField("7q0qviac", "reation_single", false, false, false, "relation")]
        public object? ReationSingle { get; set; }

        [JsonPropertyName("relation_multiple_no_limit")]
        [PocketBaseField("a4chtr6c", "relation_multiple_no_limit", false, false, false, "relation")]
        public object? RelationMultipleNoLimit { get; set; }

        [JsonPropertyName("relation_multiple_limit")]
        [PocketBaseField("otxwaoam", "relation_multiple_limit", false, false, false, "relation")]
        public object? RelationMultipleLimit { get; set; }


    }
}
