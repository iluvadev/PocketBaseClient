
// This file was generated automatically on 26/11/2022 18:38:33 from the PocketBase schema for Application orm-csharp-test (https://orm-csharp-test.pockethost.io)
//
// PocketBaseClient-csharp project: https://github.com/iluvadev/PocketBaseClient-csharp
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using PocketBaseClient.Orm;
using PocketBaseClient.Orm.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TestingModel
{
    public partial class TestForTypes : ItemBase
    {
        [JsonPropertyName("text_no_restrictions")]
        public string? textNoRestrictions { get; set; }
        [JsonPropertyName("text_restrictions")]
        public string? textRestrictions { get; set; }
        [JsonPropertyName("number_no_restrictions")]
        [JsonPropertyName("number_restrrictions")]
        [JsonPropertyName("bool")]
        [JsonPropertyName("email_no_restrictions")]
        [JsonPropertyName("email_restrictions_except")]
        [JsonPropertyName("email_restrictions_only")]
        [JsonPropertyName("url_no_restrictions")]
        [JsonPropertyName("url_restrictions_except")]
        [JsonPropertyName("url_restrictions_only")]
        [JsonPropertyName("datetime_no_restrictions")]
        [JsonPropertyName("datetime_restrictions")]
        [JsonPropertyName("select_single")]
        [JsonPropertyName("select_multiple")]
        [JsonPropertyName("json")]
        [JsonPropertyName("file_single_no_restriction")]
        [JsonPropertyName("file_single_restriction")]
        [JsonPropertyName("file_multiple_no_restrictions")]
        [JsonPropertyName("file_multiple_restrictions")]
        [JsonPropertyName("reation_single")]
        [JsonPropertyName("relation_multiple_no_limit")]
        [JsonPropertyName("relation_multiple_limit")]

    }
}
