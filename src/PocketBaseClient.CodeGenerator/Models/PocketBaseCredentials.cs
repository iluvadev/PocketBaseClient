// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using System.ComponentModel.DataAnnotations;

namespace PocketBaseClient.CodeGenerator.Models
{
    internal class PocketBaseCredentials
    {
        [Display(Name = "Type an Admin email")]
        [DataType(DataType.EmailAddress)]
        [Required]
        [MinLength(5)]
        public string? Email { get; set; }

        [Display(Name = "Type the password")]
        [DataType(DataType.Password)]
        [Required]
        [MinLength(5)]
        public string? Password { get; set; }
    }

}
