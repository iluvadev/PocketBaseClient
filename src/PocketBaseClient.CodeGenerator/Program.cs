using pocketbase_csharp_sdk;
using pocketbase_csharp_sdk.Models;
using pocketbase_csharp_sdk.Models.Collection;
using PocketBaseClient;
using PocketBaseClient.CodeGenerator.Models;
using System.CommandLine;
using System.Text.Json;

namespace PocketBaseClient.CodeGenerator
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            var rootCommand = new RootCommand("PocketBaseClient Code Generator: An application to generate client side code of your PocketBase application");

            rootCommand.AddCommand(GetSaveschemaCommand());

            // Commands:
            //  * saveschema --url --email --pwd --file
            //  * generate --schema --format --folder

            return await rootCommand.InvokeAsync(args);
        }

        private static Command GetSaveschemaCommand()
        {
            var urlOption = new Option<Uri?>(
                name: "--url",
                description: "The url of your PocketBase application",
                getDefaultValue: () => new Uri("http://127.0.0.1:8090"));

            var emailOption = new Option<string>(
                name: "--email",
                description: "email to login with Admin rights in your PocketBase application");

            var pwdOption = new Option<string>(
                name: "--pwd",
                description: "Password to login with Admin rights in your PocketBase application");

            var fileOption = new Option<FileInfo?>(
                name: "--file",
                description: "The file where download the schema information of your application",
                parseArgument: result =>
                {
                    if (result.Tokens.Count == 0)
                    {
                        result.ErrorMessage = "File does not set";
                        return null;
                    }
                    string? filePath = result.Tokens.Single().Value;
                    if (!File.Exists(filePath))
                    {
                        result.ErrorMessage = "File does not exist";
                        return null;
                    }
                    return new FileInfo(filePath);
                });

            var commandDownloadSchema = new Command("saveschema", "Download the schema of your PocketBase application")
            {
                urlOption,
                emailOption,
                pwdOption,
                fileOption
            };

            commandDownloadSchema.SetHandler(async (url, email, pwd, file) => await DownloadSchema(url!, email!, pwd!, file!),
                                             urlOption, emailOption, pwdOption, fileOption);
            return commandDownloadSchema;
        }

        static async Task DownloadSchema(Uri url, string email, string pwd, FileInfo file)
        {
            GenerateCode(file.FullName, @"C:\Dev\iluvadev\projects\PocketBaseClient-csharp\src\PocketBaseClient.SampleApp", "PocketBaseClient.SampleApp");
            return;

            var app = new PocketBaseClientApplication(url.ToString());

            Console.WriteLine($"Connecting to {url} with Admin {email}...");
            var admin = await app.Auth.Admin.AuthenticateWithPassword(email, pwd);
            if (string.IsNullOrEmpty(admin?.Token))
            {
                Console.WriteLine($">> Failed to connect to {url} with Admin {email} credentials");
                return;
            }

            Console.WriteLine($"Connected!");
            var schema = new PocketBaseSchema();

            Console.WriteLine($"Getting PocketBase Application Settings...");
            schema.SetSettingsAsync(await app.Sdk.Settings.GetAllAsync());

            Console.WriteLine($"Getting PocketBase Application Collections...");
            int? totalItems = null;
            while (totalItems == null || schema.Collections.Count < totalItems)
            {
                var collections = await app.Sdk.HttpGetListAsync<CollectionModel>("/api/collections");
                //var collections = await app.Sdk.Collections.ListAsync();
                totalItems = collections.TotalItems;
                schema.Collections.AddRange(collections.Items ?? Enumerable.Empty<CollectionModel>());
            }

            Console.WriteLine($"Saving to file {file.FullName}...");
            schema.SaveToFile(file.FullName);

            Console.WriteLine($"Done!");
        }


        private static void GenerateCode(string jsonPath, string outputPath, string generatedNamespace)
        {
            Console.WriteLine($"Generating code from schema file {jsonPath}...");
            PocketBaseSchema schema;
            try
            {
                schema = PocketBaseSchema.LoadFromFile(jsonPath) ?? throw new Exception("Empty schema");

            }
            catch (Exception ex)
            {
                Console.WriteLine($">> Failed to read schema from file {jsonPath}");
                Console.WriteLine($">> Exception: {ex}");
                return;
            }
            var codeGenerator = new CodeGenerator(schema, outputPath, generatedNamespace);
            codeGenerator.GenerateCode();
        }

        private static async void GetTestData(PocketBaseClientApplication app, string path)
        {
            Console.WriteLine($"Getting text data...");
            var pagedUsers = await app.Sdk.SendAsync<PagedCollectionModel<object>>($"/api/collections/users/records", HttpMethod.Get);
            var pagedTestTypes = await app.Sdk.SendAsync<PagedCollectionModel<object>>($"/api/collections/test_for_types/records", HttpMethod.Get);
            var pagedTestRel = await app.Sdk.SendAsync<PagedCollectionModel<object>>($"/api/collections/test_for_related/records", HttpMethod.Get);
            var all = new { Users = pagedUsers?.Items, TestTypes = pagedTestTypes?.Items, TestRel = pagedTestRel?.Items };
            var jsonString = JsonSerializer.Serialize(all);
            File.WriteAllText(path, jsonString);
            Console.WriteLine($"Done!");
        }

        private static async Task<PagedCollectionModel<object>> ListAsync(PocketBaseClientApplication app, string collection,
            int? page = null, int? perPage = null, string? sort = null, string? filter = null, string? expand = null, IDictionary<string, string>? headers = null)
        {
            var query = new Dictionary<string, object?>()
            {
                { "filter", filter },
                { "page", page },
                { "perPage", perPage },
                { "sort", sort },
                { "expand", expand },
            };
            var url = $"/api/collections/{collection}/records";
            var pagedCollection = await app.Sdk.SendAsync<PagedCollectionModel<object>>(
                url,
                HttpMethod.Get,
                headers: headers,
                query: query);
            if (pagedCollection is null) throw new ClientException(url);

            return pagedCollection;
        }


        private static Command InitializeGenerate()
        {
            var urlOption = new Option<Uri?>(
                name: "--url",
                description: "The url of your PocketBase application",
                getDefaultValue: () => new Uri("http://127.0.0.1:8090"));

            var emailOption = new Option<string>(
                name: "--email",
                description: "email to login with Admin rights in your PocketBase application");

            var pwdOption = new Option<string>(
                name: "--pwd",
                description: "Password to login with Admin rights in your PocketBase application");

            var fileOption = new Option<FileInfo?>(
                name: "--file",
                description: "The file where download the schema information of your application");

            var commandDownloadSchema = new Command("saveschema", "Download the schema of your PocketBase application")
            {
                urlOption,
                emailOption,
                pwdOption,
                fileOption
            };

            commandDownloadSchema.SetHandler(async (url, email, pwd, file) => await DownloadSchema(url!, email!, pwd!, file!),
                                             urlOption, emailOption, pwdOption, fileOption);
            return commandDownloadSchema;
        }

    }
}