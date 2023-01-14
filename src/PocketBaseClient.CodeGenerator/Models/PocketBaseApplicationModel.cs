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
    /// Model to map the PocketBase application information
    /// </summary>
    public class PocketBaseApplicationModel
    {
        /// <summary>
        /// The name of the PocketBase application
        /// </summary>
        [JsonPropertyName("appName")]
        public string? Name { get; set; }

        /// <summary>
        /// The Url of the PocketBase application (the server)
        /// </summary>
        [JsonPropertyName("appUrl")]
        public string? Url { get; set; }
    }
}
