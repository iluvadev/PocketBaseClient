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

namespace PocketBaseClient.CodeGenerator.Generation
{
    /// <summary>
    /// Information about a Field of type Json of an Item in a Collection, for the code generation
    /// </summary>
    internal class FieldInfoJson : FieldInfo
    {
        /// <inheritdoc />
        public override string TypeName => "dynamic?";

        /// <inheritdoc />
        public override string FilterType => "FieldFilterText";

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <param name="schemaFieldModel"></param>
        public FieldInfoJson(ItemInfo itemInfo, SchemaFieldModel schemaFieldModel) : base(itemInfo, schemaFieldModel) { }
    }
}
