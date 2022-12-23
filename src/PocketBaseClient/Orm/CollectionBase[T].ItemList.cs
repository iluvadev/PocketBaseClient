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


        /// <inheritdoc />
        public T? GetById(string? id, bool reload = false) => GetByIdAsync(id, reload).Result;

        /// <summary>
        /// Gets the item, with its id (async)
        /// </summary>
        /// <param name="id">The id of the item to get</param>
        /// <param name="reload">True if is forced to reload from PocketBase ignoring Cache (default is false)</param>
        /// <returns></returns>
        public async Task<T?> GetByIdAsync(string? id, bool reload = false)
            => await GetByIdInternalAsync(id, reload);


        /// <inheritdoc />
        public override bool Contains(object? element)
            => Contains(element as T);

        /// <inheritdoc />
        public bool Contains(T? element)
            => GetById(element?.Id) != null;

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
        T? IItemList<T>.Remove(string? id)
        {
            var item = GetById(id);
            if (Delete(item))
                return item;
            return null;
        }


        /// <inheritdoc />
        public bool Delete(string? id)
            => Delete(GetById(id));

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
