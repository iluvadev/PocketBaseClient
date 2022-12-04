using pocketbase_csharp_sdk.Models.Collection;
using PocketBaseClient.CodeGenerator.Models;
using System.Text;
using System.Text.Json;

namespace PocketBaseClient.CodeGenerator
{
    internal class CodeGenerator
    {
        public PocketBaseSchema Schema { get; set; }
        public string OutputPath { get; set; }
        private string OutputPathModels => Path.Combine(OutputPath, "Models");
        private string OutputPathServices => Path.Combine(OutputPath, "Services");

        public string GeneratedNamespace { get; set; }
        private string GeneratedNamespaceModels => GeneratedNamespace + ".Models";
        private string GeneratedNamespaceServices => GeneratedNamespace + ".Services";

        private string? _CodeHeader = null;
        private string CodeHeader => _CodeHeader ??= $@"
// This file was generated automatically on {DateTime.UtcNow}(UTC) from the PocketBase schema for Application {Schema.Application.AppName} ({Schema.Application.AppUrl})
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

        private struct GeneratedCode
        {
            public string Path { get; set; }
            public string Content { get; set; }
        }
        private List<GeneratedCode> _GeneratedCodeList = new List<GeneratedCode>();
        private struct CollectionInfo
        {
            public string CollectionName { get; set; }
            public string CollectionId { get; set; }
            public string CollectionClassName { get; set; }
            public string ItemsClassName { get; set; }
            public CollectionModel CollectionModel { get; set; }
        }
        private List<CollectionInfo> _CollectionList = new List<CollectionInfo>();

        private void ResetGenerationData()
        {
            _CodeHeader = null;
            _GeneratedCodeList = new List<GeneratedCode>();
            _CollectionList = new List<CollectionInfo>();
        }

