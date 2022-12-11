// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using pocketbase_csharp_sdk;
using pocketbase_csharp_sdk.Models;
using pocketbase_csharp_sdk.Models.Collection;
using PocketBaseClient.CodeGenerator.Models;
using System.CommandLine;
using System.Text.Json;

namespace PocketBaseClient.CodeGenerator
{
    class Program
    {
        public static string Banner = @"
  _____           _        _   ____                  _____ _ _            _   
 |  __ \         | |      | | |  _ \                / ____| (_)          | |  
 | |__) |__   ___| | _____| |_| |_) | __ _ ___  ___| |    | |_  ___ _ __ | |_ 
 |  ___/ _ \ / __| |/ / _ \ __|  _ < / _` / __|/ _ \ |    | | |/ _ \ '_ \| __|
 | |  | (_) | (__|   <  __/ |_| |_) | (_| \__ \  __/ |____| | |  __/ | | | |_ 
 |_|   \___/ \___|_|\_\___|\__|____/ \__,_|___/\___|\_____|_|_|\___|_| |_|\__|
   _____          _       _____                           _                   
  / ____|        | |     / ____|                         | |                  
 | |     ___   __| | ___| |  __  ___ _ __   ___ _ __ __ _| |_ ___  _ __       
 | |    / _ \ / _` |/ _ \ | |_ |/ _ \ '_ \ / _ \ '__/ _` | __/ _ \| '__|      
 | |___| (_) | (_| |  __/ |__| |  __/ | | |  __/ | | (_| | || (_) | |         
  \_____\___/ \__,_|\___|\_____|\___|_| |_|\___|_|  \__,_|\__\___/|_|         
";


        public static string Welcome = @$"

              Welcome to PocketBaseClient CodeGenerator
 An application to generate client side code in c# for your PocketBase application
";




        static async Task<int> Main(string[] args)
        {
            if (args.Length == 0)
            {
                Interactive.Process.Start();
                return 0;
            }

            var rootCommand = new RootCommand("PocketBaseClient Code Generator: An application to generate client side code of your PocketBase application");

            rootCommand.AddCommand(GetInteractiveCommand());
            rootCommand.AddCommand(GetSaveschemaCommand());

            // Commands:
            //  * saveschema --url --email --pwd --file
            //  * generate --schema --format --folder

            //TODO: Do it in own option, not Hardcoded!!
            //GenerateCode(file.FullName, @"C:\Dev\iluvadev\projects\PocketBaseClient-csharp\src\PocketBaseClient.SampleApp", "PocketBaseClient.SampleApp");

            return await rootCommand.InvokeAsync(args);
        }

        #region Commands
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

            commandDownloadSchema.SetHandler(async (url, email, pwd, file) => await SchemaDownloader.DownloadSchemaAsync(url!, email!, pwd!, file!),
                                             urlOption, emailOption, pwdOption, fileOption);
            return commandDownloadSchema;
        }

        private static Command GetInteractiveCommand()
        {
            var commandInteractive = new Command("i", "Executes PocketBaseClient Code Generator in interactive mode");
            commandInteractive.SetHandler(Interactive.Process.Start);
            return commandInteractive;
        }
        #endregion Commands

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


    }
}