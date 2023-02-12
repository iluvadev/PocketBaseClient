using System.Web;
using pocketbase_csharp_sdk.Models;
using PocketBaseClient.Orm.Cache;
using PocketBaseClient.Orm.Structures;

namespace PocketBaseClient.Orm
{
    public partial class CollectionBase<T>
    {
        #region Cache
        internal CacheItems<T> Cache { get; } = new CacheItems<T>();

        internal override bool ChangeIdInCache<E>(string oldId, E elem)
        {
            if (elem is T item && item.Id != null)
                return Cache.ChangeId(oldId, item) != null;
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

        #region Support functions
        internal T? AddIdFromPb(string id)
        {
            var item = Cache.Get(id) ?? Cache.AddOrUpdate(new T() { Id = id });
            item.Metadata_.SyncStatus = ItemSyncStatuses.Loaded;
            return item;

        }
        internal async Task<PagedCollectionModel<T>?> GetPageFromPbAsync(int? pageNumber = null, int? perPage = null, string? filter = null, string? sort = null)
        {
            var page = await App.Sdk.HttpGetListAsync<T>(UrlRecords, pageNumber, perPage, filter, sort);
            // Cache all items in the page automatically at creation
            foreach (var itemFromPb in page?.Items ?? Enumerable.Empty<T>())
                itemFromPb.Metadata_.SetLoaded();

            return page;
        }
        internal PagedCollectionModel<T>? GetPageFromPb(int? pageNumber = null, int? perPage = null, string? filter = null, string? sort = null)
        {
            var page = App.Sdk.HttpGetList<T>(UrlRecords, pageNumber, perPage, filter, sort);
            // Cache all items in the page automatically at creation
            foreach (var itemFromPb in page?.Items ?? Enumerable.Empty<T>())
                itemFromPb.Metadata_.SetLoaded();

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

                    foreach (var item in pageItems)
                        yield return item;

                    //// Return downloaded cached items
                    //foreach (var item in pageItems)
                    //    yield return Cache.Get(item.Id!) ?? item;
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
                var page = GetPageFromPb(currentPage, filter: filter, sort: sort);
                if (page != null)
                {
                    currentPage++;

                    totalItems = page.TotalItems;
                    var pageItems = page.Items ?? Enumerable.Empty<T>();
                    loadedItems += pageItems.Count();

                    foreach (var item in pageItems)
                        yield return item;

                    //// Return downloaded cached items
                    //foreach (var item in pageItems)
                    //    yield return Cache.Get(item.Id!) ?? item;
                }
            }
        }

        #endregion Support functions

