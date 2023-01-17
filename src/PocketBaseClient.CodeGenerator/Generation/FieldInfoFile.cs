﻿// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using pocketbase_csharp_sdk.Models.Collection;
using PocketBaseClient.CodeGenerator.Models;
using System.Text.Json;

namespace PocketBaseClient.CodeGenerator.Generation
{
    /// <summary>
    /// Information about a Field of type File of an Item in a Collection, for the code generation
    /// </summary>
    internal class FieldInfoFile : FieldInfo
    {
        /// <summary>
        /// Options of the field defined in PocketBase
        /// </summary>
        private PocketBaseFieldOptionsFile Options { get; }

        /// <summary>
        /// Says if the field can contain multiple values
        /// </summary>
        private bool IsMultiple => Options.MaxSelect == null || Options.MaxSelect > 1;

        /// <inheritdoc />
        public override string TypeName => "FieldFileBase";

        /// <inheritdoc />
        public override bool IsTypeNullableInProperty => false;

        /// <inheritdoc />
        public override string FilterType => "FieldFilterText";

        /// <inheritdoc />
        public override string InitialValueForProperty => $"new {TypeName}(this)";
        //public override string InitialValueForProperty => IsMultiple ? $"new {ListClassName}(this)" : base.InitialValueForProperty;

        ///// <inheritdoc />
        //public override string InitialValueForAttribute => IsMultiple ? $"new {ListClassName}()" : base.InitialValueForAttribute;


        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <param name="schemaField"></param>
        public FieldInfoFile(ItemInfo itemInfo, SchemaFieldModel schemaField) : base(itemInfo, schemaField)
        {
            Options = JsonSerializer.Deserialize<PocketBaseFieldOptionsFile>(JsonSerializer.Serialize(schemaField.Options)) ?? new PocketBaseFieldOptionsFile();
        }

        /// <inheritdoc />
        protected override List<string> GetLinesForPropertyDecorators()
        {
            var list = base.GetLinesForPropertyDecorators();

            if (Options.MimeTypes?.Any() ?? false)
                list.Add($@"[MimeTypes(""{Options.MimeTypesJoined}"", ErrorMessage = ""Only MIME Types accepted: '{Options.MimeTypesJoined}'"")]");

            list.Add("[JsonConverter(typeof(FileConverter))]");
            return list;
        }
    }
}
