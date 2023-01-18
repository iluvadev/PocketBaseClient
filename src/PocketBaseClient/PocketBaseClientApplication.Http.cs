// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase


using pocketbase_csharp_sdk.Models;
using pocketbase_csharp_sdk;
using PocketBaseClient.Orm;
using static System.Net.Mime.MediaTypeNames;
using System.Text.Json;
using System.IO;
using System.Net.Http;
using System.Threading;

namespace PocketBaseClient
{
    public partial class PocketBaseClientApplication
    {
        public async Task<T?> HttpGetAsync<T>(string url)
        {
            return await Sdk.SendAsync<T>(url, HttpMethod.Get);
        }

        public async Task<PagedCollectionModel<T>?> HttpGetListAsync<T>(string url, int? page = null, int? perPage = null, string? filter = null, string? sort = null)
        {
            var query = new Dictionary<string, object?>()
            {
                { "filter", filter },
                { "page", page },
                { "perPage", perPage },
                { "sort", sort },
                { "expand", null },
            };
            var pagedCollection = await Sdk.SendAsync<PagedCollectionModel<T>>(url, HttpMethod.Get, query: query);
            if (pagedCollection is null) throw new ClientException(url);
            return pagedCollection;
        }

        public async Task<bool> HttpDeleteAsync(string url)
        {
            try { await Sdk.SendAsync(url, HttpMethod.Delete); }
            catch { return false; }
            return true;
        }

        public async Task<T?> HttpPostAsync<T>(string url, T element, IEnumerable<FieldFileBase>? files = null)
        {
            // Convert Serialized element to Dictionary<string, object>
            var body = JsonSerializer.Deserialize<Dictionary<string, object>>(JsonSerializer.Serialize(element));

            return await Sdk.SendAsync<T>(url, HttpMethod.Post, body: body, files: files);
        }

        public async Task<T?> HttpPatchAsync<T>(string url, T element, IEnumerable<FieldFileBase>? files = null)
        {
            // Convert Serialized element to Dictionary<string, object>
            var body = JsonSerializer.Deserialize<Dictionary<string, object>>(JsonSerializer.Serialize(element));

            return await Sdk.SendAsync<T>(url, HttpMethod.Patch, body: body, files: files);
        }

        public async Task<Stream> GetStreamAsync(string url)
        {
            // TODO: REMOVE When: sdk project PR merged and published in NuGet
            Uri finalUrl = BuildUrl(url);
            try
            {
                var httpClient = new HttpClient();
                return await httpClient.GetStreamAsync(finalUrl);
            }
            catch (Exception ex)
            {
                if (ex is ClientException) throw;
                else if (ex is HttpRequestException) throw new ClientException(url: finalUrl.ToString(), originalError: ex, isAbort: true);
                else throw new ClientException(url: finalUrl.ToString(), originalError: ex);
            }
        }

        private Uri BuildUrl(string path)
        {
            // TODO: REMOVE When: sdk project PR merged and published in NuGet
            var url = AppUrl + (AppUrl.EndsWith("/") ? "" : "/");

            if (!string.IsNullOrWhiteSpace(path))
                url += path.StartsWith("/") ? path.Substring(1) : path;

            return new Uri(url, UriKind.RelativeOrAbsolute);
        }
    }
}
