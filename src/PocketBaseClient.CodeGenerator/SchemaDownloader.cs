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

namespace PocketBaseClient.CodeGenerator
{
    /// <summary>
    /// Downloader Schema from PocketBase server
    /// </summary>
    internal class SchemaDownloader
    {
        /// <summary>
        /// Downloads the Schema from PocketBase server
        /// </summary>
        /// <param name="url">Url of the PocketBase server</param>
        /// <param name="email">email of an admin account</param>
        /// <param name="pwd">password for the admin account</param>
        /// <returns></returns>
        public static async Task<PocketBaseSchema?> DownloadSchemaAsync(Uri url, string email, string pwd)
        {
            var app = new PocketBaseClientApplication(url.ToString());

            ConsoleHelper.WriteProcess($"Connecting to {url} with Admin {email}");
            var admin = await app.Auth.Admin.AuthenticateWithPassword(email, pwd);
            if (string.IsNullOrEmpty(admin?.Token))
            {
                ConsoleHelper.WriteError($"Failed to connect to {url} with Admin {email} credentials");
                return null;
            }
            ConsoleHelper.WriteDone();

            var schema = new PocketBaseSchema();

            ConsoleHelper.WriteProcess($"Getting PocketBase Application Settings");
            schema.SetPocketBaseApplicationAsync(await app.Sdk.Settings.GetAllAsync());
            ConsoleHelper.WriteDone();

            ConsoleHelper.WriteProcess($"Getting PocketBase Application Collections");
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
            schema.SchemaDate = DateTime.UtcNow;
            ConsoleHelper.WriteDone();

            return schema;
        }
    }
}
