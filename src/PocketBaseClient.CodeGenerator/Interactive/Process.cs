// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using PocketBaseClient.CodeGenerator.Generation;
using PocketBaseClient.CodeGenerator.Helpers;
using PocketBaseClient.CodeGenerator.Models;
using Sharprompt;

namespace PocketBaseClient.CodeGenerator.Interactive
{
    /// <summary>
    /// Class with all Interactive process management
    /// </summary>
    internal static class Process
    {
        /// <summary>
        /// Starts the interactive process of code generation
        /// </summary>
        public static void Start()
        {
            //Console.OutputEncoding = Encoding.UTF8;
            ConsoleHelper.WriteEmphasis(Program.Banner);
            Console.WriteLine(Program.Welcome);

            var mainAction = Prompt.Select<MainActions>("Select action to do");
            switch (mainAction)
            {
                case MainActions.GenerateNew: GenerateNew(); break;
                case MainActions.Regenerate: Regenerate(); break;
            }
        }

        /// <summary>
        /// Generate code for first time
        /// </summary>
        private static void GenerateNew()
        {
            ConsoleHelper.WriteStep(1, "Setting the PocketBase Application");
            // Get the PocketBase Url
            string pocketBaseUrl = Prompt.Input<string>("Enter the PocketBase server Url of your application",
                                                        validators: PromptValidators.PocketBaseUrl())!;
            var pocketBaseUri = new Uri(pocketBaseUrl);

            // Download the PocketBase Schema, with Application name
            var schema = DownloadPocketBaseSchema(pocketBaseUri);


            ConsoleHelper.WriteStep(2, "Defining the code to generate");

            // Ask for the Project name
            AskProjectName(schema);

            // Ask for Directory Base where generate code
            string projectFolder = AskProjectFolder();
            
            // Ask for the Namespace
            AskNamespace(schema);
            
            // Ask for whether to enable singularize and pluralize feature
            AskSingularizeAndPluralize(schema);

            ConsoleHelper.WriteStep(3, "Code generation");
            // Generate code
            var settings = new Settings(schema, projectFolder);
            var codeGenerator = new Generator();
            codeGenerator.GenerateCode(settings);

            ConsoleHelper.WriteDone();
        }
        

        /// <summary>
        /// Regenerate existant code
        /// </summary>
        private static void Regenerate()
        {
            ConsoleHelper.WriteStep(1, "Getting the generated code");
            // Ask for generated Project folder
            string projectFolder = Prompt.Input<string>("Enter the Project folder with generated code",
                                                        validators: PromptValidators.GeneratedFolderProject())!;

            // Load Schema
            Settings settings;
            ConsoleHelper.WriteProcess($"Loading PocketBase Application information from file {Settings.SchemaFileName}");
            try { settings = Settings.LoadFromFolder(projectFolder); }
            catch (Exception ex) { ConsoleHelper.WriteFailed(ex.Message); return; }
            ConsoleHelper.WriteDone();

            ConsoleHelper.WriteStep(2, "Updating the PocketBase Application", settings.ApplicationName);
            // Ask for update Schema with server information
            ConsoleHelper.WriteCurrentValue("The PocketBase server is", settings.ApplicationUrl);
            ConsoleHelper.WriteCurrentValue("The Application schema was last updated on", settings.PocketBaseSchema.SchemaDate.ToString("O"));
            if (Prompt.Confirm($"Do you want to update the schema from server?", true))
                UpdatePocketBaseSchema(settings.PocketBaseSchema);

            ConsoleHelper.WriteStep(3, "Code generation");
            // Generate code
            ConsoleHelper.WriteCurrentValue("Folder to overwrite:", projectFolder);
            if (Prompt.Confirm($"Do you want to overwrite all generated code?", true))
            {
                var codeGenerator = new Generator();
                codeGenerator.GenerateCode(settings);
            }

            ConsoleHelper.WriteDone();
        }

