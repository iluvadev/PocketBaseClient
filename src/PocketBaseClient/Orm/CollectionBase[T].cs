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
        #region Cache
        internal CacheItems<T> Cache { get; } = new CacheItems<T>();
        internal override int CachedItemsCount => Cache.Count;

        public IEnumerable<T> CachedItems => Cache.AllItems;

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
        #endregion Cache

        #region Url
        internal string UrlRecords => $"/api/collections/{HttpUtility.UrlEncode(Name)}/records";
        internal string UrlRecord(string id) => $"{UrlRecords}/{HttpUtility.UrlEncode(id)}";
        internal string UrlRecord(T item) => UrlRecord(item.Id!);
        #endregion  Url

        public CollectionBase(DataServiceBase context) : base(context) { }

        #region Support functions
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

        internal T? AddIdFromPb(string id)
        {
            var item = Cache.Get(id) ?? Cache.AddOrUpdate(new T() { Id = id });
            item.Metadata.IsNew = false;
            return item;

        }
        #endregion Support functions

        #region Count
        public int LoadCount()
        {
            var page = PocketBase.HttpGetListAsync<T>(UrlRecords, 1, 1).Result;
            GetItemsPage(page, true);

            return Metadata.Count ?? 0;
        }
        #endregion Count

        #region Fill Item from PocketBase
        private async Task<bool> FillFromPbAsync(T item)
        {
            if (item.Id == null) return false;

            var loadedItem = await PocketBase.HttpGetAsync<T>(UrlRecord(item));
            if (loadedItem == null) return false;
            loadedItem.Metadata.SetLoaded();

            item.UpdateWith(loadedItem);
            item.Metadata.SetLoaded();
            return true;
        }

        internal override async Task<bool> FillFromPbAsync<E>(E elem)
        {
            if (elem is T item)
                return await FillFromPbAsync(item);
            return false;
        }
        #endregion Fill Item from PocketBase

        #region Get Item
        public T? GetById(string id, bool reload = false) => GetByIdAsync(id, reload).Result;

        public async Task<T?> GetByIdAsync(string id, bool reload = false)
        {
            T? item = Cache.Get(id);
            if (item != null)
            {
                if (reload) item.Metadata.SetNeedBeLoaded();
                return item;
                //return !forceLoad || await FillFromPbAsync(item) ? item : null;
            }
            item = new T() { Id = id };
            if (!await FillFromPbAsync(item)) return null;

            return Cache.AddOrUpdate(item);
        }
        #endregion Get Item

        #region Get Multiple Items
        private IEnumerable<T> GetItemsPage(PagedCollectionModel<T>? page, bool updateCount = false)
        {
            if (page == null) return Enumerable.Empty<T>();
            if (updateCount)
                Metadata.Count = page.TotalItems;
            return AddLoadedRange(page.Items ?? Enumerable.Empty<T>());
        }

        public IEnumerable<T> LoadItems()
        {
            int loadedItems = 0;
            int? totalItems = null;

            while (totalItems == null || loadedItems < totalItems)
            {
                var page = PocketBase.HttpGetListAsync<T>(UrlRecords).Result;
                totalItems ??= page?.TotalItems;

                foreach (var item in GetItemsPage(page, true))
                {
                    loadedItems++;
                    yield return AddLoaded(item);
                }
            }
        }
        #endregion Get Multiple Items

        #region DiscardChanges
        public override void DiscardChanges()
        {
            foreach (var item in Cache.AllItems)
                item.DiscardChanges();
        }
        public void DiscardChanges(T item)
            => item.DiscardChanges();
        #endregion DiscardChanges

        #region Save Item
        public bool Save(T item, bool onlyIfChanges = false) => SaveAsync(item, onlyIfChanges).Result;
        public async Task<bool> SaveAsync(T item, bool onlyIfChanges = false)
        {
            if (item.Id == null) return false;
            if (!item.Metadata.IsValid) return false;

            // WARNING: There is no check for circular references!!

            var newItems = item.RelatedItems.Where(i => i != null && !i.IsSame(item) && i.Metadata.IsNew).Distinct().ToList();
            var cachedItems = item.RelatedItems.Where(i => i != null && !i.IsSame(item) && !i.Metadata.IsNew).Distinct().ToList();

            // Save related new items
            foreach (var relatedNew in newItems)
                if (relatedNew?.Metadata.IsNew ?? false)
                    await relatedNew.SaveAsync(true);

            // Save related changed items
            foreach (var relatedCached in cachedItems)
                if (relatedCached != null)
                    await relatedCached.SaveAsync(true);

            // WARNING: There is no wait for Cascade saving!!

            if (item.Metadata.IsNew)
                return await CreateAsync(item);
            else
                return await UpdateAsync(item, onlyIfChanges);
        }

        internal override async Task<bool> SaveAsync<E>(E elem, bool onlyIfChanges = false)
        {
            if (elem is T item)
                return await SaveAsync(item, onlyIfChanges);
            return false;
        }

        private async Task<bool> CreateAsync(T item)
        {
            var savedItem = await PocketBase.HttpPostAsync(UrlRecords, item);
            if (savedItem == null) return false;

            item.UpdateWith(savedItem);
            item.Metadata.SetLoaded();
            return true;
        }

        private async Task<bool> UpdateAsync(T item, bool onlyIfChanges = false)
        {
            if (item.Id == null) return false;
            if (onlyIfChanges && !item.Metadata.HasLocalChanges) return true;

            var savedItem = await PocketBase.HttpPatchAsync(UrlRecord(item), item);
            if (savedItem == null) return false;

            item.UpdateWith(savedItem);
            item.Metadata.SetLoaded();
            return true;
        }
        #endregion Save Item

        #region Delete Item
        public bool DeleteById(string id) => DeleteByIdAsync(id).Result;
        public async Task<bool> DeleteByIdAsync(string? id)
        {
            if (id == null) return false;

            if (!await PocketBase.HttpDeleteAsync(UrlRecord(id))) return false;

            //Remove from Cache
            var item = Cache.Remove(id);
            if (item != null)
                item.Metadata.IsTrash = true;

            return true;
        }

        public bool Delete(T item) => DeleteAsync(item).Result;
        public async Task<bool> DeleteAsync(T item)
        {
            if (item.Id == null) return false;
            if (item.Metadata.IsNew) return false;

            return await DeleteByIdAsync(item.Id);
        }
        internal override async Task<bool> DeleteAsync<E>(E elem)
        {
            if (elem is T item)
                return await DeleteAsync(item);

            return false;
        }
        #endregion  Delete Item
    }
}
