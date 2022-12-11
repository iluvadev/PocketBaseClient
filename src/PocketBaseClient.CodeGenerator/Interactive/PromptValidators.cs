// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using Sharprompt;
using System.ComponentModel.DataAnnotations;

namespace PocketBaseClient.CodeGenerator.Interactive
{
    internal static class PromptValidators
    {
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

        public static List<Func<object, ValidationResult>> BaseDirForProject()
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

                    return ValidationResult.Success!;
                },
            };
        }

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

                    if (!File.Exists(Path.Combine(strDir, CodeGenerator.GeneratedPropertiesFileName)))
                        return new ValidationResult($"Missing definition file {CodeGenerator.GeneratedPropertiesFileName}");

                    return ValidationResult.Success!;
                },
            };
        }


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