        #region Fill Item from PocketBase
        private async Task<bool> FillFromPbAsync(T item)
        {
            if (item.Id == null) return false;

            var loadedItem = await App.Sdk.HttpGetAsync<T>(UrlRecord(item));
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

        private bool FillFromPb(T item)
        {
            if (item.Id == null) return false;

            var loadedItem = App.Sdk.HttpGet<T>(UrlRecord(item));
            if (loadedItem == null) return false;
            loadedItem.Metadata_.SetLoaded();

            item.UpdateWith(loadedItem);
            item.Metadata_.SetLoaded();
            return true;
        }
        internal override bool FillFromPb<E>(E elem)
        {
            if (elem is T item)
                return FillFromPb(item);
            return false;
        }

        #endregion Fill Item from PocketBase

        #region Get Items
        private int? _PocketBaseItemsCount = null;

        private IEnumerable<T> GetItemsInternal(bool reload = false, GetItemsFilter include = GetItemsFilter.Load | GetItemsFilter.New)
        {
            //TODO: Need a review
            // If an Item has local changes? Changes will be lost? 
            // Force Reload all cached items? Also items not yielded?

            var allCachedItems = Cache.AllItems.ToList();

            // Count not new Items to compare with _PocketBaseCount
            if (!reload && allCachedItems.Count(i => !i.Metadata_.IsNew) == _PocketBaseItemsCount)
            {
                // Return cached items
                foreach (var item in allCachedItems)
                {
                    // Check if item must be returned
                    if (item.Metadata_.MatchFilter(include))
                        yield return item;
                }
            }
            else
            {
                // Return cached new items if must be returned
                if ((include & GetItemsFilter.New) == GetItemsFilter.New)
                    foreach (var item in allCachedItems.Where(i => i.Metadata_.SyncStatus == ItemSyncStatuses.ToBeCreated))
                        yield return item;

                // Clean cached items and return items from PocketBase

                // Set all cached loaded items as NeedToBeLoaded
                var idsToTrash = new List<string>();
                foreach (var notNewItem in allCachedItems.Where(i => i.Metadata_.SyncStatus != ItemSyncStatuses.ToBeCreated))
                {
                    notNewItem.Metadata_.SetNeedBeLoaded();
                    idsToTrash.Add(notNewItem.Id!);
                }

                // Get Items from PocketBase
                int loadedItems = 0;
                int currentPage = 1;
                while (_PocketBaseItemsCount == null || loadedItems < _PocketBaseItemsCount)
                {
                    // Get page in sync mode
                    var page = GetPageFromPb(currentPage);
                    if (page != null)
                    {
                        currentPage++;

                        _PocketBaseItemsCount = page.TotalItems;
                        var pageItems = page.Items ?? Enumerable.Empty<T>();
                        loadedItems += pageItems.Count();

                        foreach (var item in pageItems)
                            idsToTrash.Remove(item.Id!);

                        foreach (var item in pageItems)
                            // Check if item must be returned
                            if (item.Metadata_.MatchFilter(include))
                                yield return item;
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

        private async Task<T?> GetByIdInternalAsync(string? id, bool reload = false)
        {
            if (id == null) return null;
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
        private T? GetByIdInternal(string? id, bool reload = false)
        {
            if (id == null) return null;
            T? item = Cache.Get(id);
            if (item != null)
            {
                if (reload) item.Metadata_.SetNeedBeLoaded();
                return item;
                //return !forceLoad || await FillFromPbAsync(item) ? item : null;
            }
            item = new T() { Id = id };
            if (!FillFromPb(item)) return null;

            return Cache.AddOrUpdate(item);
        }
        #endregion Get Items

        #region Save
        private async Task<bool> SaveInternalAsync(T item, bool onlyIfChanges = true)
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
                return await CreateInternalAsync(item);
            else if (item.Metadata_.IsTobeDeleted)
                return await DeleteInternalAsync(item);
            else
                return await UpdateInternalAsync(item, onlyIfChanges);
        }
        private bool SaveInternal(T item, bool onlyIfChanges = true)
        {
            //TODO: Throw exceptions in case of Id null or not Valid
            if (item.Id == null) return false;
            if (!item.Metadata_.IsValid) return false;

            // WARNING: There is no check for circular references!!

            var newItems = item.RelatedItems.Where(i => i != null && !i.IsSame(item) && i.Metadata_.IsNew).Distinct().ToList();
            var cachedItems = item.RelatedItems.Where(i => i != null && !i.IsSame(item) && !i.Metadata_.IsNew).Distinct().ToList();

            // Save related new items
            foreach (var relatedNew in newItems)
                if (relatedNew?.Metadata_.IsNew ?? false)
                    relatedNew.Save(true);

            // Save related changed items
            foreach (var relatedCached in cachedItems)
                if (relatedCached != null)
                    relatedCached.Save(true);

            // WARNING: There is no wait for Cascade saving!!

            if (item.Metadata_.IsNew)
                return CreateInternal(item);
            else if (item.Metadata_.IsTobeDeleted)
                return DeleteInternal(item);
            else
                return UpdateInternal(item, onlyIfChanges);
        }

        private async Task<bool> CreateInternalAsync(T item)
        {
            var savedItem = await App.Sdk.HttpPostAsync(UrlRecords, item);
            if (savedItem == null) return false;

            item.UpdateWith(savedItem);
            item.Metadata_.SetLoaded();
            return true;
        }
        private bool CreateInternal(T item)
        {
            var savedItem = App.Sdk.HttpPost(UrlRecords, item);
            if (savedItem == null) return false;

            item.UpdateWith(savedItem);
            item.Metadata_.SetLoaded();
            return true;
        }


        private async Task<bool> UpdateInternalAsync(T item, bool onlyIfChanges = true)
        {
            if (item.Id == null) return false;
            if (onlyIfChanges && !item.Metadata_.HasLocalChanges) return true;

            var savedItem = await App.Sdk.HttpPatchAsync(UrlRecord(item), item);
            if (savedItem == null) return false;

            item.UpdateWith(savedItem);
            item.Metadata_.SetLoaded();
            return true;
        }
        private bool UpdateInternal(T item, bool onlyIfChanges = true)
        {
            if (item.Id == null) return false;
            if (onlyIfChanges && !item.Metadata_.HasLocalChanges) return true;

            var savedItem = App.Sdk.HttpPatch(UrlRecord(item), item);
            if (savedItem == null) return false;

            item.UpdateWith(savedItem);
            item.Metadata_.SetLoaded();
            return true;
        }

        private async Task<bool> DeleteInternalAsync(T item)
        {
            if (item.Id == null) return false;
           
            if (!await App.Sdk.HttpDeleteAsync(UrlRecord(item))) return false;

            // If is the Auth registry, delete it
            if (item.IsSame(App.Auth.AuthStore.Model))
                App.Auth.AuthStore.Clear();

            //Remove from Cache
            Cache.Remove(item.Id);
            item.Metadata_.IsTrash = true;

            return true;
        }
        private bool DeleteInternal(T item)
        {
            if (item.Id == null) return false;

            if (!App.Sdk.HttpDelete(UrlRecord(item))) return false;

            //Remove from Cache
            Cache.Remove(item.Id);
            item.Metadata_.IsTrash = true;

            return true;
        }
        #endregion Save

    }
}
