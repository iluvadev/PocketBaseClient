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
    public class PocketBaseFieldOptionsSelect
    {
        [JsonPropertyName("maxSelect")]
        public int? MaxSelect { get; set; }
        public bool IsSinglSelect => (MaxSelect ?? 1) == 1;

        [JsonPropertyName("values")]
        public List<string>? Values { get; set; }
    }
}
