// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using PocketBaseClient.Services;

namespace PocketBaseClient.Orm
{
    /// <summary>
    /// Base class for a Collection of PocketBase, with registries mapped in the ORM
    /// </summary>
    /// <typeparam name="T">The type of the mapped registries</typeparam>
    public abstract partial class CollectionBase<T> : CollectionBase, Structures.ICollection<T>
        where T : ItemBase, new()
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="context"></param>
        public CollectionBase(DataServiceBase context) : base(context) { }


        #region DiscardChanges
        /// <inheritdoc />
        public override void DiscardChanges()
        {
            foreach (var item in Cache.AllItems)
                item.DiscardChanges();

            Cache.RemoveTrash();
        }
        /// <summary>
        /// Discards all changes not saved in PocketBase of the Item
        /// </summary>
        /// <param name="item"></param>
        public void DiscardChanges(T item)
            => item.DiscardChanges();
        #endregion DiscardChanges

        #region Save Item

        /// <summary>
        /// Save an item to PocketBase, performing a Create, Update or Delete to server (async)
        /// </summary>
        /// <param name="item">The item to be saved</param>
        /// <param name="onlyIfChanges">False to force saving unmodified items</param>
        /// <returns></returns>
        internal async Task<bool> SaveAsync(T item, bool onlyIfChanges = true)
            => await SaveInternalAsync(item, onlyIfChanges);

        internal override async Task<bool> SaveAsync<E>(E elem, bool onlyIfChanges = true)
        {
            if (elem is T item)
                return await SaveAsync(item, onlyIfChanges);
            return false;
        }
        /// <summary>
        /// Save an item to PocketBase, performing a Create, Update or Delete to server (async)
        /// </summary>
        /// <param name="item">The item to be saved</param>
        /// <param name="onlyIfChanges">False to force saving unmodified items</param>
        /// <returns></returns>
        internal bool Save(T item, bool onlyIfChanges = true)
            => SaveInternal(item, onlyIfChanges);

        internal override bool Save<E>(E elem, bool onlyIfChanges = true)
        {
            if (elem is T item)
                return Save(item, onlyIfChanges);
            return false;
        }

        #endregion Save Item

    }
}
