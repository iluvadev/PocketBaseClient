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
using PocketBaseClient.Orm.Filters;
using System.Text;

namespace PocketBaseClient.CodeGenerator.Generation
{
    internal abstract class FieldInfo
    {
        public ItemInfo ItemInfo { get; }
        public SchemaFieldModel SchemaField { get; }

        public string PropertyName => SchemaField.Name!.ToPascalCase();
        public string AttributeName => "_" + PropertyName;
        public string DisplayName => SchemaField.Name!.ToProperCase();

        protected List<string> _RelatedItems = new List<string>();
        public List<string> RelatedItems => _RelatedItems;

        public abstract string TypeName { get; }
        public virtual string InitialValueForProperty { get; } = "null";
        public virtual string InitialValueForAttribute { get; } = "null";

        public virtual bool PrivateSetter { get; } = false;

        public abstract string FilterType { get; }

        protected FieldInfo(ItemInfo itemInfo, SchemaFieldModel schemaFieldModel)
        {
            if (schemaFieldModel.Name == null) throw new Exception("Field name is empty");

            ItemInfo = itemInfo;
            SchemaField = schemaFieldModel;
        }

        public virtual List<GeneratedCodeFile> GenerateCode(Settings settings)
        {
            return new List<GeneratedCodeFile>();
        }

        protected virtual List<string> GetLinesForPropertyAttribute()
        {
            return new()
            {
                $"private {TypeName} {AttributeName} = {InitialValueForAttribute};",
            };
        }
        protected virtual List<string> GetLinesForPropertyComments()
        {
            return new()
            {
                $"/// <summary> Maps to '{SchemaField.Name}' field in PocketBase </summary>"
            };
        }

        protected virtual List<string> GetLinesForPropertyDecorators()
        {
            return new()
            {
                @$"[JsonPropertyName(""{SchemaField.Name}"")]",
                $@"[PocketBaseField(id: ""{SchemaField.Id}"", name: ""{SchemaField.Name}"", required: {(SchemaField.Required ?? false).ToString().ToLower()}, system: {(SchemaField.System ?? false).ToString().ToLower()}, unique: {(SchemaField.Unique ?? false).ToString().ToLower()}, type: ""{SchemaField.Type}"")]",
                $@"[Display(Name = ""{DisplayName}"")]",
                (SchemaField.Required ?? false) ? $@"[Required(ErrorMessage = @""{PropertyName} is required"")]" : "",
            };
        }

        protected virtual List<string> GetLinesForPropertyDefinition()
        {
            var list = new List<string>();
            string strGet = (InitialValueForAttribute == "null") ? $"get => Get(() => {AttributeName});" : $"get => Get(() => {AttributeName} ??= {InitialValueForProperty});";
            string strSet = PrivateSetter ? $"private set => Set(value, ref {AttributeName});" : $"set => Set(value, ref {AttributeName});";

            list.Add($@"public {TypeName} {PropertyName} {{ {strGet} {strSet} }}");

            return list;
        }

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

        protected virtual List<string> GetLinesForFilterAttribute()
        {
            return new();
        }
        protected virtual List<string> GetLinesForFilterComments()
        {
            return new()
            {
                $"/// <summary> Gets a Filter to Query data over the '{SchemaField.Name}' field in PocketBase </summary>"
            };
        }
        protected virtual List<string> GetLinesForFilterDecorators()
        {
            return new();
        }
        protected virtual List<string> GetLinesForFilterDefinition()
        {
            return new()
            {
                $@"public {FilterType} {PropertyName} => new {FilterType}(""{SchemaField.Name}"");"
            };
        }
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


        protected virtual List<string> GetLinesForSortAttribute()
        {
            return new();
        }
        protected virtual List<string> GetLinesForSortComments()
        {
            return new()
            {
                $"/// <summary>Makes a SortCommand to Order by the '{SchemaField.Name}' field</summary>"
            };
        }
        protected virtual List<string> GetLinesForSortDecorators()
        {
            return new();
        }
        protected virtual List<string> GetLinesForSortDefinition()
        {
            return new()
            {
                $@"public SortCommand {PropertyName} => new SortCommand(""{SchemaField.Name}"");"
            };
        }
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
                return new FieldInfoSelect(itemInfo, schemaField);
            else if (schemaField.Type == "json")
                return new FieldInfoJson(itemInfo, schemaField);
            else if (schemaField.Type == "file")
                return new FieldInfoFile(itemInfo, schemaField);
            else if (schemaField.Type == "relation")
                return new FieldInfoRelation(itemInfo, schemaField);

            throw new Exception($"Field type '{schemaField.Type}' not supported for field '{schemaField.Name}'");
        }
    }
}
