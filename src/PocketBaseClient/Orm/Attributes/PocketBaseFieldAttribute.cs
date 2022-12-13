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
    /// <summary>
    /// Attribute with PocketBase field definition
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class PocketBaseFieldAttribute : Attribute
    {
        /// <summary>
        /// The Id of the field
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The Name of the field
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// If the field is Required
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// If the field is System
        /// </summary>
        public bool System { get; set; }

        /// <summary>
        /// If the field is Unique
        /// </summary>
        public bool Unique { get; set; }

        /// <summary>
        /// The type of the field
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="required"></param>
        /// <param name="system"></param>
        /// <param name="unique"></param>
        /// <param name="type"></param>
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
