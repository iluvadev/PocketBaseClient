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
    /// Information about a Field of type Relation of an Item in a Collection, for the code generation
    /// </summary>
    internal abstract class FieldInfoRelation : FieldInfo
    {
        /// <summary>
        /// Options of the field defined in PocketBase
        /// </summary>
        protected PocketBaseFieldOptionsRelation Options { get; }
        
        
        private string? _ReferencedClassName = null;
        /// <summary>
        /// The class name that the Relation refers to
        /// </summary>
        protected string ReferencedClassName
            => _ReferencedClassName ??= ItemInfo.CollectionInfo.AllCollectionsGetter().First(c => c.Id == Options.CollectionId).ItemInfo.ClassName;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <param name="schemaField"></param>
        /// <param name="options"></param>
        public FieldInfoRelation(ItemInfo itemInfo, SchemaFieldModel schemaField, PocketBaseFieldOptionsRelation options) : base(itemInfo, schemaField)
        {
            Options = options;
        }


        /// <summary>
        /// Factory for a Field info of type relation
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <param name="schemaField"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static FieldInfoRelation NewFieldInfoRelation(ItemInfo itemInfo, SchemaFieldModel schemaField)
        {
            if (schemaField.Type != "relation")
                throw new Exception($"Field type '{schemaField.Type}' not expected for field '{schemaField.Name}' (expecting 'relation')");

            var options = JsonSerializer.Deserialize<PocketBaseFieldOptionsRelation>(JsonSerializer.Serialize(schemaField.Options)) ?? new PocketBaseFieldOptionsRelation();
            if (options.IsMultiple)
                return new FieldInfoRelationMultiple(itemInfo, schemaField, options);
            else
                return new FieldInfoRelationOne(itemInfo, schemaField, options);
        }
    }
}
