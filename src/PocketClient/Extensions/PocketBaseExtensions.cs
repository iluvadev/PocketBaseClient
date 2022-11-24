using pocketbase_csharp_sdk;
using pocketbase_csharp_sdk.Models;

namespace PocketClient
{
    internal static class PocketBaseExtensions
    {
        //public async Task<PagedCollectionModel<T>> ListAsync(
        //    int? page = null,
        //    int? perPage = null,
        //    string? sort = null,
        //    string? filter = null,
        //    string? expand = null,
        //    IDictionary<string, string>? headers = null)
        //{
        //    var query = new Dictionary<string, object?>()
        //    {
        //        { "filter", filter },
        //        { "page", page },
        //        { "perPage", perPage },
        //        { "sort", sort },
        //        { "expand", expand },
        //    };
        //    var pagedCollection = await client.SendAsync<PagedCollectionModel<T>>(
        //        BasePath(),
        //        HttpMethod.Get,
        //        headers: headers,
        //        query: query);
        //    if (pagedCollection is null) throw new ClientException(BasePath());

        //    return pagedCollection;
        //}

        internal static Dictionary<string, string> AddAuthorizationHeader(this PocketBase pocketBase, Dictionary<string, string>? headers = null)
        {
            headers ??= new Dictionary<string, string>();
            if (!headers.ContainsKey("Authorization") && pocketBase.AuthStore.IsValid && pocketBase.AuthStore.Token != null)
                headers["Authorization"] = pocketBase.AuthStore.Token;
            return headers;
        }
        internal static async Task<T?> HttpGetAsync<T>(this PocketBase pocketBase, string url)
        {
            return await pocketBase.SendAsync<T>(url, HttpMethod.Get, headers: AddAuthorizationHeader(pocketBase));
        }

        internal static async Task<PagedCollectionModel<T>?> HttpGetListAsync<T>(this PocketBase pocketBase, string url, int? page = null, int? perPage = null)
        {
            var query = new Dictionary<string, object?>()
            {
                { "filter", null },
                { "page", page },
                { "perPage", perPage },
                { "sort", null },
                { "expand", null },
            };
            var pagedCollection = await pocketBase.SendAsync<PagedCollectionModel<T>>(url, HttpMethod.Get, headers: AddAuthorizationHeader(pocketBase), query: query);
            if (pagedCollection is null) throw new ClientException(url);

            return pagedCollection;
        }

    }
}
