// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

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
