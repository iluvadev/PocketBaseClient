// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using pocketbase_csharp_sdk.Json;
using System.Text.Json.Serialization;

namespace PocketBaseClient.CodeGenerator.Models
{
    public class PocketBaseFieldOptionsDatetime
    {
        [JsonPropertyName("max")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? Max { get; set; }

        [JsonPropertyName("min")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? Min { get; set; }
    }
}
