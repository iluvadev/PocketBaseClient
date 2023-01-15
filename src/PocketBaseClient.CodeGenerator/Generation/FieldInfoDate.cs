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
    /// Information about a Field of type Date of an Item in a Collection, for the code generation
    /// </summary>
    internal class FieldInfoDate : FieldInfo
    {
        /// <summary>
        /// Options of the field defined in PocketBase
        /// </summary>
        private PocketBaseFieldOptionsDatetime Options { get; }

        /// <inheritdoc />
        public override string TypeName => "DateTime?";

        /// <inheritdoc />
        public override string FilterType => "FieldFilterDate";

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <param name="schemaField"></param>
        public FieldInfoDate(ItemInfo itemInfo, SchemaFieldModel schemaField) : base(itemInfo, schemaField)
        {
            Options = JsonSerializer.Deserialize<PocketBaseFieldOptionsDatetime>(JsonSerializer.Serialize(schemaField.Options)) ?? new PocketBaseFieldOptionsDatetime();
        }

        /// <inheritdoc />
        protected override List<string> GetLinesForPropertyDecorators()
        {
            var list = base.GetLinesForPropertyDecorators();

            if (Options.Max != null || Options.Min != null)
            {
                var min = Options.Min ?? DateTime.MinValue.ToUniversalTime();
                var max = Options.Max ?? DateTime.MaxValue.ToUniversalTime();
                list.Add($@"[Range(typeof(DateTime), ""{min}"", ""{max}"", ErrorMessage = ""Minimum '{min}', Maximum '{max}'"")]");
            }

            list.Add("[JsonConverter(typeof(DateTimeConverter))]");
            return list;
        }
    }
}
