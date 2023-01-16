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
    /// Model to map PocketBase Options for fields of type Relation
    /// </summary>
    public class PocketBaseFieldOptionsRelation
    {
        /// <summary>
        /// Max number of elements in the field
        /// </summary>
        [JsonPropertyName("maxSelect")]
        public int? MaxSelect { get; set; }
        /// <summary>
        /// Says if the field has only one element
        /// </summary>
        [JsonIgnore]
        public bool IsSinglSelect => (MaxSelect ?? 0) == 1;

        /// <summary>
        /// Indicates if server will performs cascade delete
        /// </summary>
        [JsonPropertyName("cascadeDelete")]
        public bool? CascadeDelete { get; set; }

        /// <summary>
        /// The related collectionId
        /// </summary>
        [JsonPropertyName("collectionId")]
        public string? CollectionId { get; set; }
    }
}
