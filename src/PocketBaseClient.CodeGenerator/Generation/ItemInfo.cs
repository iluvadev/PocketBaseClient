// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using System.Text;

namespace PocketBaseClient.CodeGenerator.Generation
{
    /// <summary>
    /// Information about an Item of a Collection, for the code generation
    /// </summary>
    internal class ItemInfo
    {
        /// <summary>
        /// The Collection of the Item
        /// </summary>
        public CollectionInfo CollectionInfo { get; }

        /// <summary>
        /// Name of the Class that maps the Item type, in the generated code
        /// </summary>
        public string ClassName { get; }

        /// <summary>
        /// Filename where save the Item class, in the generated code
        /// </summary>
        public string FileName => ClassName + ".cs";

        /// <summary>
        /// Name of parameters and variables to use when the Item is referenced, in the generated code
        /// </summary>
        public string VarName => ClassName.ToCamelCase();

        /// <summary>
        /// Name of the Class that maps the Filtering options for the Item, in the generated code
        /// </summary>
        public string FiltersClassName => ClassName + ".Filters";

        /// <summary>
        /// Filename where save the Filtering options class for the Item, in the generated code
        /// </summary>
        public string FiltersFileName => FiltersClassName + ".cs";


        /// <summary>
        /// Name of the Class that maps the Sorting options for the Item, in the generated code
        /// </summary>
        public string SortsClassName => ClassName + ".Sorts";

        /// <summary>
        /// Filename where save the Sorting options class for the Item, in the generated code
        /// </summary>
        public string SortsFileName => SortsClassName + ".cs";

        /// <summary>
        /// List of Fields of the Item
        /// </summary>
        public List<FieldInfo> Fields { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="collectionInfo"></param>
        /// <param name="className"></param>
        public ItemInfo(CollectionInfo collectionInfo, string className)
        {
            CollectionInfo = collectionInfo;
            ClassName = className;

            Fields = CollectionInfo.CollectionModel.Schema!.Select(s => FieldInfo.NewFieldInfo(this, s)).ToList();
        }

        /// <summary>
        /// Generates code for the Items
        /// </summary>
        /// <param name="settings">Generation code settings</param>
        /// <returns></returns>
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

        /// <summary>
        /// Generates the code for the Item class
        /// </summary>
        /// <param name="settings">Generation code settings</param>
        /// <returns></returns>
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
            // Do not Update with this instance
            if (ReferenceEquals(this, itemBase)) return;

            base.UpdateWith(itemBase);

            if (itemBase is {ClassName} item)
            {{");
            foreach (var propertyName in Fields.Select(f => f.PropertyName))
                sb.AppendLine($@"                {propertyName} = item.{propertyName};");
            sb.AppendLine($@"
            }}
        }}");
            
            sb.AppendLine($@"
        #region Constructors

        public {ClassName}() : base()
        {{
        }}");

            sb.Append($@"
        [JsonConstructor]
        public {ClassName}(string? id, DateTime? created, DateTime? updated");
            for (int i = 0; i < Fields.Count; i++)
                sb.Append($@", {Fields[i].TypeName} {GetParameterNameForConstructor(Fields[i])}");
            sb.AppendLine($@")
            : base(id, created, updated)
        {{");
            foreach (var field in Fields)
                sb.AppendLine($@"            this.{field.PropertyName} = {GetParameterNameForConstructor(field)};");
            sb.AppendLine($@"
            AddInternal(this);
        }}
        #endregion");

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

        public static async Task<{ClassName}?> GetByIdAsync(string id, bool reload = false)
            => await GetCollection().GetByIdAsync(id, reload);

        public static {ClassName}? GetById(string id, bool reload = false)
            => GetCollection().GetById(id, reload);
    }}
}}");

            return new GeneratedCodeFile(fileName, sb.ToString());
        }

        /// <summary>
        /// Generates the parameter name for JSON constructor
        /// </summary>
        /// <param name="propertyName">Property name of field. Assume that the first letter is upper-cased.</param>
        /// <returns>Parameter name, which its first letter is lower-cased</returns>
        private string GetParameterNameForConstructor(FieldInfo fieldInfo)
        {
            return $"@{fieldInfo.PropertyName.ToCamelCase()}";
            //if (string.IsNullOrEmpty(propertyName))
            //    throw new InvalidOperationException("Property name cannot be null or empty");
            //if (!char.IsUpper(propertyName[0]))
            //    throw new InvalidOperationException("Property name must start with upper case letter");
            //return $"{propertyName[0].ToString().ToLower()}{propertyName.Substring(1)}";
        }

        /// <summary>
        /// Generates the code for the Filtering options class for the Item
        /// </summary>
        /// <param name="settings">Generation code settings</param>
        /// <returns></returns>
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
            foreach (var field in Fields)
                sb.AppendLine(field.GenerateCodeForFilter("            "));
            sb.AppendLine($@"
        }}
    }}
}}");
            return new GeneratedCodeFile(fileName, sb.ToString());
        }

        /// <summary>
        /// Generates the code for the Sorting options class for the Item
        /// </summary>
        /// <param name="settings">Generation code settings</param>
        /// <returns></returns>
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
