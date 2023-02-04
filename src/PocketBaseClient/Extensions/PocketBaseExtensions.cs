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
using pocketbase_csharp_sdk.Event;
using pocketbase_csharp_sdk.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace PocketBaseClient
{
    /// <summary>
    /// Extensions for PocketBase
    /// </summary>
    public static class PocketBaseExtensions
    {
        #region Sync
        private static HttpClient? _HttpClient = null;
        private static HttpClient HttpClient => _HttpClient ??= new HttpClient();


        internal static T? HttpGet<T>(this PocketBase pocketBase, string url)
        {
            return pocketBase.Send<T>(url, HttpMethod.Get);
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

        private static T? Send<T>(this PocketBase pocketBase, string path, HttpMethod method, IDictionary<string, string>? headers = null, IDictionary<string, object?>? query = null, IDictionary<string, object>? body = null)
        {
            headers ??= new Dictionary<string, string>();
            query ??= new Dictionary<string, object?>();
            body ??= new Dictionary<string, object>();

            Uri url = pocketBase.BuildUrl(path, query);

            using (HttpRequestMessage request = pocketBase.CreateRequest(url, method, headers: headers, query: query, body: body))
            {
                try
                {
                    var response = HttpClient.Send(request);
                    if ((int)response.StatusCode >= 400)
                        throw new ClientException(url.ToString(), statusCode: (int)response.StatusCode);

                    response.EnsureSuccessStatusCode();

                    using (var stream = response.Content.ReadAsStream())
                        return JsonSerializer.Deserialize<T>(stream);
                }
                catch (Exception ex)
                {
                    if (ex is ClientException) throw;
                    throw new ClientException(url: url.ToString(), originalError: ex, isAbort: ex is HttpRequestException);
                }
            }
        }

        private static HttpRequestMessage CreateRequest(this PocketBase pocketBase, Uri url, HttpMethod method, IDictionary<string, string> headers, IDictionary<string, object?> query, IDictionary<string, object> body)
        {
            HttpRequestMessage request;

            request = BuildJsonRequest(method, url, headers, body);

            if (!headers.ContainsKey("Authorization") && pocketBase.AuthStore.IsValid)
                request.Headers.Add("Authorization", pocketBase.AuthStore.Token);

            if (!headers.ContainsKey("Accept-Language"))
                request.Headers.Add("Accept-Language", "en-US");// pocketBase._language);

            return request;
        }
        private static HttpRequestMessage BuildJsonRequest(HttpMethod method, Uri url, IDictionary<string, string>? headers = null, IDictionary<string, object>? body = null)
        {
            var request = new HttpRequestMessage(method, url);
            if (body is not null && body.Count > 0)
                request.Content = JsonContent.Create(body);

            if (headers is not null)
                foreach (var header in headers)
                    request.Headers.Add(header.Key, header.Value);

            return request;
        }
        #endregion Sync

        internal static async Task<T?> HttpGetAsync<T>(this PocketBase pocketBase, string url)
        {
            return await pocketBase.SendAsync<T>(url, HttpMethod.Get);
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

        internal static async Task<bool> HttpDeleteAsync(this PocketBase pocketBase, string url)
        {
            try { await pocketBase.SendAsync(url, HttpMethod.Delete); }
            catch { return false; }
            return true;
        }

        internal static async Task<T?> HttpPostAsync<T>(this PocketBase pocketBase, string url, T element)
        {
            // Convert Serialized element to Dictionary<string, object>
            var body = JsonSerializer.Deserialize<Dictionary<string, object>>(JsonSerializer.Serialize(element));

            return await pocketBase.SendAsync<T>(url, HttpMethod.Post, body: body);
        }

        internal static async Task<T?> HttpPatchAsync<T>(this PocketBase pocketBase, string url, T element)
        {
            // Convert Serialized element to Dictionary<string, object>
            var body = JsonSerializer.Deserialize<Dictionary<string, object>>(JsonSerializer.Serialize(element));

            return await pocketBase.SendAsync<T>(url, HttpMethod.Patch, body: body);
        }

    }
}
