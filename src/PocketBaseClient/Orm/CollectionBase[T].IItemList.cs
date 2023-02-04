// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using PocketBaseClient.Orm.Structures;
using System.Collections;

namespace PocketBaseClient.Orm
{
    public abstract partial class CollectionBase<T>
    {
        internal override IEnumerable GetCachedObjects()
            => Cache.AllItems;
        internal override IEnumerable GetObjects()
            => GetItems();

        /// <inheritdoc />
        public IEnumerable<T> GetItems(bool reload = false, GetItemsFilter include = GetItemsFilter.Load | GetItemsFilter.New)
            => GetItemsInternal(reload, include);

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator()
            => GetItems().GetEnumerator();

        /// <summary>
        /// Gets the item, with its id (async)
        /// </summary>
        /// <param name="id">The id of the item to get</param>
        /// <param name="reload">True if is forced to reload from PocketBase ignoring Cache (default is false)</param>
        /// <returns></returns>
        public async Task<T?> GetByIdAsync(string? id, bool reload = false)
            => await GetByIdInternalAsync(id, reload);

        /// <summary>
        /// Gets the item, with its id 
        /// </summary>
        /// <param name="id">The id of the item to get</param>
        /// <param name="reload">True if is forced to reload from PocketBase ignoring Cache (default is false)</param>
        /// <returns></returns>
        public T? GetById(string? id, bool reload = false)
            => GetByIdInternal(id, reload);

        /// <inheritdoc />
        T? IBasicList<T>.Add(T? item)
            => AddInternal(item) as T;

        internal override object? AddInternal(object? element)
        {
            if (element is T item && item.Id != null)
                return Cache.AddOrUpdate(item);

            return default(T);
        }


        /// <inheritdoc />
        T? IBasicList<T>.Remove(T? item)
            => RemoveInternal(item) as T;

        protected override object? RemoveInternal(object? element)
        {
            if (element is T item && Delete(item))
                return item;
            return default(T);
        }

        /// <inheritdoc />
        async Task<T?> Structures.ICollection<T>.RemoveAsync(string? id)
        {
            var item = await GetByIdAsync(id);
            if (Delete(item))
                return item;
            return null;
        }

        /// <inheritdoc />
        public bool Delete(T? item)
        {
            if (item == null) return false;
            if (item.Metadata_.IsNew) return item.Metadata_.IsTrash = true;

            item.Metadata_.SyncStatus = ItemSyncStatuses.ToBeDeleted;
            return true;
        }
    }
}
