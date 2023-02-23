// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using PocketBaseClient.CodeGenerator.Helpers;
using System.Diagnostics;
using System.Text;

namespace PocketBaseClient.CodeGenerator.Generation
{
    /// <summary>
    /// Class with all generation code process
    /// </summary>
    internal class Generator
    {
        /// <summary>
        /// List of the Collections
        /// </summary>
        private List<CollectionInfo> Collections { get; } = new List<CollectionInfo>();

        /// <summary>
        /// List of Generated files with its content
        /// </summary>
        private List<GeneratedCodeFile> GeneratedFiles { get; } = new List<GeneratedCodeFile>();

        /// <summary>
        /// Ctor
        /// </summary>
        public Generator()
        {
        }

        /// <summary>
        /// Clears Generation code information
        /// </summary>
        private void ResetGenerationData()
        {
            Collections.Clear();
            GeneratedFiles.Clear();
        }

        /// <summary>
        /// Starts the Generation code process
        /// </summary>
        /// <param name="settings">Generation code settings</param>
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

        /// <summary>
        /// Generates the code for the PocketBase schema
        /// </summary>
        /// <param name="settings">Generation code settings</param>
        private void GenerateCodeInternal(Settings settings)
        {
            StringExtensions.SingularizeAndPluralize = settings.PocketBaseSchema.SingularizeAndPluralize;

            ResetGenerationData();
            foreach (var collectionModel in settings.PocketBaseSchema.Collections)
                Collections.Add(new CollectionInfo(collectionModel, () => Collections));

            foreach (var collection in Collections)
                GeneratedFiles.AddRange(collection.GenerateCode(settings));

            GeneratedFiles.Add(GetCodeFileForApplication(settings));
            GeneratedFiles.Add(GetCodeFileForDataService(settings));
            GeneratedFiles.Add(GetCodeFileForAuthService(settings));
            GeneratedFiles.Add(GetCodeFileForProject(settings));

            GeneratedFiles.Add(GetCodeFileForSummary(settings));

            foreach (var generatedFile in GeneratedFiles)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(generatedFile.FileName) ?? settings.BasePath);
                File.WriteAllText(generatedFile.FileName, generatedFile.Content);
            }

            settings.PocketBaseSchema.SaveToFile(Path.Combine(settings.BasePath, Settings.SchemaFileName));
        }

        /// <summary>
        /// Generates the code for the class that represents the PocketBase application
        /// </summary>
        /// <param name="settings">Generation code settings</param>
        /// <returns></returns>
        private GeneratedCodeFile GetCodeFileForApplication(Settings settings)
        {
            string appClassName = (settings.ApplicationName ?? "MyPocketBase").ToPascalCase() + "Application";
            string serviceDataClassName = (settings.ApplicationName ?? "MyPocketBase").ToPascalCase() + "DataService";
            string serviceAuthClassName = (settings.ApplicationName ?? "MyPocketBase").ToPascalCase() + "AuthService";
            string fileName = Path.Combine(settings.BasePath, appClassName + ".cs");

            string content = $@"{settings.CodeHeader}
using PocketBaseClient;
using {settings.NamespaceServices};

namespace {settings.BaseNamespace}
{{
    public partial class {appClassName} : PocketBaseClientApplication
    {{
        private {serviceDataClassName}? _Data = null;
        /// <summary> Access to Data for Application {settings.ApplicationName} </summary>
        public {serviceDataClassName} Data => _Data ??= new {serviceDataClassName}(this);

        private {serviceAuthClassName}? _Auth = null;
        /// <summary> Access to Auth for Application {settings.ApplicationName} </summary>
        public new {serviceAuthClassName} Auth => _Auth ??= new {serviceAuthClassName}(this);

        #region Constructors
        public {appClassName}() : this(""{settings.ApplicationUrl}"") {{ }}
        public {appClassName}(string url, string appName = ""{settings.ApplicationName}"") : base(url, appName) {{ }}
        #endregion Constructors
    }}
}}
";
            return new GeneratedCodeFile(fileName, content);
        }

        /// <summary>
        /// Generates the code for the class with Service Data
        /// </summary>
        /// <param name="settings">Generation code settings</param>
        /// <returns></returns>
        private GeneratedCodeFile GetCodeFileForDataService(Settings settings)
        {
            string serviceDataClassName = (settings.ApplicationName ?? "MyPocketBase").ToPascalCase() + "DataService";
            string fileName = Path.Combine(settings.PathServices, serviceDataClassName + ".cs");

            StringBuilder sb = new StringBuilder();
            sb.Append($@"{settings.CodeHeader}
using PocketBaseClient;
using PocketBaseClient.Services;
using {settings.NamespaceModels};

namespace {settings.NamespaceServices}
{{
    public partial class {serviceDataClassName} : DataServiceBase
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
        public {serviceDataClassName}(PocketBaseClientApplication app) : base(app)
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

        /// <summary>
        /// Generates the code for the class with Service Auth
        /// </summary>
        /// <param name="settings">Generation code settings</param>
        /// <returns></returns>
        private GeneratedCodeFile GetCodeFileForAuthService(Settings settings)
        {
            string serviceAuthClassName = (settings.ApplicationName ?? "MyPocketBase").ToPascalCase() + "AuthService";
            string serviceDataClassName = (settings.ApplicationName ?? "MyPocketBase").ToPascalCase() + "DataService";
            string fileName = Path.Combine(settings.PathServices, serviceAuthClassName + ".cs");

            StringBuilder sb = new StringBuilder();
            sb.Append($@"{settings.CodeHeader}
using PocketBaseClient;
using PocketBaseClient.Services;
using PocketBaseClient.Orm;
using {settings.NamespaceModels};

namespace {settings.NamespaceServices}
{{
    public partial class {serviceAuthClassName} : AuthServiceBase
    {{
        #region Auth Collections");
            foreach (var colInfo in Collections.Where(c => c.IsAuth))
                sb.Append($@"
        /// <summary> Auth for Collection '{colInfo.CollectionModel.Name}' in PocketBase </summary>
        public AuthCollectionService<{colInfo.ItemsClassName}> {colInfo.ItemsClassName} => ({serviceDataClassName}.GetCollection<{colInfo.ItemsClassName}>() as CollectionAuthBase<{colInfo.ItemsClassName}>)!.Auth;");
            sb.Append($@"
        #endregion Auth Collections

        #region Constructor
        public {serviceAuthClassName}(PocketBaseClientApplication app) : base(app) {{ }}
        #endregion Constructor
    }}
}}
");
            return new GeneratedCodeFile(fileName, sb.ToString());
        }

        /// <summary>
        /// Generates the c# Project 
        /// </summary>
        /// <param name="settings">Generation code settings</param>
        /// <returns></returns>
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

        /// <summary>
        /// Generates the file with the generation code Summary report 
        /// </summary>
        /// <param name="settings">Generation code settings</param>
        /// <returns></returns>
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
