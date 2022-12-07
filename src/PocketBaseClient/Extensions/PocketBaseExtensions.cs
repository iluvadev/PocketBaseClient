using pocketbase_csharp_sdk;
using pocketbase_csharp_sdk.Models;
using System.Text.Json;

namespace PocketBaseClient
{
    public static class PocketBaseExtensions
    {
        internal static async Task<T?> HttpGetAsync<T>(this PocketBase pocketBase, string url)
        {
            return await pocketBase.SendAsync<T>(url, HttpMethod.Get);
        }

        public static async Task<PagedCollectionModel<T>?> HttpGetListAsync<T>(this PocketBase pocketBase, string url, int? page = null, int? perPage = null)
        {
            var query = new Dictionary<string, object?>()
            {
                { "filter", null },
                { "page", page },
                { "perPage", perPage },
                { "sort", null },
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
