using pocketbase_csharp_sdk;
using pocketbase_csharp_sdk.Models;
using PocketBaseClient.Orm.Cache;
using PocketBaseClient.Services;
using System.Web;

namespace PocketBaseClient.Orm
{
    public abstract class CollectionBase<T> : CollectionBase
        where T : ItemBase, new()
    {
        internal CacheItems<T> Cache { get; } = new CacheItems<T>();
        internal override int CachedItemsCount => Cache.Count;

        public CollectionBase(DataServiceBase context) : base(context) { }

        private T AddLoaded(T item)
        {
            var cachedItem = Cache.AddOrUpdate(item);
            cachedItem.Metadata.SetLoaded();

            return cachedItem;
        }
        private IEnumerable<T> AddLoadedRange(IEnumerable<T> items)
        {
            foreach (var item in items)
                yield return AddLoaded(item);
        }

        private T? AddLoadedIfNotNull(T? item)
        {
            if (item == null) return null;
            return AddLoaded(item);
        }

        internal T? AddOrGetById(string id) => Cache.Get(id) ?? Cache.AddOrUpdate(new T() { Id = id });
        public T? GetById(string id, bool forceLoad = false) => GetByIdAsync(id, forceLoad).Result;

        public async Task<T?> GetByIdAsync(string id, bool forceLoad = false)
        {
            T? item = forceLoad ? null : Cache.Get(id);
            if (item != null) return item;

            // /api/collections/collectionIdOrName/records/recordId
            string url = $"/api/collections/{HttpUtility.UrlEncode(Name)}/records/{HttpUtility.UrlEncode(id)}";

            return AddLoadedIfNotNull(await PocketBase.HttpGetAsync<T>(url));
        }


        private IEnumerable<T> GetItemsPage(PagedCollectionModel<T>? page, bool updateCount = false)
        {
            if (page == null) return Enumerable.Empty<T>();
            if (updateCount)
                Metadata.Count = page.TotalItems;
            return AddLoadedRange(page.Items ?? Enumerable.Empty<T>());
        }

        public int LoadCount()
        {
            string url = $"/api/collections/{HttpUtility.UrlEncode(Name)}/records";
            var page = PocketBase.HttpGetListAsync<T>(url, 1, 1).Result;
            GetItemsPage(page, true);

            return Metadata.Count ?? 0;
        }

        public IEnumerable<T> CachedItems => Cache.AllItems;

        public IEnumerable<T> LoadItems()
        {
            int loadedItems = 0;
            int? totalItems = null;

            // /api/collections/collectionIdOrName/records
            string url = $"/api/collections/{HttpUtility.UrlEncode(Name)}/records";

            while (totalItems == null || loadedItems < totalItems)
            {
                var page = PocketBase.HttpGetListAsync<T>(url).Result;
                totalItems ??= page?.TotalItems;

                foreach (var item in GetItemsPage(page, true))
                {
                    loadedItems++;
                    yield return AddLoaded(item);
                }
            }
        }

        internal T UpdateCached(T item)
        {
            return Cache.AddOrUpdate(item);
        }

        internal override bool CacheContains<E>(E elem)
        {
            if (elem is T item && item.Id != null)
                return Cache.Get(item.Id)?.GetHashCode() == item.GetHashCode();
            return false;
        }
    }
}
