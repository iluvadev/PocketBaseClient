using pocketbase_csharp_sdk.Models.Collection;
using PocketBaseClient.CodeGenerator.Helpers;
using System.Diagnostics;
using System.Text;

namespace PocketBaseClient.CodeGenerator.Generation
{
    internal class Generator
    {
        private List<CollectionInfo> Collections { get; } = new List<CollectionInfo>();
        private List<GeneratedCodeFile> GeneratedFiles { get; } = new List<GeneratedCodeFile>();

        public Generator()
        {
        }

        private void ResetGenerationData()
        {
            Collections.Clear();
            GeneratedFiles.Clear();
        }

        public void GenerateCode(Settings settings)
        {
            ConsoleHelper.WriteProcess($"Generating code for {settings.ApplicationName} in {settings.BasePath}");
            GenerateCodeInternal(settings);
            ConsoleHelper.WriteDone();

            new Process
            {
                StartInfo = new ProcessStartInfo(Path.Combine(settings.BasePath, settings.SummaryFileName))
                {
                    UseShellExecute = true,
                }
            }.Start();
        }
        private void GenerateCodeInternal(Settings settings)
        {
            StringExtensions.SingularizeAndPluralize = settings.PocketBaseSchema.SingularizeAndPluralize;
            
            ResetGenerationData();
            foreach (var collectionModel in settings.PocketBaseSchema.Collections)
                Collections.Add(new CollectionInfo(collectionModel, () => Collections));

            foreach (var collection in Collections)
                GeneratedFiles.AddRange(collection.GenerateCode(settings));

            GeneratedFiles.Add(GetCodeFileForApplication(settings));
            GeneratedFiles.Add(GetCodeFileForService(settings));
            GeneratedFiles.Add(GetCodeFileForProject(settings));

            GeneratedFiles.Add(GetCodeFileForSummary(settings));

            foreach (var generatedFile in GeneratedFiles)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(generatedFile.FileName) ?? settings.BasePath);
                File.WriteAllText(generatedFile.FileName, generatedFile.Content);
            }

            settings.PocketBaseSchema.SaveToFile(Path.Combine(settings.BasePath, Settings.SchemaFileName));
        }

        private GeneratedCodeFile GetCodeFileForApplication(Settings settings)
        {
            string appClassName = (settings.ApplicationName ?? "MyPocketBase").ToPascalCase() + "Application";
            string serviceClassName = (settings.ApplicationName ?? "MyPocketBase").ToPascalCase() + "DataService";
            string fileName = Path.Combine(settings.BasePath, appClassName + ".cs");

            string content = $@"{settings.CodeHeader}
using PocketBaseClient;
using {settings.NamespaceServices};

namespace {settings.BaseNamespace}
{{
    public partial class {appClassName} : PocketBaseClientApplication
    {{
        private {serviceClassName}? _Data = null;
        /// <summary> Access to Data for Application {settings.ApplicationName} </summary>
        public {serviceClassName} Data => _Data ??= new {serviceClassName}(this);

        #region Constructors
        public {appClassName}() : this(""{settings.ApplicationUrl}"") {{ }}
        public {appClassName}(string url, string appName = ""{settings.ApplicationName}"") : base(url, appName) {{ }}
        #endregion Constructors
    }}
}}
";
            return new GeneratedCodeFile(fileName, content);
        }

        private GeneratedCodeFile GetCodeFileForService(Settings settings)
        {
            string serviceClassName = (settings.ApplicationName ?? "MyPocketBase").ToPascalCase() + "DataService";
            string fileName = Path.Combine(settings.PathServices, serviceClassName + ".cs");

            StringBuilder sb = new StringBuilder();
            sb.Append($@"{settings.CodeHeader}
using PocketBaseClient;
using PocketBaseClient.Services;
using {settings.NamespaceModels};

namespace {settings.NamespaceServices}
{{
    public partial class {serviceClassName} : DataServiceBase
    {{
        #region Collections");

            foreach (var colInfo in Collections)
                sb.Append($@"
        /// <summary> Collection '{colInfo.CollectionModel.Name}' in PocketBase </summary>
        public {colInfo.ClassName} {colInfo.NameInDataService} {{ get; }}");
            sb.Append($@"

        /// <inheritdoc />
        protected override void RegisterCollections()
        {{");
            foreach (var colInfo in Collections)
                sb.Append($@"
            RegisterCollection(typeof({settings.NamespaceModels}.{colInfo.ItemInfo.ClassName}), {colInfo.NameInDataService});");
            sb.Append($@"
        }}
        #endregion Collections

        #region Constructor
        public {serviceClassName}(PocketBaseClientApplication app) : base(app)
        {{
            // Collections");
            foreach (var colInfo in Collections)
                sb.Append($@"
            {colInfo.NameInDataService} = new {colInfo.ClassName}(this);");
            sb.Append($@"

            RegisterCollections();
        }}
        #endregion Constructor
    }}
}}
");
            return new GeneratedCodeFile(fileName, sb.ToString());
        }

        private GeneratedCodeFile GetCodeFileForProject(Settings settings)
        {
            var fileName = Path.Combine(settings.BasePath, settings.ProjectFileName);
            var content = @"
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
";
            return new GeneratedCodeFile(fileName, content);
        }

        private GeneratedCodeFile GetCodeFileForSummary(Settings settings)
        {
            var fileName = Path.Combine(settings.BasePath, settings.SummaryFileName);

            var appName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            var appVer = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(settings.CodeHeader);
            sb.AppendLine(Program.Banner);
            sb.AppendLine($@"
Code generated with:
    PocketBaseClient CodeGenerator: {appName} v.{appVer}
    At: {DateTime.UtcNow.ToString("O")}

PocketBase Application: 
    Name: {settings.ApplicationName}
    Url: {settings.ApplicationUrl}
    Schema Date: {settings.PocketBaseSchema.SchemaDate.ToString("O")}

Files Generated:");
            foreach (var strFile in GeneratedFiles.Select(c => c.FileName).OrderBy(c => c))
                sb.AppendLine($"    {strFile}");

            return new GeneratedCodeFile(fileName, sb.ToString());
        }
    }
}
