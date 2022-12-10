using pocketbase_csharp_sdk.Models.Collection;
using PocketBaseClient.CodeGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketBaseClient.CodeGenerator
{
    internal class SchemaDownloader
    {
        public static async Task DownloadSchema(Uri url, string email, string pwd, FileInfo file)
        {
            var app = new PocketBaseClientApplication(url.ToString());

            ConsoleExtensions.WriteProcess($"Connecting to {url} with Admin {email}");
            var admin = await app.Auth.Admin.AuthenticateWithPassword(email, pwd);
            if (string.IsNullOrEmpty(admin?.Token))
            {
                ConsoleExtensions.WriteError($"Failed to connect to {url} with Admin {email} credentials");
                return;
            }
            ConsoleExtensions.WriteDone();

            var schema = new PocketBaseSchema();

            ConsoleExtensions.WriteProcess($"Getting PocketBase Application Settings");
            schema.SetSettingsAsync(await app.Sdk.Settings.GetAllAsync());
            ConsoleExtensions.WriteDone();

            ConsoleExtensions.WriteProcess($"Getting PocketBase Application Collections");
            int? totalItems = null;
            int currentPage = 1;
            while (totalItems == null || schema.Collections.Count < totalItems)
            {
                var collections = await app.Sdk.HttpGetListAsync<CollectionModel>("/api/collections", page: currentPage);
                //var collections = await app.Sdk.Collections.ListAsync();
                totalItems = collections!.TotalItems;
                schema.Collections.AddRange(collections.Items ?? Enumerable.Empty<CollectionModel>());
                currentPage++;
            }
            ConsoleExtensions.WriteDone();

            ConsoleExtensions.WriteProcess($"Saving to file {file.FullName}");
            schema.SaveToFile(file.FullName);
            ConsoleExtensions.WriteDone();
        }
    }
}
