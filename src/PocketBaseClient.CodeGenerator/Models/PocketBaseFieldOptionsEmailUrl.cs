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
    /// Model to map PocketBase Options for fields of type Email or Url
    /// </summary>
    public class PocketBaseFieldOptionsEmailUrl
    {
        /// <summary>
        /// Exepted domains
        /// </summary>
        [JsonPropertyName("exceptDomains")]
        public List<string>? ExceptDomains { get; set; }

        /// <summary>
        /// Excepted domains as string joined by ','
        /// </summary>
        [JsonIgnore]
        public string ExceptDomainsJoined => (ExceptDomains?.Any() ?? false) ? string.Join(',', ExceptDomains) : string.Empty;

        /// <summary>
        /// Only domains accepted
        /// </summary>
        [JsonPropertyName("onlyDomains")]
        public List<string>? OnlyDomains { get; set; }

        /// <summary>
        /// Only domains accepted as string joined by ','
        /// </summary>
        [JsonIgnore]
        public string OnlyDomainsJoined => (OnlyDomains?.Any() ?? false) ? string.Join(',', OnlyDomains) : string.Empty;

    }
}
