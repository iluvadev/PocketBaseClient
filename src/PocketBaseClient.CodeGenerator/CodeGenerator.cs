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
using PocketBaseClient.CodeGenerator.Helpers;
using PocketBaseClient.CodeGenerator.Models;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace PocketBaseClient.CodeGenerator
{
    internal class CodeGenerator
    {
        public static string GeneratedPropertiesFileName = "pbcodegen.json";

        public PocketBaseSchema Schema { get; set; }
        public string OutputPath { get; set; }
        private string OutputPathModels => Path.Combine(OutputPath, "Models");
        private string OutputPathServices => Path.Combine(OutputPath, "Services");
        private string OutputProjectFileName => Path.Combine(OutputPath, $"{Schema.ProjectName}.csproj");
        private string OutputSummaryFileName => Path.Combine(OutputPath, "CodeGenerationSummary.txt");
        private string SchemaFileName => Path.Combine(OutputPath, GeneratedPropertiesFileName);

        public string GeneratedNamespace => Schema.Namespace;
        private string GeneratedNamespaceModels => GeneratedNamespace + ".Models";
        private string GeneratedNamespaceServices => GeneratedNamespace + ".Services";

        private string? _CodeHeader = null;
        private string CodeHeader => _CodeHeader ??= $@"
// This file was generated automatically for the PocketBase Application {Schema.PocketBaseApplication.Name} ({Schema.PocketBaseApplication.Url})
//    See CodeGenerationSummary.txt for more details
//
// PocketBaseClient-csharp project: https://github.com/iluvadev/PocketBaseClient-csharp
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase
";

        public CodeGenerator(PocketBaseSchema schema, string outputPath)
        {
            Schema = schema;
            OutputPath = outputPath;
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
            ProcessProject();
            ProcessGenerationSummary();

            foreach (var generatedCode in _GeneratedCodeList)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(generatedCode.Path) ?? OutputPath);
                File.WriteAllText(generatedCode.Path, generatedCode.Content);
            }

            Schema.SaveToFile(SchemaFileName);
        }

        private void ProcessProject()
        {
            _GeneratedCodeList.Add(
                new GeneratedCode
                {
                    Path = OutputProjectFileName,
                    Content = @"
<Project Sdk=""Microsoft.NET.Sdk"">

  <PropertyGroup>
    <TargetFramework>net6.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include=""PocketBaseClient"" Version=""*"" />
  </ItemGroup>

</Project>
"
                });
        }

        private void ProcessGenerationSummary()
        {
            var appName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            var appVer = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

            var summaryFile = new GeneratedCode
            {
                Path = OutputSummaryFileName
            };

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"
PocketBaseClient-csharp project: https://github.com/iluvadev/PocketBaseClient-csharp
Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE

pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk
pocketbase project: https://github.com/pocketbase/pocketbase
");
            sb.AppendLine(Program.Banner);
            sb.AppendLine($@"
Code generated with:
    PocketBaseClient CodeGenerator: {appName} v.{appVer}
    At: {DateTime.UtcNow.ToString("O")}

PocketBase Application: 
    Name: {Schema.PocketBaseApplication.Name}
    Url: {Schema.PocketBaseApplication.Url}
    Schema Date: {Schema.SchemaDate.ToString("O")}

Files Generated:");
            foreach (var strFile in _GeneratedCodeList.Select(c => c.Path).OrderBy(c => c))
                sb.AppendLine($"    {strFile}");

            summaryFile.Content = sb.ToString();

            _GeneratedCodeList.Add(summaryFile);
        }

        private void ProcessApplicationAndService()
        {
            string appClassName = (Schema.PocketBaseApplication.Name ?? "MyPocketBase").ToPascalCase() + "Application";
            string serviceClassName = (Schema.PocketBaseApplication.Name ?? "MyPocketBase").ToPascalCase() + "DataService";
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
        public {appClassName}() : this(""{Schema.PocketBaseApplication.Url}"") {{ }}
        public {appClassName}(string url, string appName = ""{Schema.PocketBaseApplication.Name}"") : base(url, appName) {{ }}
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
using PocketBaseClient;
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
            RegisterCollection(typeof({GeneratedNamespaceModels}.{colInfo.ItemsClassName}), {colInfo.CollectionName});");
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

            RegisterCollections();
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

            var collectionNaturalName = collection.Name.Replace("_", " ");

            var colInfo = new CollectionInfo
            {
                CollectionName = collectionNaturalName.Pluralize().ToPascalCase() + "Collection",
                CollectionId = collection.Id,
                CollectionClassName = "Collection" + collectionNaturalName.Pluralize().ToPascalCase(),
                ItemsClassName = collectionNaturalName.Singularize().ToPascalCase(),
                CollectionModel = collection,
            };
            _CollectionList.Add(colInfo);

            var colCode = new GeneratedCode
            {
                Path = Path.Combine(OutputPathModels, $"{colInfo.CollectionClassName}.cs"),
                Content = $@"{CodeHeader}
using PocketBaseClient.Orm;
using PocketBaseClient.Orm.Filters;
using PocketBaseClient.Services;

namespace {GeneratedNamespaceModels}
{{
    public partial class {colInfo.CollectionClassName} : CollectionBase<{colInfo.ItemsClassName}>
    {{
        public override string Id => ""{collection.Id}"";
        public override string Name => ""{collection.Name}"";
        public override bool System => {(collection.System ?? false).ToString().ToLower()};

        public {colInfo.CollectionClassName}(DataServiceBase context) : base(context) {{ }}


        public CollectionQuery<{colInfo.CollectionClassName}, {colInfo.ItemsClassName}> Filter(string filterString)
             => new CollectionQuery<{colInfo.CollectionClassName}, {colInfo.ItemsClassName}>(this, FilterQuery.Create(filterString));

        public CollectionQuery<{colInfo.CollectionClassName}, {colInfo.ItemsClassName}> Filter(Func<{colInfo.ItemsClassName}.Filters, FilterQuery> filter)
            => new CollectionQuery<{colInfo.CollectionClassName}, {colInfo.ItemsClassName}>(this, filter(new {colInfo.ItemsClassName}.Filters()));

    }}
}}
"
            };
            _GeneratedCodeList.Add(colCode);
        }

        private void ProcessSchemaCollection(CollectionInfo colInfo)
        {
            ProcessSchemaItemCollection(colInfo);
            ProcessSchemaItemFilterCollection(colInfo);
        }
        private void ProcessSchemaItemCollection(CollectionInfo colInfo)
        {
            var itemCode = new GeneratedCode
            {
                Path = Path.Combine(OutputPathModels, $"{colInfo.ItemsClassName}.cs")
            };
            List<string> relatedItems = new List<string>();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($@"{CodeHeader}
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

namespace {GeneratedNamespaceModels}
{{
    public partial class {colInfo.ItemsClassName} : ItemBase
    {{
        #region Collection
        private static CollectionBase? _Collection = null;
        [JsonIgnore]
        public override CollectionBase Collection => _Collection ??= DataServiceBase.GetCollection<{colInfo.ItemsClassName}>()!;
        #endregion Collection

        #region Field Properties");
            foreach (var schemaField in colInfo.CollectionModel.Schema!)
                ProcessSchemaField(colInfo, schemaField, "        ", sb, relatedItems);

            sb.AppendLine($@"
        #endregion Field Properties

        public override void UpdateWith(ItemBase itemBase)
        {{
            base.UpdateWith(itemBase);

            if (itemBase is {colInfo.ItemsClassName} item)
            {{");
            foreach (var propertyName in colInfo.CollectionModel.Schema!.Where(s => s?.Name != null).Select(s => s.Name!.ToPascalCase()))
                sb.AppendLine($@"                {propertyName} = item.{propertyName};");
            sb.AppendLine($@"
            }}
        }}");

            if (relatedItems.Any())
            {
                sb.Append($@"
        protected override IEnumerable<ItemBase?> RelatedItems 
            => base.RelatedItems");
                foreach (var relatedItem in relatedItems)
                    sb.Append($@"{relatedItem}");
                sb.AppendLine(";");
            }
            sb.AppendLine($@"
        #region Collection
        public static {colInfo.CollectionClassName} GetCollection() 
            => ({colInfo.CollectionClassName})DataServiceBase.GetCollection<{colInfo.ItemsClassName}>()!;
        #endregion Collection


        #region GetById
        public static {colInfo.ItemsClassName}? GetById(string id, bool reload = false) 
            => GetByIdAsync(id, reload).Result;

        public static async Task<{colInfo.ItemsClassName}?> GetByIdAsync(string id, bool reload = false)
            => await DataServiceBase.GetCollection<{colInfo.ItemsClassName}>()!.GetByIdAsync(id, reload);
        #endregion GetById
    }}
}}");
            itemCode.Content = sb.ToString();
            _GeneratedCodeList.Add(itemCode);
        }

        private void ProcessSchemaField(CollectionInfo colInfo, SchemaFieldModel schemaField, string indent, StringBuilder sb, List<string> relatedItems)
        {
            string propertyName = schemaField.Name?.ToPascalCase() ?? throw new Exception("Field name is missing");

            sb.Append(GetVarForSchemaField(colInfo, schemaField, indent, propertyName, relatedItems));
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
                initialValue = $"new LimitableList<{propertyName}Enum>()";
                return $"LimitableList<{propertyName}Enum>";
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
                initialValue = $"new LimitableList<{colInfo.ItemsClassName}>()";
                return $"LimitableList<{colInfo.ItemsClassName}>";
            }

            return $"object";
        }
        private StringBuilder GetVarForSchemaField(CollectionInfo colInfo, SchemaFieldModel schemaField, string indent, string propertyName, List<string> relatedItems)
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
                if (options != null)
                {
                    if (options.MaxSelect == 1)
                        relatedItems.Add(@$".Union(new List<ItemBase?>() {{ {propertyName} }})");
                    else
                    {
                        relatedItems.Add(@$".Union({propertyName})");
                        if (options.MaxSelect > 1)
                        {
                            var colRelatedInfo = _CollectionList.First(c => c.CollectionId == options.CollectionId);
                            CreateLimitedListForField(colInfo, propertyName, colRelatedInfo.ItemsClassName, options!.MaxSelect!.Value);
                        }
                    }
                }
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
        public class {propertyName}List : LimitableList<{propertyType}>
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
            sb.AppendLine($@"{indent}[Display(Name = ""{(schemaField.Name ?? propertyName).ToProperCase()}"")]");
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
                else if (options.MaxSelect != null)
                    sb.AppendLine($@"{indent}[JsonConverter(typeof(EnumListConverter<{propertyName}List, {propertyName}Enum>))]");
                else //List
                    sb.AppendLine($@"{indent}[JsonConverter(typeof(EnumListConverter<LimitableList<{propertyName}Enum>, {propertyName}Enum>))]");
            }
            else if (schemaField.Type == "json")
            {
            }
            else if (schemaField.Type == "file")
            {
            }
            else if (schemaField.Type == "relation")
            {
                var options = JsonSerializer.Deserialize<PocketBaseFieldOptionsRelation>(JsonSerializer.Serialize(schemaField.Options)) ?? new PocketBaseFieldOptionsRelation();
                var colInfo = _CollectionList.First(c => c.CollectionId == options.CollectionId);
                if (options.IsSinglSelect)
                    sb.AppendLine($@"{indent}[JsonConverter(typeof(RelationConverter<{colInfo.ItemsClassName}>))]");
                else if (options.MaxSelect != null)
                    sb.AppendLine($@"{indent}[JsonConverter(typeof(RelationListConverter<{propertyName}List, {colInfo.ItemsClassName}>))]");
                else //List
                    sb.AppendLine($@"{indent}[JsonConverter(typeof(RelationListConverter<LimitableList<{colInfo.ItemsClassName}>, {colInfo.ItemsClassName}>))]");
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
            sb.Append($@"{indent}   ");
            if (initialVal.StartsWith("new LimitableList<") || initialVal.EndsWith("List()"))
                sb.Append($@"private ");
            sb.AppendLine($@"set => Set(value, ref _{propertyName});");
            sb.AppendLine($@"{indent}}}");

            return sb;
        }

        private void ProcessSchemaItemFilterCollection(CollectionInfo colInfo)
        {
            var itemFilterCode = new GeneratedCode
            {
                Path = Path.Combine(OutputPathModels, $"{colInfo.ItemsClassName}.Filters.cs")
            };
            List<string> relatedItems = new List<string>();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($@"{CodeHeader}
using PocketBaseClient.Orm.Filters;
using System.Net.Mail;

namespace {GeneratedNamespaceModels}
{{
    public partial class {colInfo.ItemsClassName} 
    {{
        public class Filters : ItemBaseFilters
        {{
");
            foreach (var schemaField in colInfo.CollectionModel.Schema!.Where(s => s?.Name != null))
                GetPropertyFilterForSchemaField(schemaField, "            ", schemaField.Name!.ToPascalCase(), sb);
            sb.AppendLine($@"
        }}
    }}
}}");
            itemFilterCode.Content = sb.ToString();
            _GeneratedCodeList.Add(itemFilterCode);
        }

        private void GetPropertyFilterForSchemaField(SchemaFieldModel schemaField, string indent, string propertyName, StringBuilder sb)
        {
            if (schemaField.Type == "text")
            {
                sb.AppendLine($@"{indent}public FilterQuery {propertyName}(OperatorText op, string value) => FilterQuery.Create(""{schemaField.Name}"", op, value);");
            }
            else if (schemaField.Type == "number")
            {
                sb.AppendLine($@"{indent}public FilterQuery {propertyName}(OperatorNumeric op, int value) => FilterQuery.Create(""{schemaField.Name}"", op, value);");
            }
            else if (schemaField.Type == "bool")
            {
                sb.AppendLine($@"{indent}public FilterQuery {propertyName}(bool value) => FilterQuery.Create(""{schemaField.Name}"", value);");
            }
            else if (schemaField.Type == "email")
            {
                sb.AppendLine($@"{indent}public FilterQuery {propertyName}(OperatorText op, MailAddress value) => FilterQuery.Create(""{schemaField.Name}"", op, value);");
                sb.AppendLine($@"{indent}public FilterQuery {propertyName}(OperatorText op, string value) => FilterQuery.Create(""{schemaField.Name}"", op, value);");
            }
            else if (schemaField.Type == "url")
            {
                sb.AppendLine($@"{indent}public FilterQuery {propertyName}(OperatorText op, Uri value) => FilterQuery.Create(""{schemaField.Name}"", op, value);");
                sb.AppendLine($@"{indent}public FilterQuery {propertyName}(OperatorText op, string value) => FilterQuery.Create(""{schemaField.Name}"", op, value);");
            }
            else if (schemaField.Type == "date")
            {
                sb.AppendLine($@"{indent}public FilterQuery {propertyName}(OperatorNumeric op, DateTime value) => FilterQuery.Create(""{schemaField.Name}"", op, value);");
            }
            else if (schemaField.Type == "select")
            {
                //var options = JsonSerializer.Deserialize<PocketBaseFieldOptionsSelect>(JsonSerializer.Serialize(schemaField.Options)) ?? new PocketBaseFieldOptionsSelect();
                //if (options.IsSinglSelect)
                //    sb.AppendLine($@"{indent}[JsonConverter(typeof(EnumConverter<{propertyName}Enum>))]");
                //else if (options.MaxSelect != null)
                //    sb.AppendLine($@"{indent}[JsonConverter(typeof(EnumListConverter<{propertyName}List, {propertyName}Enum>))]");
                //else //List
                //    sb.AppendLine($@"{indent}[JsonConverter(typeof(EnumListConverter<List<{propertyName}Enum>, {propertyName}Enum>))]");
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
                //else if (options.MaxSelect != null)
                //    sb.AppendLine($@"{indent}[JsonConverter(typeof(RelationListConverter<{propertyName}List, {colInfo.ItemsClassName}>))]");
                //else //List
                //    sb.AppendLine($@"{indent}[JsonConverter(typeof(RelationListConverter<List<{colInfo.ItemsClassName}>, {colInfo.ItemsClassName}>))]");
            }
        }


        //public static void GenerateCode(string jsonPath, string outputPath, string generatedNamespace)
        //{
        //    ConsoleExtensions.WriteProcess($"Generating code from schema file {jsonPath}");
        //    PocketBaseSchema schema;
        //    try
        //    {
        //        schema = PocketBaseSchema.LoadFromFile(jsonPath) ?? throw new Exception("Empty schema");

        //    }
        //    catch (Exception ex)
        //    {
        //        ConsoleExtensions.WriteError($"Failed to read schema from file {jsonPath}");
        //        ConsoleExtensions.WriteError($"Exception: {ex}");
        //        return;
        //    }
        //    var codeGenerator = new CodeGenerator(schema, outputPath, generatedNamespace);
        //    codeGenerator.GenerateCode();
        //    ConsoleExtensions.WriteDone();
        //}

        public static void GenerateCode(PocketBaseSchema schema, string projectFolder)
        {
            ConsoleHelper.WriteProcess($"Generating code for {schema.PocketBaseApplication.Name} in {projectFolder}");
            var codeGenerator = new CodeGenerator(schema, projectFolder);
            codeGenerator.GenerateCode();
            ConsoleHelper.WriteDone();

            new Process
            {
                StartInfo = new ProcessStartInfo(codeGenerator.OutputSummaryFileName)
                {
                    UseShellExecute = true,
                }
            }.Start();
        }
    }
}
