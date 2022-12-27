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
    internal class FieldInfoRelation : FieldInfo
    {
        private PocketBaseFieldOptionsRelation Options { get; }
        private bool IsMultiple => Options.MaxSelect == null || Options.MaxSelect > 1;
        public override bool PrivateSetter => IsMultiple;
        public override string TypeName => IsMultiple ? ListClassName : ReferencedClassName + "?";
        public override string InitialValueForProperty => IsMultiple ? $"new {ListClassName}(this)" : base.InitialValueForProperty;
        public override string InitialValueForAttribute => IsMultiple ? $"new {ListClassName}()" : base.InitialValueForAttribute;


        private string? _ReferencedClassName = null;
        private string ReferencedClassName
            => _ReferencedClassName ??= ItemInfo.CollectionInfo.AllCollectionsGetter().First(c => c.Id == Options.CollectionId).ItemInfo.ClassName;
        private string ListClassName => PropertyName + "List";
        private string ListFileName => ItemInfo.ClassName + "." + ListClassName + ".cs";

        public override string FilterType => IsMultiple ? $"FieldFilterItemList<{ListClassName}, {ReferencedClassName}>" : $"FieldFilterItem<{ReferencedClassName}>";

        public FieldInfoRelation(ItemInfo itemInfo, SchemaFieldModel schemaField) : base(itemInfo, schemaField)
        {
            Options = JsonSerializer.Deserialize<PocketBaseFieldOptionsRelation>(JsonSerializer.Serialize(schemaField.Options)) ?? new PocketBaseFieldOptionsRelation();

            if (IsMultiple)
                _RelatedItems.Add(@$".Union({PropertyName})");
            else
                _RelatedItems.Add(@$".Union(new List<ItemBase?>() {{ {PropertyName} }})");
        }

        public override List<GeneratedCodeFile> GenerateCode(Settings settings)
        {
            var list = base.GenerateCode(settings);

            if (IsMultiple)
                list.Add(GetCodeFileForList(settings));

            return list;
        }

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
