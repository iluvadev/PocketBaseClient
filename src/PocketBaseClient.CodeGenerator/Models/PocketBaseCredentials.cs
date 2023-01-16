﻿// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PocketBaseClient.CodeGenerator.Models
{
    /// <summary>
    /// Model to map PocketBase credentials
    /// </summary>
    internal class PocketBaseCredentials
    {
        /// <summary>
        /// The credentials email
        /// </summary>
        [Display(Name = "Type an Admin email")]
        [DataType(DataType.EmailAddress)]
        [Required]
        [MinLength(5)]
        public string? Email { get; set; }

        /// <summary>
        /// The password
        /// </summary>
        [Display(Name = "Type the password")]
        [DataType(DataType.Password)]
        [Required]
        [MinLength(5)]
        [JsonIgnore]
        public string? Password { get; set; }
    }

}
