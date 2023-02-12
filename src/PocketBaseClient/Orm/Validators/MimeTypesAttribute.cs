// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using MimeMapping;
using PocketBaseClient.Orm.Structures;
using System.ComponentModel.DataAnnotations;

namespace PocketBaseClient.Orm.Validators
{
    /// <summary>
    /// Validator for the "MIME Types" restriction
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class MimeTypesAttribute : ValidationAttribute
    {
        /// <summary>
        /// The "MIME Types" defined in server
        /// </summary>
        public string MimeTypesValues { get; }
        private List<string> MimeTypesList => MimeTypesValues?.Split(',').ToList() ?? new List<string>();

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="mimeTypesValues"></param>
        public MimeTypesAttribute(string mimeTypesValues)
        {
            MimeTypesValues = mimeTypesValues;
        }

        /// <inheritdoc />
        public override bool IsValid(object? value)
        {
            if (value == null) return false;

            if (value is FieldFileBase fieldFile)
                return MimeTypesList.Contains(MimeUtility.GetMimeMapping(fieldFile.FileName));

            if (value is IBasicList basicList)
            {
                foreach (var item in basicList)
                    if (item is FieldFileBase itemFile)
                        if (!MimeTypesList.Contains(MimeUtility.GetMimeMapping(itemFile.FileName)))
                            return false;
            }
            return true;
        }
    }
}
