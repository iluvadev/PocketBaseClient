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
    internal class FieldInfoRelationMultiple : FieldInfoRelation
    {
        /// <inheritdoc />
        public override bool PrivateSetter => true;

        /// <inheritdoc />
        public override string TypeName => ListClassName;

        /// <inheritdoc />
        public override bool IsTypeNullableInProperty => false;

        /// <inheritdoc />
        public override string InitialValueForProperty => $"new {TypeName}(this)";

        /// <summary>
        /// The Class name of the type List if is multiple
        /// </summary>
        private string ListClassName => PropertyName + "List";

        /// <summary>
        /// The filename to the class for the list when is multiple
        /// </summary>
        private string ListFileName => ItemInfo.ClassName + "." + ListClassName + ".cs";

        /// <inheritdoc />
        public override string FilterType => $"FieldFilterItemList<{ListClassName}, {ReferencedClassName}>";

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <param name="schemaField"></param>
        /// <param name="options"></param>
        public FieldInfoRelationMultiple(ItemInfo itemInfo, SchemaFieldModel schemaField, PocketBaseFieldOptionsRelation options) : base(itemInfo, schemaField, options)
        {
            RelatedItems.Add(@$".Union({PropertyName})");
        }

        /// <inheritdoc />
        public override List<GeneratedCodeFile> GenerateCode(Settings settings)
        {
            var list = base.GenerateCode(settings);

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

            list.Add($@"[JsonConverter(typeof(RelationListConverter<{ListClassName}, {ReferencedClassName}>))]");

            return list;
        }

    }
}
