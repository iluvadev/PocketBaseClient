using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketBaseClient.Orm.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class PocketBaseFieldAttribute : Attribute
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Required { get; set; }
        public bool System { get; set; }
        public bool Unique { get; set; }
        public string Type { get; set; }

        public PocketBaseFieldAttribute(string id, string name, bool required, bool system, bool unique, string type)
        {
            Id = id;
            Name = name;
            Required = required;
            System = system;
            Unique = unique;
            Type = type;
        }
    }
}
