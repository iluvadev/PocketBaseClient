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
    /// Model to map PocketBase Options for fields of type File
    /// </summary>
    public class PocketBaseFieldOptionsFile
    {
        /// <summary>
        /// Max number of elements in the field
        /// </summary>
        [JsonPropertyName("maxSelect")]
        public int? MaxSelect { get; set; }
        /// <summary>
        /// Says if the field has only one element
        /// </summary>
        public bool IsSinglSelect => (MaxSelect ?? 1) == 1;

        /// <summary>
        /// Max size of the files
        /// </summary>
        [JsonPropertyName("maxSize")]
        public long? MaxSize { get; set; }

        /// <summary>
        /// Mimetypes accepted
        /// </summary>
        [JsonPropertyName("mimeTypes")]
        public List<string>? MimeTypes { get; set; }

        /// <summary>
        /// Thumbs accepted
        /// </summary>
        [JsonPropertyName("thumbs")]
        public List<string>? Thumbs { get; set; }
    }
}
