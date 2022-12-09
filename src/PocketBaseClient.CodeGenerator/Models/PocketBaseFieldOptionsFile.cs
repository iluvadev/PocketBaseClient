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
    public class PocketBaseFieldOptionsFile
    {
        [JsonPropertyName("maxSelect")]
        public int? MaxSelect { get; set; }
        public bool IsSinglSelect => (MaxSelect ?? 1) == 1;

        [JsonPropertyName("maxSize")]
        public long? MaxSize { get; set; }

        [JsonPropertyName("mimeTypes")]
        public List<string>? MimeTypes { get; set; }

        [JsonPropertyName("thumbs")]
        public List<string>? Thumbs { get; set; }
    }
}
