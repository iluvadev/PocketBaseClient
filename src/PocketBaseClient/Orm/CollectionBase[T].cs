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
using PocketBaseClient.Orm.Cache;
using PocketBaseClient.Services;
using System.Web;

namespace PocketBaseClient.Orm
{
    public abstract partial class CollectionBase<T> : CollectionBase
        where T : ItemBase, new()
    {
        #region Cache
        internal CacheItems<T> Cache { get; } = new CacheItems<T>();
        internal T UpdateCached(T item)
        {
            return Cache.AddOrUpdate(item);
        }

        internal override bool AddToCache<E>(E elem)
        {
            if (elem is T item && item.Id != null)
                return Cache.AddOrUpdate(item) != null;
            return false;
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
        internal T? AddIdFromPb(string id)
        {
            var item = Cache.Get(id) ?? Cache.AddOrUpdate(new T() { Id = id });
            item.Metadata_.IsNew = false;
            return item;

        }
        internal async Task<PagedCollectionModel<T>?> GetPageFromPbAsync(int? pageNumber = null, int? perPage = null, string? filter = null, string? sort = null)
        {
            var page = await PocketBase.HttpGetListAsync<T>(UrlRecords, pageNumber, perPage, filter, sort);
            // Cache all items in the page
            foreach (var itemFromPb in page?.Items ?? Enumerable.Empty<T>())
            {
                var item = Cache.AddOrUpdate(itemFromPb);
                item.Metadata_.SetLoaded();
            }
            return page;
        }
        internal async IAsyncEnumerable<T> GetItemsFromPbAsync(string? filter = null, string? sort = null)
        {
            int loadedItems = 0;
            int? totalItems = null;
            int currentPage = 1;
            while (totalItems == null || loadedItems < totalItems)
            {
                var page = await GetPageFromPbAsync(pageNumber: currentPage, filter: filter, sort: sort);
                if (page != null)
                {
                    currentPage++;

                    totalItems = page.TotalItems;
                    var pageItems = page.Items ?? Enumerable.Empty<T>();
                    loadedItems += pageItems.Count();

                    // Return downloaded cached items
                    foreach (var item in pageItems)
                        yield return Cache.Get(item.Id!) ?? item;
                }
            }
        }
        internal IEnumerable<T> GetItemsFromPb(string? filter = null, string? sort = null)
        {
            int loadedItems = 0;
            int? totalItems = null;
            int currentPage = 1;
            while (totalItems == null || loadedItems < totalItems)
            {
                var page = GetPageFromPbAsync(pageNumber: currentPage, filter: filter, sort: sort).Result;
                if (page != null)
                {
                    currentPage++;

                    totalItems = page.TotalItems;
                    var pageItems = page.Items ?? Enumerable.Empty<T>();
                    loadedItems += pageItems.Count();

                    // Return downloaded cached items
                    foreach (var item in pageItems)
                        yield return Cache.Get(item.Id!) ?? item;
                }
            }
        }

        #endregion Support functions

        #region Fill Item from PocketBase
        private async Task<bool> FillFromPbAsync(T item)
        {
            if (item.Id == null) return false;

            var loadedItem = await PocketBase.HttpGetAsync<T>(UrlRecord(item));
            if (loadedItem == null) return false;
            loadedItem.Metadata_.SetLoaded();

            item.UpdateWith(loadedItem);
            item.Metadata_.SetLoaded();
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
                if (reload) item.Metadata_.SetNeedBeLoaded();
                return item;
                //return !forceLoad || await FillFromPbAsync(item) ? item : null;
            }
            item = new T() { Id = id };
            if (!await FillFromPbAsync(item)) return null;

            return Cache.AddOrUpdate(item);
        }
        #endregion Get Item

        #region Get All Items
        private int? _PocketBaseCount = null;

        public IEnumerable<T> Items => GetItems();

        public IEnumerable<T> GetItems(bool reload = false)
        {
            // First: return new items
            foreach (var item in Cache.NewItems)
                yield return item;

            // Count not new Items to compare with _PocketBaseCount
            if (!reload && Cache.NotNewItems.Count() == _PocketBaseCount)
            {
                // Return cached items
                foreach (var item in Cache.NotNewItems)
                    yield return item;
            }
            else
            {
                // Clean cached items and return items from PocketBase

                // Set all cached as NeedToBeLoaded
                var idsToTrash = new List<string>();
                foreach (var notNewItem in Cache.NotNewItems)
                {
                    notNewItem.Metadata_.SetNeedBeLoaded();
                    idsToTrash.Add(notNewItem.Id!);
                }

                // Get Items from PocketBase
                int loadedItems = 0;
                int currentPage = 1;
                while (_PocketBaseCount == null || loadedItems < _PocketBaseCount)
                {
                    var page = GetPageFromPbAsync(currentPage).Result;
                    if (page != null)
                    {
                        currentPage++;

                        _PocketBaseCount = page.TotalItems;
                        var pageItems = page.Items ?? Enumerable.Empty<T>();
                        loadedItems += pageItems.Count();

                        foreach (var item in pageItems)
                            idsToTrash.Remove(item.Id!);

                        // Return downloaded cached items
                        foreach (var item in pageItems)
                            yield return Cache.Get(item.Id!) ?? item;
                    }
                }

                // Mark as Trash all not downloaded
                foreach (var idToTrash in idsToTrash)
                {
                    var itemToTrash = Cache.Get(idToTrash);
                    if (itemToTrash != null)
                        itemToTrash.Metadata_.IsTrash = true;
                }
                Cache.RemoveTrash();
            }
        }
        #endregion Get All Items

        #region DiscardChanges
        public override void DiscardChanges()
        {
            foreach (var item in Cache.AllItems)
                item.DiscardChanges();

            Cache.RemoveTrash();
        }
        public void DiscardChanges(T item)
            => item.DiscardChanges();
        #endregion DiscardChanges

        #region Save Item
        public bool Save(T item, bool onlyIfChanges = false) => SaveAsync(item, onlyIfChanges).Result;
        public async Task<bool> SaveAsync(T item, bool onlyIfChanges = false)
        {
            if (item.Id == null) return false;
            if (!item.Metadata_.IsValid) return false;

            // WARNING: There is no check for circular references!!

            var newItems = item.RelatedItems.Where(i => i != null && !i.IsSame(item) && i.Metadata_.IsNew).Distinct().ToList();
            var cachedItems = item.RelatedItems.Where(i => i != null && !i.IsSame(item) && !i.Metadata_.IsNew).Distinct().ToList();

            // Save related new items
            foreach (var relatedNew in newItems)
                if (relatedNew?.Metadata_.IsNew ?? false)
                    await relatedNew.SaveAsync(true);

            // Save related changed items
            foreach (var relatedCached in cachedItems)
                if (relatedCached != null)
                    await relatedCached.SaveAsync(true);

            // WARNING: There is no wait for Cascade saving!!

            if (item.Metadata_.IsNew)
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
            item.Metadata_.SetLoaded();
            return true;
        }

        private async Task<bool> UpdateAsync(T item, bool onlyIfChanges = false)
        {
            if (item.Id == null) return false;
            if (onlyIfChanges && !item.Metadata_.HasLocalChanges) return true;

            var savedItem = await PocketBase.HttpPatchAsync(UrlRecord(item), item);
            if (savedItem == null) return false;

            item.UpdateWith(savedItem);
            item.Metadata_.SetLoaded();
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
                item.Metadata_.IsTrash = true;

            return true;
        }

        public bool Delete(T item) => DeleteAsync(item).Result;
        public async Task<bool> DeleteAsync(T item)
        {
            if (item.Id == null) return false;
            if (item.Metadata_.IsNew) return false;

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
