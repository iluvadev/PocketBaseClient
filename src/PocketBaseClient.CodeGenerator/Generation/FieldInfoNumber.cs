// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
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
    /// Information about a Field of type Json of an Item in a Collection, for the code generation
    /// </summary>
    internal class FieldInfoNumber : FieldInfo
    {
        /// <summary>
        /// Options of the field defined in PocketBase
        /// </summary>
        private PocketBaseFieldOptionsNumber Options { get; }
        
        /// <inheritdoc />
        public override string TypeName => "int?";

        /// <inheritdoc />
        public override string FilterType => "FieldFilterNumber";

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <param name="schemaField"></param>
        public FieldInfoNumber(ItemInfo itemInfo, SchemaFieldModel schemaField) : base(itemInfo, schemaField)
        {
            Options = JsonSerializer.Deserialize<PocketBaseFieldOptionsNumber>(JsonSerializer.Serialize(schemaField.Options)) ?? new PocketBaseFieldOptionsNumber();
        }

        /// <inheritdoc />
        protected override List<string> GetLinesForPropertyDecorators()
        {
            var list = base.GetLinesForPropertyDecorators();

            if (Options.Max != null || Options.Min != null)
            {
                var min = Options.Min ?? int.MinValue;
                var max = Options.Max ?? int.MaxValue;
                list.Add($@"[Range({min}, {max}, ErrorMessage = ""Minimum {min}, Maximum {max}"")]");
            }

            return list;
        }
    }
}
