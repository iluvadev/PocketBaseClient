using PocketBaseClient.CodeGenerator.Models;
using System.CommandLine;
using System.Reflection.PortableExecutable;
using PocketBaseClient;
using pocketbase_csharp_sdk.Models.Collection;

namespace PocketBaseClient.CodeGenerator
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            var rootCommand = new RootCommand("PocketBaseClient Code Generator: An application to generate client side code of your PocketBase application");
            rootCommand.AddCommand(InitializeDownloadSchema());

            return await rootCommand.InvokeAsync(args);
        }

        private static Command InitializeDownloadSchema()
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

        static async Task DownloadSchema(Uri url, string email, string pwd, FileInfo file)
        {
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
                var collections = await app.Sdk.Collections.ListAsync();
                totalItems = collections.TotalItems;
                schema.Collections.AddRange(collections.Items ?? Enumerable.Empty<CollectionModel>());
            }

            Console.WriteLine($"Saving to file {file.FullName}...");
            schema.SaveToFile(file.FullName);

            Console.WriteLine($"Done!");
        }

        //internal static async Task ReadFile(
        //        FileInfo file, int delay, ConsoleColor fgColor, bool lightMode)
        //{
        //    Console.BackgroundColor = lightMode ? ConsoleColor.White : ConsoleColor.Black;
        //    Console.ForegroundColor = fgColor;
        //    List<string> lines = File.ReadLines(file.FullName).ToList();
        //    foreach (string line in lines)
        //    {
        //        Console.WriteLine(line);
        //        await Task.Delay(delay * line.Length);
        //    };
        //}
    }
}