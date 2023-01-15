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
    /// Model to map PocketBase Options for fields of type Number
    /// </summary>
    public class PocketBaseFieldOptionsNumber
    {
        /// <summary>
        /// Max number
        /// </summary>
        [JsonPropertyName("max")]
        public float? Max { get; set; }

        /// <summary>
        /// Min number
        /// </summary>
        [JsonPropertyName("min")]
        public float? Min { get; set; }
    }
}