        /// <summary>
        /// Ask for the project name to be generated
        /// </summary>
        /// <param name="schema">The PocketBase schema</param>
        private static void AskProjectName(PocketBaseSchema schema)
        {
            bool isOk = false;

            while (!isOk)
            {
                ConsoleHelper.WriteCurrentValue("The name for your Project will be:", schema.ProjectName);
                isOk = !Prompt.Confirm("Do you want to change this project name?", false);
                if (!isOk)
                    schema.ProjectName = Prompt.Input<string>("Enter the Project Name",
                        validators: PromptValidators.NameForProjectOrNamespace()).Trim();
            }
        }

        /// <summary>
        /// Ask for the project folder where generate the code
        /// </summary>
        /// <returns></returns>
        private static string AskProjectFolder()
        {
            string folder = "";
            bool isOk = false;

            while (!isOk)
            {
                folder = Prompt.Input<string>("Enter the Directory where create the Project with code",
                    validators: PromptValidators.ProjectFolder());
                var dirInfo = new DirectoryInfo(folder);
                isOk = !dirInfo.Exists || 
                       Prompt.Confirm("The directory already exists, do you want to overwrite this?", false);
            }

            return folder;
        }

        /// <summary>
        /// Ask for the namespace to use in the code to be generated
        /// </summary>
        /// <param name="schema">The PocketBase schema</param>
        private static void AskNamespace(PocketBaseSchema schema)
        {
            bool isOk = false;

            while (!isOk)
            {
                ConsoleHelper.WriteCurrentValue("The namespace for generated code will be:", schema.Namespace);
                isOk = !Prompt.Confirm("Do you want to change this namespace?", false);
                if (!isOk)
                    schema.Namespace = Prompt.Input<string>("Enter the correct Namespace",
                        validators: PromptValidators.NameForProjectOrNamespace()).ToNamespace();
            }
        }

        /// <summary>
        /// Ask for the option of Singularize and Plurarize generated class names
        /// </summary>
        /// <param name="schema">The PocketBase schema</param>
        private static void AskSingularizeAndPluralize(PocketBaseSchema schema)
        {
            ConsoleHelper.WriteEmphasis(@"
  This feature may be useful if your collections' name are plural.
  i.e. If you have a collection named ""posts"", 
       name of class and data service of collection will use pluralized name ""Posts"" and
       name of class of item will use singularized name ""Post"".
       Disabling using this feature, both of it will use ""Posts"".
");
            schema.SingularizeAndPluralize =
                Prompt.Confirm("Do you want to enable singularize and pluralize feature?", true);
        }

        /// <summary>
        /// Updates the PocketBase schema downloading from PocketBase server
        /// </summary>
        /// <param name="schema">The PocketBase schema</param>
        private static void UpdatePocketBaseSchema(PocketBaseSchema schema)
        {
            var pocketBaseUri = new Uri(schema.PocketBaseApplication.Url!);
            var downloadedSchema = DownloadPocketBaseSchema(pocketBaseUri);

            schema.Collections = downloadedSchema.Collections;
        }

        /// <summary>
        /// Downloads the PocketBase schema from server
        /// </summary>
        /// <param name="pocketBaseUri">The url of the server</param>
        /// <returns></returns>
        private static PocketBaseSchema DownloadPocketBaseSchema(Uri pocketBaseUri)
        {
            PocketBaseCredentials? pocketBaseCredentials;
            PocketBaseSchema? schema = null;

            while (schema == null)
            {
                pocketBaseCredentials = Prompt.Bind<PocketBaseCredentials>();
                try
                {
                    schema = Task.Run(async () => await SchemaDownloader.DownloadSchemaAsync(pocketBaseUri,
                            pocketBaseCredentials.Email ?? "", pocketBaseCredentials.Password ?? "")).GetAwaiter()
                        .GetResult();
                }
                catch (Exception ex)
                {
                    ConsoleHelper.WriteError("There was an error downloading the PocketBase schema");
                    ConsoleHelper.WriteError(ex.ToString());
                }
            }
            return schema;
        }
    }
}
