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

namespace PocketBaseClient.CodeGenerator.Generation
{
    /// <summary>
    /// Information about a Field of type Select with only One value of an Item in a Collection, for the code generation
    /// </summary>
    internal class FieldInfoSelectOne : FieldInfoSelect
    {
        /// <inheritdoc />
        public override string TypeName => EnumName;

        /// <inheritdoc />
        public override string FilterType => $"FieldFilterEnum<{EnumName}>";

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <param name="schemaField"></param>
        /// <param name="options"></param>
        public FieldInfoSelectOne(ItemInfo itemInfo, SchemaFieldModel schemaField, PocketBaseFieldOptionsSelect options) : base(itemInfo, schemaField, options) { }


        /// <inheritdoc />
        protected override List<string> GetLinesForPropertyDecorators()
        {
            var list = base.GetLinesForPropertyDecorators();

            list.Add($@"[JsonConverter(typeof(EnumConverter<{EnumName}>))]");

            return list;
        }
    }
}
