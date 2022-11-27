using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PocketBaseClient.Orm.Validators
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class OnlyDomainsAttribute : ValidationAttribute
    {
        public string OnlyDomainsValues { get; }
        private List<string> OnlyDomainsList => OnlyDomainsValues?.Split(',').ToList()??new List<string>();

        public OnlyDomainsAttribute(string exceptDomainsValues)
        {
            OnlyDomainsValues = exceptDomainsValues;
        }
        public override bool IsValid(object? value)
        {
            if (value == null) return false;

            if(value is MailAddress email)
                return OnlyDomainsList.Contains(email.Host);

            if (value is Uri uri)
                return OnlyDomainsList.Contains(uri.Host);

            return false;
        }
    }
}
