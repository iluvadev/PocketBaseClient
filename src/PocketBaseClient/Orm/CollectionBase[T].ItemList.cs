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
            => Contains(element?.Id);

        /// <inheritdoc />
        public override object? Add(object? item)
            => Add(item as T);
        
        /// <inheritdoc />
        public T? Add(T? item)
            => Cache.AddOrUpdate(item);

        /// <inheritdoc />
        public override object? Remove(object? element)
            => Remove(element as T);

        /// <inheritdoc />
        public T? Remove(T? item)
        {
            if (item == null) return null;
            if (!Delete(item)) return null;
            return item;
        }

        /// <inheritdoc />
        public T? Remove(string? id)
            => Remove(GetById(id));


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
