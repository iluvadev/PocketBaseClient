// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using System.Text.Json.Serialization;

namespace PocketBaseClient.CodeGenerator.Models
{
    public class PocketBaseFieldOptionsRelation
    {
        [JsonPropertyName("maxSelect")]
        public int? MaxSelect { get; set; }
        public bool IsSinglSelect => (MaxSelect ?? 0) == 1;

        [JsonPropertyName("cascadeDelete")]
        public bool? CascadeDelete { get; set; }

        [JsonPropertyName("collectionId")]
        public string? CollectionId { get; set; }
    }
}
