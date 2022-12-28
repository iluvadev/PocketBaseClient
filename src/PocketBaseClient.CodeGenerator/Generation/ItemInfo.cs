using System.Text;

namespace PocketBaseClient.CodeGenerator.Generation
{
    internal class ItemInfo
    {
        public CollectionInfo CollectionInfo { get; }

        public string ClassName { get; }
        public string VarName => ClassName.ToCamelCase();
        public string FileName => ClassName + ".cs";
        public string FiltersClassName => ClassName + ".Filters";
        public string FiltersFileName => FiltersClassName + ".cs";
        public string SortsClassName => ClassName + ".Sorts";
        public string SortsFileName => SortsClassName + ".cs";

        public List<FieldInfo> Fields { get; }

        public ItemInfo(CollectionInfo collectionInfo, string className)
        {
            CollectionInfo = collectionInfo;
            ClassName = className;

            Fields = CollectionInfo.CollectionModel.Schema!.Select(s => FieldInfo.NewFieldInfo(this, s)).ToList();
        }

        public List<GeneratedCodeFile> GenerateCode(Settings settings)
        {
            var generatedFiles = new List<GeneratedCodeFile>();

            generatedFiles.Add(GetCodeFileForItem(settings));
            generatedFiles.Add(GetCodeFileForFilters(settings));
            generatedFiles.Add(GetCodeFileForSorts(settings));
            
            foreach (var field in Fields)
                generatedFiles.AddRange(field.GenerateCode(settings));

            return generatedFiles;
        }

        private GeneratedCodeFile GetCodeFileForItem(Settings settings)
        {
            var fileName = Path.Combine(settings.PathModels, FileName);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($@"{settings.CodeHeader}
using pocketbase_csharp_sdk.Json;
using PocketBaseClient.Orm;
using PocketBaseClient.Orm.Attributes;
using PocketBaseClient.Orm.Json;
using PocketBaseClient.Orm.Validators;
using PocketBaseClient.Services;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace {settings.NamespaceModels}
{{
    public partial class {ClassName} : ItemBase
    {{
        #region Collection
        private static CollectionBase? _Collection = null;
        /// <inheritdoc />
        [JsonIgnore]
        public override CollectionBase Collection => _Collection ??= DataServiceBase.GetCollection<{ClassName}>()!;
        #endregion Collection
");
            sb.AppendLine($@"        #region Field Properties");
            foreach (var field in Fields)
                sb.AppendLine(field.GenerateCodeForProperty("        "));
            sb.AppendLine($@"        #endregion Field Properties");
            sb.AppendLine($@"
        /// <inheritdoc />
        public override void UpdateWith(ItemBase itemBase)
        {{
            base.UpdateWith(itemBase);

            if (itemBase is {ClassName} item)
            {{");
            foreach (var propertyName in Fields.Select(f => f.PropertyName))
                sb.AppendLine($@"                {propertyName} = item.{propertyName};");
            sb.AppendLine($@"
            }}
        }}");

            var relatedItems = Fields.SelectMany(f => f.RelatedItems);
            if (relatedItems.Any())
            {
                sb.Append($@"
        /// <inheritdoc />
        protected override IEnumerable<ItemBase?> RelatedItems 
            => base.RelatedItems");
                foreach (var relatedItem in relatedItems)
                    sb.Append($@"{relatedItem}");
                sb.AppendLine(";");
            }
            sb.AppendLine($@"
        #region Collection
        public static {CollectionInfo.ClassName} GetCollection() 
            => ({CollectionInfo.ClassName})DataServiceBase.GetCollection<{ClassName}>()!;
        #endregion Collection

        #region GetById
        public static {ClassName}? GetById(string id, bool reload = false) 
            => GetByIdAsync(id, reload).Result;

        public static async Task<{ClassName}?> GetByIdAsync(string id, bool reload = false)
            => await DataServiceBase.GetCollection<{ClassName}>()!.GetByIdAsync(id, reload);
        #endregion GetById
    }}
}}");

            return new GeneratedCodeFile(fileName, sb.ToString());
        }

        private GeneratedCodeFile GetCodeFileForFilters(Settings settings)
        {
            var fileName = Path.Combine(settings.PathModels, FiltersFileName);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($@"{settings.CodeHeader}
using PocketBaseClient.Orm.Filters;

namespace {settings.NamespaceModels}
{{
    public partial class {ClassName}
    {{
        public class Filters : ItemBaseFilters
        {{
");
            foreach(var field in Fields)
                sb.AppendLine(field.GenerateCodeForFilter("            "));
            sb.AppendLine($@"
        }}
    }}
}}");
            return new GeneratedCodeFile(fileName, sb.ToString());
        }

        private GeneratedCodeFile GetCodeFileForSorts(Settings settings)
        {
            var fileName = Path.Combine(settings.PathModels, SortsFileName);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($@"{settings.CodeHeader}
using PocketBaseClient.Orm.Filters;

namespace {settings.NamespaceModels}
{{
    public partial class {ClassName}
    {{
        public class Sorts : ItemBaseSorts
        {{
");
            foreach (var field in Fields)
                sb.AppendLine(field.GenerateCodeForSort("            "));
            sb.AppendLine($@"
        }}
    }}
}}");
            return new GeneratedCodeFile(fileName, sb.ToString());
        }
    }
}
