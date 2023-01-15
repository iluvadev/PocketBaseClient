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
    internal class FieldInfoRelation : FieldInfo
    {
        /// <summary>
        /// Options of the field defined in PocketBase
        /// </summary>
        private PocketBaseFieldOptionsRelation Options { get; }

        /// <summary>
        /// Says if the field can contain multiple values
        /// </summary>
        private bool IsMultiple => Options.MaxSelect == null || Options.MaxSelect > 1;

        /// <inheritdoc />
        public override bool PrivateSetter => IsMultiple;

        /// <inheritdoc />
        public override string TypeName => IsMultiple ? ListClassName : ReferencedClassName + "?";

        /// <inheritdoc />
        public override string InitialValueForProperty => IsMultiple ? $"new {ListClassName}(this)" : base.InitialValueForProperty;

        /// <inheritdoc />
        public override string InitialValueForAttribute => IsMultiple ? $"new {ListClassName}()" : base.InitialValueForAttribute;


        private string? _ReferencedClassName = null;
        /// <summary>
        /// The class name that the Relation refers to
        /// </summary>
        private string ReferencedClassName
            => _ReferencedClassName ??= ItemInfo.CollectionInfo.AllCollectionsGetter().First(c => c.Id == Options.CollectionId).ItemInfo.ClassName;

        /// <summary>
        /// The Class name of the type List if is multiple
        /// </summary>
        private string ListClassName => PropertyName + "List";
        
        /// <summary>
        /// The filename to the class for the list when is multiple
        /// </summary>
        private string ListFileName => ItemInfo.ClassName + "." + ListClassName + ".cs";

        /// <inheritdoc />
        public override string FilterType => IsMultiple ? $"FieldFilterItemList<{ListClassName}, {ReferencedClassName}>" : $"FieldFilterItem<{ReferencedClassName}>";


        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <param name="schemaField"></param>
        public FieldInfoRelation(ItemInfo itemInfo, SchemaFieldModel schemaField) : base(itemInfo, schemaField)
        {
            Options = JsonSerializer.Deserialize<PocketBaseFieldOptionsRelation>(JsonSerializer.Serialize(schemaField.Options)) ?? new PocketBaseFieldOptionsRelation();

            if (IsMultiple)
                _RelatedItems.Add(@$".Union({PropertyName})");
            else
                _RelatedItems.Add(@$".Union(new List<ItemBase?>() {{ {PropertyName} }})");
        }

        /// <inheritdoc />
        public override List<GeneratedCodeFile> GenerateCode(Settings settings)
        {
            var list = base.GenerateCode(settings);

            if (IsMultiple)
                list.Add(GetCodeFileForList(settings));

            return list;
        }

        /// <summary>
        /// Creates the code for the list of elements, when is multiple
        /// </summary>
        /// <param name="settings">Generation code settings</param>
        /// <returns></returns>
        private GeneratedCodeFile GetCodeFileForList(Settings settings)
        {
            var fileName = Path.Combine(settings.PathModels, ListFileName);
            var content = $@"{settings.CodeHeader}
using PocketBaseClient.Orm.Structures;

namespace {settings.NamespaceModels}
{{
    public partial class {ItemInfo.ClassName}
    {{
        public class {ListClassName} : FieldItemList<{ReferencedClassName}>
        {{
            public {ListClassName}() : this(null) {{ }}

            public {ListClassName}({ItemInfo.ClassName}? {ItemInfo.VarName}) : base({ItemInfo.VarName}, ""{PropertyName}"", ""{SchemaField.Id}"", {Options.MaxSelect?.ToString() ?? "null"}) {{ }}
        }}
    }}
}}
";
            return new GeneratedCodeFile(fileName, content);
        }

        /// <inheritdoc />
        protected override List<string> GetLinesForPropertyDecorators()
        {
            var list = base.GetLinesForPropertyDecorators();

            if (IsMultiple)
                list.Add($@"[JsonConverter(typeof(RelationListConverter<{ListClassName}, {ReferencedClassName}>))]");
            else
                list.Add($@"[JsonConverter(typeof(RelationConverter<{ReferencedClassName}>))]");

            return list;
        }
    }
}
