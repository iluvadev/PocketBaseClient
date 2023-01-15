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
    /// <summary>
    /// Model to map PocketBase Options for fields of type Text
    /// </summary>
    public class PocketBaseFieldOptionsText
    {
        /// <summary>
        /// Max length of the field values
        /// </summary>
        [JsonPropertyName("max")]
        public int? Max { get; set; }

        /// <summary>
        /// Min length of the field values
        /// </summary>
        [JsonPropertyName("min")]
        public int? Min { get; set; }

        /// <summary>
        /// Pattern for the field values
        /// </summary>
        [JsonPropertyName("pattern")]
        public string? Pattern { get; set; }
    }
}
