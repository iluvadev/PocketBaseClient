using pocketbase_csharp_sdk.Models.Collection;
using PocketBaseClient.CodeGenerator.Models;
using System.Text.Json;

namespace PocketBaseClient.CodeGenerator
{
    internal class CodeGenerator
    {
        public PocketBaseSchema Schema { get; set; }
        public string OutputPath { get; set; }
        public string GeneratedNamespace { get; set; }

        private string? _CodeHeader = null;
        private string CodeHeader => _CodeHeader ??= $@"
// This file was generated automatically on {DateTime.UtcNow} from the PocketBase schema for Application {Schema.Application.AppName} ({Schema.Application.AppUrl})
//
// PocketBaseClient-csharp project: https://github.com/iluvadev/PocketBaseClient-csharp
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase
";

        public CodeGenerator(PocketBaseSchema schema, string outputPath, string? generatedNamespace = null)
        {
            Schema = schema;
            OutputPath = outputPath;
            GeneratedNamespace = generatedNamespace ?? $"{schema.Application.AppName?.ToPascalCase() ?? "PocketBaseClient"}.Models";
        }


        public void GenerateCode()
        {
            ResetGenerationData();
            foreach (var collection in Schema.Collections)
            {
                ProcessCollection(collection);
            }

            foreach (var generatedCode in _GeneratedCodeList)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(generatedCode.Path) ?? OutputPath);
                File.WriteAllText(generatedCode.Path, generatedCode.Content);
            }
        }

        private struct GeneratedCode
        {
            public string Path { get; set; }
            public string Content { get; set; }
        }
        private List<GeneratedCode> _GeneratedCodeList = new List<GeneratedCode>();
        private struct CollectionInfo
        {
            public string CollectionClassName { get; set; }
            public string ItemsClassName { get; set; }
        }
        private List<CollectionInfo> _CollectionList = new List<CollectionInfo>();

        private void ResetGenerationData()
        {
            _CodeHeader = null;
            _GeneratedCodeList = new List<GeneratedCode>();
            _CollectionList = new List<CollectionInfo>();
        }

        private void ProcessCollection(CollectionModel collection)
        {
            if (collection.Name == null) throw new Exception("Collection name is empty");
            if (collection.Schema == null) throw new Exception($"Schema is empty for collection {collection.Name}");

            var colInfo = new CollectionInfo
            {
                CollectionClassName = "Collection" + collection.Name.Pluralize().ToPascalCase(),
                ItemsClassName = collection.Name.Singularize().ToPascalCase()
            };
            _CollectionList.Add(colInfo);

            var colCode = new GeneratedCode
            {
                Path = Path.Combine(OutputPath, $"{colInfo.CollectionClassName}.cs"),
                Content = $@"{CodeHeader}
using PocketBaseClient.Orm;
using PocketBaseClient.Services;

namespace {GeneratedNamespace}
{{
    public partial class {colInfo.CollectionClassName} : CollectionBase<{colInfo.ItemsClassName}>
    {{
        public override string Id => ""{collection.Id}"";
        public override string Name => ""{collection.Name}"";
        public override bool System => {(collection.System ?? false).ToString().ToLower()};

        public {colInfo.CollectionClassName}(DataServiceBase context) : base(context) {{ }}
    }}
}}
"
            };
            _GeneratedCodeList.Add(colCode);

            var itemCode = new GeneratedCode
            {
                Path = Path.Combine(OutputPath, $"{colInfo.ItemsClassName}.cs")
            };
            var itemContent = $@"{CodeHeader}
using PocketBaseClient.Orm;
using PocketBaseClient.Orm.Attributes;
using PocketBaseClient.Orm.Json;
using PocketBaseClient.Orm.Validators;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text.Json.Serialization;

namespace {GeneratedNamespace}
{{
    public partial class {colInfo.ItemsClassName} : ItemBase
    {{
";
            foreach (var schemaField in collection.Schema)
            {
                itemContent += ProcessSchemaField(schemaField);
            }
            itemContent += $@"
    }}
}}
";
            itemCode.Content = itemContent;
            _GeneratedCodeList.Add(itemCode);
        }

        private string ProcessSchemaField(SchemaFieldModel schemaField)
        {
            string propertyName = schemaField.Name?.ToPascalCase() ?? throw new Exception("Field name is missing");

            string indent = "        ";
            string code = $@"{indent}[JsonPropertyName(""{schemaField.Name}"")]{Environment.NewLine}";
            code += $@"{indent}[PocketBaseField(""{schemaField.Id}"", ""{schemaField.Name}"", {(schemaField.Required ?? false).ToString().ToLower()}, {(schemaField.System ?? false).ToString().ToLower()}, {(schemaField.Unique ?? false).ToString().ToLower()}, ""{schemaField.Type}"")]{Environment.NewLine}";
            if (schemaField.Required ?? false)
                code += $@"{indent}[Required(ErrorMessage = @""{schemaField.Name} is required"")]{Environment.NewLine}";
            if (schemaField.Type == "text")
            {
                var options = JsonSerializer.Deserialize<PocketBaseFieldOptionsText>(JsonSerializer.Serialize(schemaField.Options)) ?? new PocketBaseFieldOptionsText();
                if (options.Min != null) options.Max ??= int.MaxValue;
                if (options.Max != null)
                {
                    code += $@"{indent}[StringLength({options.Max}";
                    if (options.Min != null) code += $@", MinimumLength = {options.Min}";
                    code += $@", ErrorMessage = """;
                    if (options.Min != null) code += $@"Minimum {options.Min}, ";
                    code += $@"Maximum {options.Max} characters"")]{Environment.NewLine}";
                }
                if (!string.IsNullOrEmpty(options.Pattern))
                    code += $@"{indent}[RegularExpression(@""{options.Pattern}"", ErrorMessage = @""Pattern '{options.Pattern}' not match"")]{Environment.NewLine}";
                code += $@"{indent}public string? {propertyName} {{ get; set; }}{Environment.NewLine}{Environment.NewLine}";
            }
            else if (schemaField.Type == "number")
            {
                var options = JsonSerializer.Deserialize<PocketBaseFieldOptionsNumber>(JsonSerializer.Serialize(schemaField.Options)) ?? new PocketBaseFieldOptionsNumber();
                if (options.Max != null || options.Min != null)
                {
                    options.Min ??= int.MinValue;
                    options.Max ??= int.MaxValue;
                    code += $@"{indent}[Range({options.Min}, {options.Max}, ErrorMessage = ""Minimum {options.Min}, Maximum {options.Max}"")]{Environment.NewLine}";
                }
                code += $@"{indent}public int? {propertyName} {{ get; set; }}{Environment.NewLine}{Environment.NewLine}";
            }
            else if (schemaField.Type == "bool")
            {
                code += $@"{indent}public bool? {propertyName} {{ get; set; }}{Environment.NewLine}{Environment.NewLine}";
            }
            else if (schemaField.Type == "email")
            {
                code += $@"{indent}[JsonConverter(typeof(EmailConverter))]{Environment.NewLine}";
                var options = JsonSerializer.Deserialize<PocketBaseFieldOptionsEmailUrl>(JsonSerializer.Serialize(schemaField.Options)) ?? new PocketBaseFieldOptionsEmailUrl();
                if (options.OnlyDomains != null)
                    code += $@"{indent}[OnlyDomains(""{options.OnlyDomainsJoined}"", ErrorMessage = ""Only domains accepted: '{options.OnlyDomainsJoined}'"")]{Environment.NewLine}";
                else if (options.ExceptDomains != null)
                    code += $@"{indent}[ExceptDomains(""{options.ExceptDomainsJoined}"", ErrorMessage = ""Except domains accepted: '{options.ExceptDomainsJoined}'"")]{Environment.NewLine}";
                code += $@"{indent}public MailAddress? {propertyName} {{ get; set; }}{Environment.NewLine}{Environment.NewLine}";
            }
            else if (schemaField.Type == "url")
            {
                code += $@"{indent}[JsonConverter(typeof(UrlConverter))]{Environment.NewLine}";
                var options = JsonSerializer.Deserialize<PocketBaseFieldOptionsEmailUrl>(JsonSerializer.Serialize(schemaField.Options)) ?? new PocketBaseFieldOptionsEmailUrl();
                if (options.OnlyDomains != null)
                    code += $@"{indent}[OnlyDomains(""{options.OnlyDomainsJoined}"", ErrorMessage = ""Only domains accepted: '{options.OnlyDomainsJoined}'"")]{Environment.NewLine}";
                else if (options.ExceptDomains != null)
                    code += $@"{indent}[ExceptDomains(""{options.ExceptDomainsJoined}"", ErrorMessage = ""Except domains accepted: '{options.ExceptDomainsJoined}'"")]{Environment.NewLine}";
                code += $@"{indent}public Uri? {propertyName} {{ get; set; }}{Environment.NewLine}{Environment.NewLine}";
            }
            else if (schemaField.Type == "date")
            {
                code += $@"{indent}[JsonConverter(typeof(DateTimeConverter))]{Environment.NewLine}";
                var options = JsonSerializer.Deserialize<PocketBaseFieldOptionsDatetime>(JsonSerializer.Serialize(schemaField.Options)) ?? new PocketBaseFieldOptionsDatetime();
                if (options.Max != null || options.Min != null)
                {
                    options.Min ??= DateTime.MinValue.ToUniversalTime();
                    options.Max ??= DateTime.MaxValue.ToUniversalTime();
                    code += $@"{indent}[Range(typeof(DateTime), ""{options.Min}"", ""{options.Max}"", ErrorMessage = ""Minimum '{options.Min}', Maximum '{options.Max}'"")]{Environment.NewLine}";
                }
                code += $@"{indent}public DateTime? {propertyName} {{ get; set; }}{Environment.NewLine}{Environment.NewLine}";
            }
            else if (schemaField.Type == "select")
            {

                code += $@"{indent}public object? {propertyName} {{ get; set; }}{Environment.NewLine}{Environment.NewLine}";
            }
            else if (schemaField.Type == "json")
            {

                code += $@"{indent}public object? {propertyName} {{ get; set; }}{Environment.NewLine}{Environment.NewLine}";
            }
            else if (schemaField.Type == "file")
            {

                code += $@"{indent}public object? {propertyName} {{ get; set; }}{Environment.NewLine}{Environment.NewLine}";
            }
            else if (schemaField.Type == "relation")
            {

                code += $@"{indent}public object? {propertyName} {{ get; set; }}{Environment.NewLine}{Environment.NewLine}";
            }

            return code;
        }
    }
}
