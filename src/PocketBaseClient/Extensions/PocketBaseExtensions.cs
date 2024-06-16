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
using PocketBaseClient.Orm;
using PocketBaseClient.Orm.Json;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PocketBaseClient
{
    /// <summary>
    /// Extensions for PocketBase
    /// </summary>
    public static class PocketBaseExtensions
    {
        internal static async Task<T?> HttpGetAsync<T>(this PocketBase pocketBase, string url)
        {
            return await pocketBase.SendAsync<T>(url, HttpMethod.Get);
        }
        internal static T? HttpGet<T>(this PocketBase pocketBase, string url)
        {
            return pocketBase.Send<T>(url, HttpMethod.Get);
        }


        public static async Task<PagedCollectionModel<T>?> HttpGetListAsync<T>(this PocketBase pocketBase, string url, int? page = null, int? perPage = null, string? filter = null, string? sort = null)
        {
            var query = new Dictionary<string, object?>()
            {
                { "filter", filter },
                { "page", page },
                { "perPage", perPage },
                { "sort", sort },
                { "expand", null },
            };
            var pagedCollection = await pocketBase.SendAsync<PagedCollectionModel<T>>(url, HttpMethod.Get, query: query);
            if (pagedCollection is null) throw new ClientException(url);

            return pagedCollection;
        }
        public static PagedCollectionModel<T> HttpGetList<T>(this PocketBase pocketBase, string url, int? page = null, int? perPage = null, string? filter = null, string? sort = null)
        {
            var query = new Dictionary<string, object?>()
            {
                { "filter", filter },
                { "page", page },
                { "perPage", perPage },
                { "sort", sort },
                { "expand", null },
            };
            var pagedCollection = pocketBase.Send<PagedCollectionModel<T>>(url, HttpMethod.Get, query: query);
            if (pagedCollection is null) throw new ClientException(url);

            return pagedCollection;
        }


        internal static async Task<bool> HttpDeleteAsync(this PocketBase pocketBase, string url)
        {
            try { await pocketBase.SendAsync(url, HttpMethod.Delete); }
            catch { return false; }
            return true;
        }
        internal static bool HttpDelete(this PocketBase pocketBase, string url)
        {
            try { pocketBase.Send(url, HttpMethod.Delete); }
            catch { return false; }
            return true;
        }


        internal static async Task<T?> HttpPostAsync<T>(this PocketBase pocketBase, string url, T element)
        {
            // Convert Serialized element to Dictionary<string, object>
            var body = JsonSerializer.Deserialize<Dictionary<string, object>>(JsonSerializer.Serialize(element));

            return await pocketBase.HttpPostAsync<T>(url, body);
        }
        internal static async Task<T?> HttpPostAsync<T>(this PocketBase pocketBase, string url, Dictionary<string, object>? body)
        {
            return await pocketBase.SendAsync<T>(url, HttpMethod.Post, body: body);
        }
        internal static T? HttpPost<T>(this PocketBase pocketBase, string url, T element)
        {
            // Convert Serialized element to Dictionary<string, object>
            var body = JsonSerializer.Deserialize<Dictionary<string, object>>(JsonSerializer.Serialize(element));

            return pocketBase.HttpPost<T>(url, body: body);
        }
        internal static T? HttpPost<T>(this PocketBase pocketBase, string url, Dictionary<string, object>? body)
        {
            return pocketBase.Send<T>(url, HttpMethod.Post, body: body);
        }


        internal static async Task<T?> HttpPatchAsync<T>(this PocketBase pocketBase, string url, T item)
            where T : ItemBase
        {
            var body = JsonSerializer.Deserialize<Dictionary<string, object>>(JsonSerializer.Serialize(item));
            var files = item.RelatedFiles.Where(f => f != null && f.HasChanges)?.Select(f => f!.GetSdkFileToUpload()).Where(f => f != null).Select(f => f!)?.ToList();

            if (files?.Any() ?? false)
            {
                await pocketBase.SendAsync<T>(url, HttpMethod.Patch, files: files);
            }
            // remove FieldFileBase field for fix The field contains unknown filenames.
            foreach (var property in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
                     .Where(prop => prop.PropertyType.IsSubclassOf(typeof(FieldFileBase))))
            {
                var jsonPropertyNameAttribute = property.GetCustomAttribute<JsonPropertyNameAttribute>();
                if (jsonPropertyNameAttribute is not null)
                {
                    body.Remove(jsonPropertyNameAttribute.Name);
                }
            }
            var result = await pocketBase.SendAsync<T>(url, HttpMethod.Patch, body: body);
            return result;
        }
        internal static T? HttpPatch<T>(this PocketBase pocketBase, string url, T item)
            where T : ItemBase
        {
            // Convert Serialized element to Dictionary<string, object>
            var body = JsonSerializer.Deserialize<Dictionary<string, object>>(JsonSerializer.Serialize(item));
            var files = item.RelatedFiles.Where(f => f != null && f.HasChanges)?.Select(f => f!.GetSdkFileToUpload()).Where(f => f != null).Select(f => f!)?.ToList();

            var result = pocketBase.Send<T>(url, HttpMethod.Patch, body: body);

            if (files?.Any() ?? false)
                return pocketBase.Send<T>(url, HttpMethod.Patch, files: files);

            return result;
        }


        internal static async Task<Stream> HttpGetStreamAsync(this PocketBase pocketBase, string url, string? thumb = null)
        {
            var query = new Dictionary<string, object?>();
            if (!string.IsNullOrEmpty(thumb))
                query.Add("thumb", thumb);

            return await pocketBase.GetStreamAsync(url, query);
        }

    }
}
