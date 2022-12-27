using PocketBaseClient.CodeGenerator.Models;

namespace PocketBaseClient.CodeGenerator.Generation
{
    internal class Settings
    {
        public static string SchemaFileName = "pbcodegen.json";
        public string SummaryFileName => "CodeGenerationSummary.txt";
        public string ProjectFileName => PocketBaseSchema.ProjectName + ".csproj";


        public string CodeHeader { get; }

        public string BasePath { get; }
        public string PathModels => Path.Combine(BasePath, "Models");
        public string PathServices => Path.Combine(BasePath, "Services");

        public string BaseNamespace => PocketBaseSchema.Namespace;
        public string NamespaceModels => BaseNamespace + ".Models";
        public string NamespaceServices => BaseNamespace + ".Services";

        public string ApplicationName => PocketBaseSchema.PocketBaseApplication.Name!;
        public string ApplicationUrl => PocketBaseSchema.PocketBaseApplication.Url!;

        public PocketBaseSchema PocketBaseSchema { get; }


        public Settings(PocketBaseSchema pocketBaseSchema, string basePath)
        {
            PocketBaseSchema = pocketBaseSchema;
            BasePath = basePath;

            CodeHeader = $@"
// This file was generated automatically for the PocketBase Application {ApplicationName} ({ApplicationUrl})
//    See CodeGenerationSummary.txt for more details
//
// PocketBaseClient-csharp project: https://github.com/iluvadev/PocketBaseClient-csharp
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase
"; 
        }

        public static Settings LoadFromFolder(string path)
        {
            var schema = PocketBaseSchema.LoadFromFile(Path.Combine(path, SchemaFileName));
            return new Settings(schema!, path);
        }

    }
}
