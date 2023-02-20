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
using System.Text;

namespace PocketBaseClient.CodeGenerator.Generation
{
    /// <summary>
    /// Information about a Field of an Item in a Collection, for the code generation
    /// </summary>
    internal abstract class FieldInfo
    {
        /// <summary>
        /// Says if the Field is Required
        /// </summary>
        protected bool IsRequired => SchemaField.Required ?? false;

        /// <summary>
        /// The item of this field
        /// </summary>
        public ItemInfo ItemInfo { get; }

        /// <summary>
        /// The Field schema defined in PocketBase
        /// </summary>
        public SchemaFieldModel SchemaField { get; }

        /// <summary>
        /// The name used in the Property that maps the field, in the generated code
        /// </summary>
        public string PropertyName => SchemaField.Name!.ToPascalCase();

        /// <summary>
        /// The name to use in the attribute used to map the field, in the generated code
        /// </summary>
        public string AttributeName => "_" + PropertyName;

        /// <summary>
        /// Display name of the field, in the generated code
        /// </summary>
        public string DisplayName => SchemaField.Name!.Replace("_", " ").ToProperCase();

        protected List<string> _RelatedItems = new List<string>();
        /// <summary>
        /// The Property Names of the items where this field refers, in the generated code
        /// </summary>
        public List<string> RelatedItems => _RelatedItems;

        protected List<string> _RelatedFiles = new List<string>();
        /// <summary>
        /// The Property Names of the fields of type File, in the generated code
        /// </summary>
        public List<string> RelatedFiles => _RelatedFiles;

        /// <summary>
        /// The name of the mapped c# type for the field, in the generated code
        /// </summary>
        public abstract string TypeName { get; }

        /// <summary>
        /// Says if the mapped c# type for the field is nullable, in the generated code
        /// </summary>
        public virtual bool IsTypeNullableInAttribute { get; } = true;

        /// <summary>
        /// Says if the mapped c# type for the field is nullable, in the generated code
        /// </summary>
        public virtual bool IsTypeNullableInProperty => !IsRequired;

        /// <summary>
        /// The Type for the mapped attribute in c#
        /// </summary>
        private string TypeNameForAttribute => TypeName + (IsTypeNullableInAttribute ? "?" : "");

        /// <summary>
        /// The Type for the mapped property in c#
        /// </summary>
        private string TypeNameForProperty => TypeName + (IsTypeNullableInProperty ? "?" : "");

        /// <summary>
        /// The initial value of the mapped property for the field, in the generated code
        /// </summary>
        public virtual string InitialValueForProperty => IsTypeNullableInProperty ? "null" : "default";

        /// <summary>
        /// The initial value of the mapped attribute for the field, in the generated code
        /// </summary>
        public virtual string InitialValueForAttribute => IsTypeNullableInAttribute ? "null" : "default";

        /// <summary>
        /// Says if the Property setter must be private, in the generated code
        /// </summary>
        public virtual bool PrivateSetter { get; } = false;

