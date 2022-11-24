using pocketbase_csharp_sdk;
using pocketbase_csharp_sdk.Models;

namespace PocketBaseClient
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

        internal static async Task<T?> HttpGetAsync<T>(this PocketBase pocketBase, string url)
        {
            return await pocketBase.SendAsync<T>(url, HttpMethod.Get);
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
            var pagedCollection = await pocketBase.SendAsync<PagedCollectionModel<T>>(url, HttpMethod.Get, query: query);
            if (pagedCollection is null) throw new ClientException(url);

            return pagedCollection;
        }

    }
}