        public void GenerateCode()
        {
            ResetGenerationData();

            foreach (var collection in Schema.Collections)
                ProcessCollection(collection);

            foreach (var colInfo in _CollectionList)
                ProcessSchemaCollection(colInfo);

            ProcessApplicationAndService();

            foreach (var generatedCode in _GeneratedCodeList)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(generatedCode.Path) ?? OutputPath);
                File.WriteAllText(generatedCode.Path, generatedCode.Content);
            }
        }
        private void ProcessApplicationAndService()
        {
            string appClassName = (Schema.Application.AppName ?? "MyPocketBase").ToPascalCase() + "Application";
            string serviceClassName = (Schema.Application.AppName ?? "MyPocketBase").ToPascalCase() + "DataService";
            var itemCodeApp = new GeneratedCode
            {
                Path = Path.Combine(OutputPath, $"{appClassName}.cs")
            };
            StringBuilder sbApp = new StringBuilder();
            sbApp.Append($@"{CodeHeader}
using PocketBaseClient;
using {GeneratedNamespaceServices};

namespace {GeneratedNamespace}
{{
    public partial class {appClassName} : PocketBaseClientApplication
    {{
        private {serviceClassName}? _Data = null;
        public {serviceClassName} Data => _Data ??= new {serviceClassName}(this);

        #region Constructors
        public {appClassName}() : this(""{Schema.Application.AppUrl}"") {{ }}
        public {appClassName}(string url, string appName = ""{Schema.Application.AppName}"") : base(url, appName) {{ }}
        #endregion Constructors
    }}
}}
");
            itemCodeApp.Content = sbApp.ToString();
            _GeneratedCodeList.Add(itemCodeApp);

            var itemCodeService = new GeneratedCode
            {
                Path = Path.Combine(OutputPathServices, $"{serviceClassName}.cs")
            };
            StringBuilder sbService = new StringBuilder();
            sbService.Append($@"{CodeHeader}
using PocketBaseClient.Services;
using {GeneratedNamespaceModels};

namespace {GeneratedNamespaceServices}
{{
    public partial class {serviceClassName} : DataServiceBase
    {{
        #region Collections");

            foreach (var colInfo in _CollectionList)
                sbService.Append($@"
        public {colInfo.CollectionClassName} {colInfo.CollectionName} {{ get; }}");
            sbService.Append($@"

        protected override void RegisterCollections()
        {{");
            foreach (var colInfo in _CollectionList)
                sbService.Append($@"
            RegisterCollection(typeof({colInfo.ItemsClassName}), {colInfo.CollectionName});");
            sbService.Append($@"
        }}
        #endregion Collections

        #region Constructor
        public {serviceClassName}(PocketBaseClientApplication app) : base(app)
        {{
            // Collections");
            foreach (var colInfo in _CollectionList)
                sbService.Append($@"
            {colInfo.CollectionName} = new {colInfo.CollectionClassName}(this);");
            sbService.Append($@"
        }}
        #endregion Constructor
    }}
}}
");
            itemCodeService.Content = sbService.ToString();
            _GeneratedCodeList.Add(itemCodeService);

        }


        private void ProcessCollection(CollectionModel collection)
        {
            if (collection.Name == null) throw new Exception("Collection name is empty");
            if (collection.Schema == null) throw new Exception($"Schema is empty for collection {collection.Name}");
            if (collection.Id == null) throw new Exception($"Collection Id is empty for collection {collection.Name}");

            var colInfo = new CollectionInfo
            {
                CollectionName = collection.Name.Pluralize().ToPascalCase() + "Collection",
                CollectionId = collection.Id,
                CollectionClassName = "Collection" + collection.Name.Pluralize().ToPascalCase(),
                ItemsClassName = collection.Name.Singularize().ToPascalCase(),
                CollectionModel = collection,
            };
            _CollectionList.Add(colInfo);

            var colCode = new GeneratedCode
            {
                Path = Path.Combine(OutputPathModels, $"{colInfo.CollectionClassName}.cs"),
                Content = $@"{CodeHeader}
using PocketBaseClient.Orm;
using PocketBaseClient.Services;

namespace {GeneratedNamespaceModels}
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
        }

        private void ProcessSchemaCollection(CollectionInfo colInfo)
        {
            var itemCode = new GeneratedCode
            {
                Path = Path.Combine(OutputPathModels, $"{colInfo.ItemsClassName}.cs")
            };
            StringBuilder sb = new StringBuilder();
            sb.Append($@"{CodeHeader}
using pocketbase_csharp_sdk.Json;
using PocketBaseClient.Orm;
using PocketBaseClient.Orm.Attributes;
using PocketBaseClient.Orm.Json;
using PocketBaseClient.Orm.Validators;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace {GeneratedNamespaceModels}
{{
    public partial class {colInfo.ItemsClassName} : ItemBase
    {{
");
            foreach (var schemaField in colInfo.CollectionModel.Schema!)
            {
                ProcessSchemaField(colInfo, schemaField, "        ", sb);
            }
            sb.Append($@"
        public override string ToString()
        {{
            var options = new JsonSerializerOptions {{ WriteIndented = true }};
            return JsonSerializer.Serialize(this, options);
        }}
    }}
}}
");
            itemCode.Content = sb.ToString();
            _GeneratedCodeList.Add(itemCode);
        }

        private void ProcessSchemaField(CollectionInfo colInfo, SchemaFieldModel schemaField, string indent, StringBuilder sb)
        {
            string propertyName = schemaField.Name?.ToPascalCase() ?? throw new Exception("Field name is missing");

            sb.Append(GetVarForSchemaField(colInfo, schemaField, indent, propertyName));
            sb.Append(GetDecoratorsForSchemaField(schemaField, indent, propertyName));
            sb.Append(GetPropertyForSchemaField(schemaField, indent, propertyName));
            sb.AppendLine();
        }

        private string GetTypeForSchemaType(SchemaFieldModel schemaField, string propertyName, out string initialValue)
        {
            string schemaType = schemaField.Type!;
            initialValue = "null";

            if (schemaType == "text") return $"string?";
            if (schemaType == "number") return $"int?";
            if (schemaType == "bool") return $"bool?";
            if (schemaType == "email") return $"MailAddress?";
            if (schemaType == "url") return $"Uri?";
            if (schemaType == "date") return $"DateTime?";

            if (schemaType == "select")
            {
                var options = JsonSerializer.Deserialize<PocketBaseFieldOptionsSelect>(JsonSerializer.Serialize(schemaField.Options)) ?? new PocketBaseFieldOptionsSelect();
                if (options.IsSinglSelect) return $"{propertyName}Enum?";
                if (options.MaxSelect != null)
                {
                    initialValue = $"new {propertyName}List()";
                    return $"{propertyName}List";
                }
                initialValue = $"new List<{propertyName}Enum>()";
                return $"List<{propertyName}Enum>";
            }
            if (schemaType == "json") return $"dynamic?";

            if (schemaType == "file")
            {
                return $"object?";
            }
            if (schemaType == "relation")
            {
                var options = JsonSerializer.Deserialize<PocketBaseFieldOptionsRelation>(JsonSerializer.Serialize(schemaField.Options)) ?? new PocketBaseFieldOptionsRelation();
                var colInfo = _CollectionList.First(c => c.CollectionId == options.CollectionId);
                if (options.IsSinglSelect) return $"{colInfo.ItemsClassName}?";
                if (options.MaxSelect != null)
                {
                    initialValue = $"new {propertyName}List()";
                    return $"{propertyName}List";
                }
                initialValue = $"new List<{colInfo.ItemsClassName}>()";
                return $"List<{colInfo.ItemsClassName}>";
            }

            return $"object";
        }
        private StringBuilder GetVarForSchemaField(CollectionInfo colInfo, SchemaFieldModel schemaField, string indent, string propertyName)
        {
            var sb = new StringBuilder();

            if (schemaField.Type == "select")
            {
                CreateEnumForField(colInfo, schemaField, propertyName);
                var options = JsonSerializer.Deserialize<PocketBaseFieldOptionsSelect>(JsonSerializer.Serialize(schemaField.Options)) ?? new PocketBaseFieldOptionsSelect();
                if ((options?.MaxSelect ?? 0) > 1)
                    CreateLimitedListForField(colInfo, propertyName, $"{propertyName}Enum", options!.MaxSelect!.Value);
            }
            if (schemaField.Type == "relation")
            {
                var options = JsonSerializer.Deserialize<PocketBaseFieldOptionsRelation>(JsonSerializer.Serialize(schemaField.Options)) ?? new PocketBaseFieldOptionsRelation();
                if ((options?.MaxSelect ?? 0) > 1)
                    CreateLimitedListForField(colInfo, propertyName, colInfo.ItemsClassName, options!.MaxSelect!.Value);
            }

            sb.AppendLine(@$"{indent}private {GetTypeForSchemaType(schemaField, propertyName, out var initialVal)} _{propertyName} = {initialVal};");

            return sb;
        }

        private void CreateEnumForField(CollectionInfo colInfo, SchemaFieldModel schemaField, string propertyName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($@"{CodeHeader}
using System.ComponentModel;

namespace {GeneratedNamespaceModels}
{{
    public partial class {colInfo.ItemsClassName}
    {{
        public enum {propertyName}Enum
        {{
");
            string indent = "            ";
            var options = JsonSerializer.Deserialize<PocketBaseFieldOptionsSelect>(JsonSerializer.Serialize(schemaField.Options)) ?? new PocketBaseFieldOptionsSelect();
            foreach (var value in options.Values ?? new List<string>())
            {
                sb.AppendLine(@$"{indent}[Description(""{value}"")]");
                sb.AppendLine(@$"{indent}{value.Singularize().ToPascalCase()},");
                sb.AppendLine();
            }
            sb.Append($@"
        }}
    }}
}}
");
            _GeneratedCodeList.Add(new GeneratedCode
            {
                Path = Path.Combine(OutputPathModels, $"{colInfo.ItemsClassName}.{propertyName}Enum.cs"),
                Content = sb.ToString(),
            });
        }

        private void CreateLimitedListForField(CollectionInfo colInfo, string propertyName, string propertyType, int limit)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($@"{CodeHeader}
using PocketBaseClient.Orm;

namespace {GeneratedNamespaceModels}
{{
    public partial class {colInfo.ItemsClassName}
    {{
        public class {propertyName}List : LimitedList<{propertyType}>
        {{
            public {propertyName}List() : base({limit}) {{ }}
        }}
    }}
}}
");
            _GeneratedCodeList.Add(new GeneratedCode
            {
                Path = Path.Combine(OutputPathModels, $"{colInfo.ItemsClassName}.{propertyName}List.cs"),
                Content = sb.ToString(),
            });
        }

        private StringBuilder GetDecoratorsForSchemaField(SchemaFieldModel schemaField, string indent, string propertyName)
        {
            var sb = new StringBuilder();
            sb.AppendLine($@"{indent}[JsonPropertyName(""{schemaField.Name}"")]");
            sb.AppendLine($@"{indent}[PocketBaseField(id: ""{schemaField.Id}"", name: ""{schemaField.Name}"", required: {(schemaField.Required ?? false).ToString().ToLower()}, system: {(schemaField.System ?? false).ToString().ToLower()}, unique: {(schemaField.Unique ?? false).ToString().ToLower()}, type: ""{schemaField.Type}"")]");
            if (schemaField.Required ?? false)
                sb.AppendLine($@"{indent}[Required(ErrorMessage = @""{schemaField.Name} is required"")]");
            if (schemaField.Type == "text")
            {
                var options = JsonSerializer.Deserialize<PocketBaseFieldOptionsText>(JsonSerializer.Serialize(schemaField.Options)) ?? new PocketBaseFieldOptionsText();
                if (options.Min != null) options.Max ??= int.MaxValue;
                if (options.Max != null)
                {
                    sb.Append($@"{indent}[StringLength({options.Max}");
                    if (options.Min != null) sb.Append($@", MinimumLength = {options.Min}");
                    sb.Append($@", ErrorMessage = """);
                    if (options.Min != null) sb.Append($@"Minimum {options.Min}, ");
                    sb.AppendLine($@"Maximum {options.Max} characters"")]");
                }
                if (!string.IsNullOrEmpty(options.Pattern))
                    sb.AppendLine($@"{indent}[RegularExpression(@""{options.Pattern}"", ErrorMessage = @""Pattern '{options.Pattern}' not match"")]");
            }
            else if (schemaField.Type == "number")
            {
                var options = JsonSerializer.Deserialize<PocketBaseFieldOptionsNumber>(JsonSerializer.Serialize(schemaField.Options)) ?? new PocketBaseFieldOptionsNumber();
                if (options.Max != null || options.Min != null)
                {
                    options.Min ??= int.MinValue;
                    options.Max ??= int.MaxValue;
                    sb.AppendLine($@"{indent}[Range({options.Min}, {options.Max}, ErrorMessage = ""Minimum {options.Min}, Maximum {options.Max}"")]");
                }
            }
            else if (schemaField.Type == "bool")
            {
            }
            else if (schemaField.Type == "email")
            {
                sb.AppendLine($@"{indent}[JsonConverter(typeof(EmailConverter))]");
                var options = JsonSerializer.Deserialize<PocketBaseFieldOptionsEmailUrl>(JsonSerializer.Serialize(schemaField.Options)) ?? new PocketBaseFieldOptionsEmailUrl();
                if (options.OnlyDomains != null)
                    sb.AppendLine($@"{indent}[OnlyDomains(""{options.OnlyDomainsJoined}"", ErrorMessage = ""Only domains accepted: '{options.OnlyDomainsJoined}'"")]");
                else if (options.ExceptDomains != null)
                    sb.AppendLine($@"{indent}[ExceptDomains(""{options.ExceptDomainsJoined}"", ErrorMessage = ""Except domains accepted: '{options.ExceptDomainsJoined}'"")]");
            }
            else if (schemaField.Type == "url")
            {
                sb.AppendLine($@"{indent}[JsonConverter(typeof(UrlConverter))]");
                var options = JsonSerializer.Deserialize<PocketBaseFieldOptionsEmailUrl>(JsonSerializer.Serialize(schemaField.Options)) ?? new PocketBaseFieldOptionsEmailUrl();
                if (options.OnlyDomains != null)
                    sb.AppendLine($@"{indent}[OnlyDomains(""{options.OnlyDomainsJoined}"", ErrorMessage = ""Only domains accepted: '{options.OnlyDomainsJoined}'"")]");
                else if (options.ExceptDomains != null)
                    sb.AppendLine($@"{indent}[ExceptDomains(""{options.ExceptDomainsJoined}"", ErrorMessage = ""Except domains accepted: '{options.ExceptDomainsJoined}'"")]");
            }
            else if (schemaField.Type == "date")
            {
                sb.AppendLine($@"{indent}[JsonConverter(typeof(DateTimeConverter))]");
                var options = JsonSerializer.Deserialize<PocketBaseFieldOptionsDatetime>(JsonSerializer.Serialize(schemaField.Options)) ?? new PocketBaseFieldOptionsDatetime();
                if (options.Max != null || options.Min != null)
                {
                    options.Min ??= DateTime.MinValue.ToUniversalTime();
                    options.Max ??= DateTime.MaxValue.ToUniversalTime();
                    sb.AppendLine($@"{indent}[Range(typeof(DateTime), ""{options.Min}"", ""{options.Max}"", ErrorMessage = ""Minimum '{options.Min}', Maximum '{options.Max}'"")]");
                }
            }
            else if (schemaField.Type == "select")
            {
                var options = JsonSerializer.Deserialize<PocketBaseFieldOptionsSelect>(JsonSerializer.Serialize(schemaField.Options)) ?? new PocketBaseFieldOptionsSelect();
                if (options.IsSinglSelect)
                    sb.AppendLine($@"{indent}[JsonConverter(typeof(EnumConverter<{propertyName}Enum>))]");
                else
                    sb.AppendLine($@"{indent}[JsonConverter(typeof(ListEnumConverter<{propertyName}List, {propertyName}Enum>))]");
            }
            else if (schemaField.Type == "json")
            {
            }
            else if (schemaField.Type == "file")
            {
            }
            else if (schemaField.Type == "relation")
            {
                //var options = JsonSerializer.Deserialize<PocketBaseFieldOptionsRelation>(JsonSerializer.Serialize(schemaField.Options)) ?? new PocketBaseFieldOptionsRelation();
                //var colInfo = _CollectionList.First(c => c.CollectionId == options.CollectionId);
                //if (options.IsSinglSelect)
                //    sb.AppendLine($@"{indent}[JsonConverter(typeof(RelationConverter<{colInfo.ItemsClassName}>))]");
                //else if(options.MaxSelect!=null)
                //    sb.AppendLine($@"{indent}[JsonConverter(typeof(ListEnumConverter<{propertyName}List, {colInfo.ItemsClassName}>))]");
                //else //List
                //    sb.AppendLine($@"{indent}[JsonConverter(typeof(ListEnumConverter<{propertyName}List, {colInfo.ItemsClassName}>))]");
            }
            return sb;
        }

        private StringBuilder GetPropertyForSchemaField(SchemaFieldModel schemaField, string indent, string propertyName)
        {
            var sb = new StringBuilder();
            var propertyType = GetTypeForSchemaType(schemaField, propertyName, out var initialVal);
            sb.AppendLine($@"{indent}public {propertyType} {propertyName}");
            sb.AppendLine($@"{indent}{{");
            sb.AppendLine((initialVal == "null") ? 
                          $@"{indent}   get => Get(() => _{propertyName});" : 
                          $@"{indent}   get => Get(() => _{propertyName} ??= {initialVal});");
            sb.AppendLine($@"{indent}   set => Set(value, ref _{propertyName});");
            sb.AppendLine($@"{indent}}}");

            return sb;
        }
    }
}
