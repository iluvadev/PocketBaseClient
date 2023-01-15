// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using PocketBaseClient.CodeGenerator.Models;

namespace PocketBaseClient.CodeGenerator.Generation
{
    /// <summary>
    /// Generation code Settings
    /// </summary>
    internal class Settings
    {
        /// <summary>
        /// Name of the file with the Schema information
        /// </summary>
        public static string SchemaFileName = "pbcodegen.json";

        /// <summary>
        /// Name of the file with the generation code summary report
        /// </summary>
        public string SummaryFileName => "CodeGenerationSummary.txt";

        /// <summary>
        /// Name of the file for the generated c# Project
        /// </summary>
        public string ProjectFileName => PocketBaseSchema.ProjectName + ".csproj";

        /// <summary>
        /// Header to be included in each generated code file
        /// </summary>
        public string CodeHeader { get; }

        /// <summary>
        /// Starting path to the generated code
        /// </summary>
        public string BasePath { get; }

        /// <summary>
        /// Path for the generated code for Models
        /// </summary>
        public string PathModels => Path.Combine(BasePath, "Models");

        /// <summary>
        /// Path for the generated code for Services
        /// </summary>
        public string PathServices => Path.Combine(BasePath, "Services");

        /// <summary>
        /// Starting Namespace for the generated code
        /// </summary>
        public string BaseNamespace => PocketBaseSchema.Namespace;

        /// <summary>
        /// Namespace for the generated code for Models
        /// </summary>
        public string NamespaceModels => BaseNamespace + ".Models";

        /// <summary>
        /// Namespace for the generated code for Services
        /// </summary>
        public string NamespaceServices => BaseNamespace + ".Services";

        /// <summary>
        /// The PocketBase application name
        /// </summary>
        public string ApplicationName => PocketBaseSchema.PocketBaseApplication.Name!;

        /// <summary>
        /// The PocketBase application url
        /// </summary>
        public string ApplicationUrl => PocketBaseSchema.PocketBaseApplication.Url!;

        /// <summary>
        /// The Schema from PocketBase
        /// </summary>
        public PocketBaseSchema PocketBaseSchema { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="pocketBaseSchema"></param>
        /// <param name="basePath"></param>
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

        /// <summary>
        /// Load the Settings from folder
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Settings LoadFromFolder(string path)
        {
            var schema = PocketBaseSchema.LoadFromFile(Path.Combine(path, SchemaFileName));
            return new Settings(schema!, path);
        }

    }
}