        /// <summary>
        /// The type for the Filter options for the field, in the generated code
        /// </summary>
        public abstract string FilterType { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <param name="schemaFieldModel"></param>
        /// <exception cref="Exception"></exception>
        protected FieldInfo(ItemInfo itemInfo, SchemaFieldModel schemaFieldModel)
        {
            if (schemaFieldModel.Name == null) throw new Exception("Field name is empty");

            ItemInfo = itemInfo;
            SchemaField = schemaFieldModel;
        }

        /// <summary>
        /// The list of generated code for the Field
        /// </summary>
        /// <param name="settings">Generation code settings</param>
        /// <returns></returns>
        public virtual List<GeneratedCodeFile> GenerateCode(Settings settings)
        {
            return new List<GeneratedCodeFile>();
        }

        #region Property in the Item Class
        /// <summary>
        /// Returns generated lines of code for the Attribute for the field in the Item class
        /// </summary>
        /// <returns></returns>
        protected virtual List<string> GetLinesForPropertyAttribute()
        {
            return new()
            {
                $"private {TypeNameForAttribute} {AttributeName} = {InitialValueForAttribute};",
            };
        }

        /// <summary>
        /// Returns generated lines of code for the Comments of the Property for the field in the Item class
        /// </summary>
        /// <returns></returns>
        protected virtual List<string> GetLinesForPropertyComments()
        {
            return new()
            {
                $"/// <summary> Maps to '{SchemaField.Name}' field in PocketBase </summary>"
            };
        }

        /// <summary>
        /// Returns generated lines of code for the Decorators of the Property for the field in the Item class
        /// </summary>
        /// <returns></returns>
        protected virtual List<string> GetLinesForPropertyDecorators()
        {
            return new()
            {
                $@"[JsonPropertyName(""{SchemaField.Name}"")]",
                $@"[PocketBaseField(id: ""{SchemaField.Id}"", name: ""{SchemaField.Name}"", required: {(SchemaField.Required ?? false).ToString().ToLower()}, system: {(SchemaField.System ?? false).ToString().ToLower()}, unique: {(SchemaField.Unique ?? false).ToString().ToLower()}, type: ""{SchemaField.Type}"")]",
                $@"[Display(Name = ""{DisplayName}"")]",
                (SchemaField.Required ?? false) ? $@"[Required(ErrorMessage = @""{PropertyName} is required"")]" : "",
                PrivateSetter ? "[JsonInclude]" : "",
            };
        }

        /// <summary>
        /// Returns generated lines of code for the Property definition for the field in the Item class
        /// </summary>
        /// <returns></returns>
        protected virtual List<string> GetLinesForPropertyDefinition()
        {
            var list = new List<string>();
            string strGet = (InitialValueForProperty == "null") ? $"get => Get(() => {AttributeName});" : $"get => Get(() => {AttributeName} ??= {InitialValueForProperty});";
            string strSet = PrivateSetter ? $"private set => Set(value, ref {AttributeName});" : $"set => Set(value, ref {AttributeName});";

            list.Add($@"public {TypeNameForProperty} {PropertyName} {{ {strGet} {strSet} }}");

            return list;
        }

        /// <summary>
        /// Returns the generated code for the Property that represents the field in the Item class
        /// </summary>
        /// <param name="indent">The indentation for each line</param>
        /// <returns></returns>
        public string GenerateCodeForProperty(string indent)
        {
            var sb = new StringBuilder();
            foreach (var line in GetLinesForPropertyAttribute().Where(l => !string.IsNullOrEmpty(l)))
                sb.AppendLine(indent + line);
            foreach (var line in GetLinesForPropertyComments().Where(l => !string.IsNullOrEmpty(l)))
                sb.AppendLine(indent + line);
            foreach (var line in GetLinesForPropertyDecorators().Where(l => !string.IsNullOrEmpty(l)))
                sb.AppendLine(indent + line);
            foreach (var line in GetLinesForPropertyDefinition().Where(l => !string.IsNullOrEmpty(l)))
                sb.AppendLine(indent + line);

            return sb.ToString();
        }
        #endregion  Property in the Item Class

        #region Filter definition in the Item Filtering Class
        /// <summary>
        /// Returns generated lines of code for the Attribute for the filter options for the field in the Filtering class
        /// </summary>
        /// <returns></returns>
        protected virtual List<string> GetLinesForFilterAttribute()
        {
            return new();
        }

        /// <summary>
        /// Returns generated lines of code for the Comments of the Property for the filter options for the field in the Filtering class
        /// </summary>
        /// <returns></returns>
        protected virtual List<string> GetLinesForFilterComments()
        {
            return new()
            {
                $"/// <summary> Gets a Filter to Query data over the '{SchemaField.Name}' field in PocketBase </summary>"
            };
        }

        /// <summary>
        /// Returns generated lines of code for the Decorators of the Property for the filter options for the field in the Filtering class
        /// </summary>
        /// <returns></returns>
        protected virtual List<string> GetLinesForFilterDecorators()
        {
            return new();
        }

        /// <summary>
        /// Returns generated lines of code for the Property definition for the filter options for the field in the Filtering class
        /// </summary>
        /// <returns></returns>
        protected virtual List<string> GetLinesForFilterDefinition()
        {
            return new()
            {
                $@"public {FilterType} {PropertyName} => new {FilterType}(""{SchemaField.Name}"");"
            };
        }

        /// <summary>
        /// Returns the generated code for the Property that defines the filter option for the field in the Filtering class
        /// </summary>
        /// <param name="indent">The indentation for each line</param>
        /// <returns></returns>
        public string GenerateCodeForFilter(string indent)
        {
            var sb = new StringBuilder();
            foreach (var line in GetLinesForFilterAttribute().Where(l => !string.IsNullOrEmpty(l)))
                sb.AppendLine(indent + line);
            foreach (var line in GetLinesForFilterComments().Where(l => !string.IsNullOrEmpty(l)))
                sb.AppendLine(indent + line);
            foreach (var line in GetLinesForFilterDecorators().Where(l => !string.IsNullOrEmpty(l)))
                sb.AppendLine(indent + line);
            foreach (var line in GetLinesForFilterDefinition().Where(l => !string.IsNullOrEmpty(l)))
                sb.AppendLine(indent + line);

            return sb.ToString();
        }
        #endregion Filter definition in the Item Filtering Class

        #region Sort definition in the Item Sorting Class
        /// <summary>
        /// Returns generated lines of code for the Attribute for the sorting options for the field in the Sorting class
        /// </summary>
        /// <returns></returns>
        protected virtual List<string> GetLinesForSortAttribute()
        {
            return new();
        }

        /// <summary>
        /// Returns generated lines of code for the Comments of the Property for the sorting options for the field in the Sorting class
        /// </summary>
        /// <returns></returns>
        protected virtual List<string> GetLinesForSortComments()
        {
            return new()
            {
                $"/// <summary>Makes a SortCommand to Order by the '{SchemaField.Name}' field</summary>"
            };
        }

        /// <summary>
        /// Returns generated lines of code for the Decorators of the Property for the sorting options for the field in the Sorting class
        /// </summary>
        /// <returns></returns>
        protected virtual List<string> GetLinesForSortDecorators()
        {
            return new();
        }

        /// <summary>
        /// Returns generated lines of code for the Property definition for the sorting options for the field in the Sorting class
        /// </summary>
        /// <returns></returns>
        protected virtual List<string> GetLinesForSortDefinition()
        {
            return new()
            {
                $@"public SortCommand {PropertyName} => new SortCommand(""{SchemaField.Name}"");"
            };
        }

        /// <summary>
        /// Returns the generated code for the Property that defines the sorting option for the field in the Sorting class
        /// </summary>
        /// <param name="indent">The indentation for each line</param>
        /// <returns></returns>
        public string GenerateCodeForSort(string indent)
        {
            var sb = new StringBuilder();
            foreach (var line in GetLinesForSortAttribute().Where(l => !string.IsNullOrEmpty(l)))
                sb.AppendLine(indent + line);
            foreach (var line in GetLinesForSortComments().Where(l => !string.IsNullOrEmpty(l)))
                sb.AppendLine(indent + line);
            foreach (var line in GetLinesForSortDecorators().Where(l => !string.IsNullOrEmpty(l)))
                sb.AppendLine(indent + line);
            foreach (var line in GetLinesForSortDefinition().Where(l => !string.IsNullOrEmpty(l)))
                sb.AppendLine(indent + line);

            return sb.ToString();
        }
        #endregion Sort definition in the Item Sorting Class


        /// <summary>
        /// Factory for a Field info
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <param name="schemaField"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static FieldInfo NewFieldInfo(ItemInfo itemInfo, SchemaFieldModel schemaField)
        {
            if (schemaField.Type == "text")
                return new FieldInfoText(itemInfo, schemaField);
            else if (schemaField.Type == "number")
                return new FieldInfoNumber(itemInfo, schemaField);
            else if (schemaField.Type == "bool")
                return new FieldInfoBool(itemInfo, schemaField);
            else if (schemaField.Type == "email")
                return new FieldInfoEmail(itemInfo, schemaField);
            else if (schemaField.Type == "url")
                return new FieldInfoUrl(itemInfo, schemaField);
            else if (schemaField.Type == "date")
                return new FieldInfoDate(itemInfo, schemaField);
            else if (schemaField.Type == "select")
                return FieldInfoSelect.NewFieldInfoSelect(itemInfo, schemaField);
            else if (schemaField.Type == "json")
                return new FieldInfoJson(itemInfo, schemaField);
            else if (schemaField.Type == "file")
                return FieldInfoFile.NewFieldInfoFile(itemInfo, schemaField);
            else if (schemaField.Type == "relation")
                return FieldInfoRelation.NewFieldInfoRelation(itemInfo, schemaField);

            throw new Exception($"Field type '{schemaField.Type}' not supported for field '{schemaField.Name}'");
        }
    }
}
