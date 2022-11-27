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
    sealed public class ExceptDomainsAttribute : ValidationAttribute
    {
        public string ExceptDomainsValues { get; }
        private List<string> ExceptDomainsList => ExceptDomainsValues?.Split(',').ToList()??new List<string>();

        public ExceptDomainsAttribute(string exceptDomainsValues)
        {
            ExceptDomainsValues = exceptDomainsValues;
        }
        public override bool IsValid(object? value)
        {
            if (value == null) return true;

            if(value is MailAddress email)
                return !ExceptDomainsList.Contains(email.Host);

            if (value is Uri uri)
                return !ExceptDomainsList.Contains(uri.Host);

            return true;
        }
    }
}
