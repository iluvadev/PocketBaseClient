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
using System.Net.Mail;

namespace PocketBaseClient.Orm.Validators
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class ExceptDomainsAttribute : ValidationAttribute
    {
        public string ExceptDomainsValues { get; }
        private List<string> ExceptDomainsList => ExceptDomainsValues?.Split(',').ToList() ?? new List<string>();

        public ExceptDomainsAttribute(string exceptDomainsValues)
        {
            ExceptDomainsValues = exceptDomainsValues;
        }
        public override bool IsValid(object? value)
        {
            if (value == null) return true;

            if (value is MailAddress email)
                return !ExceptDomainsList.Contains(email.Host);

            if (value is Uri uri)
                return !ExceptDomainsList.Contains(uri.Host);

            return true;
        }
    }
}
