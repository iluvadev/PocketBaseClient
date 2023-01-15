﻿// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using PocketBaseClient.CodeGenerator.Generation;
using Sharprompt;
using System.ComponentModel.DataAnnotations;

namespace PocketBaseClient.CodeGenerator.Interactive
{
    /// <summary>
    /// Class with Validators for prompt values from Console input
    /// </summary>
    internal static class PromptValidators
    {
        /// <summary>
        /// Validators for PocketBase url inputs
        /// </summary>
        /// <returns></returns>
        public static List<Func<object, ValidationResult>> PocketBaseUrl()
        {
            return new List<Func<object, ValidationResult>>()
            {
                Validators.Required(),
                value =>
                {
                    if(value is not string)
                        return new ValidationResult("Value is not an string");
                    
                    string strUrl = (value as string??string.Empty).Trim();
                    if(string.IsNullOrEmpty(strUrl))
                        return new ValidationResult("Value is null or empty");

                    bool result = Uri.TryCreate(strUrl,  UriKind.Absolute, out var uriResult) && 
                                  (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                    
                    if (!result)
                        return new ValidationResult("Value is not a valid url");
                    return ValidationResult.Success!;
                },
            };
        }

        /// <summary>
        /// Validators for Project folder inputs
        /// </summary>
        /// <returns></returns>
        public static List<Func<object, ValidationResult>> ProjectFolder()
        {
            return new List<Func<object, ValidationResult>>()
            {
                Validators.Required(),
                value =>
                {
                    if(value is not string)
                        return new ValidationResult("Value is not an string");

                    string strDir = (value as string??string.Empty).Trim();
                    if(string.IsNullOrEmpty(strDir))
                        return new ValidationResult("Value is null or empty");

                    return ValidationResult.Success!;
                },
            };
        }

        /// <summary>
        /// Validators for Generated Project folder inputs
        /// </summary>
        /// <returns></returns>
        public static List<Func<object, ValidationResult>> GeneratedFolderProject()
        {
            return new List<Func<object, ValidationResult>>()
            {
                Validators.Required(),
                value =>
                {
                    if(value is not string)
                        return new ValidationResult("Value is not an string");

                    string strDir = (value as string??string.Empty).Trim();
                    if(string.IsNullOrEmpty(strDir))
                        return new ValidationResult("Value is null or empty");

                    if (!Directory.Exists(Path.GetDirectoryName(strDir)))
                        return new ValidationResult("Path do not exists");

                    if (!File.Exists(Path.Combine(strDir, Settings.SchemaFileName)))
                        return new ValidationResult($"Missing definition file {Settings.SchemaFileName}");

                    return ValidationResult.Success!;
                },
            };
        }

        /// <summary>
        /// Validators for names for Project or for Namespaces inputs
        /// </summary>
        /// <returns></returns>
        public static List<Func<object, ValidationResult>> NameForProjectOrNamespace()
        {
            return new List<Func<object, ValidationResult>>()
            {
                Validators.Required(),
                Validators.MinLength(5),
                value =>
                {
                    if(value is not string)
                        return new ValidationResult("Value is not an string");

                    string strDir = (value as string??string.Empty).Trim();
                    if(string.IsNullOrEmpty(strDir))
                        return new ValidationResult("Value is null or empty");

                    return ValidationResult.Success!;
                },
            };
        }
    }
}
