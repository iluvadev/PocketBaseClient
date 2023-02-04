// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

namespace PocketBaseClient.Orm.Structures
{
    /// <summary>
    /// Definition for Lists of items that resides in Local (memory)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ILocalItemList<T> : IItemList<T>
         where T : ItemBase, new()
    {
        /// <summary>
        /// Gets the item of the list, with its id
        /// </summary>
        /// <param name="id">The id of the item to get</param>
        /// <param name="reload">True if is forced to reload from PocketBase (default is false)</param>
        /// <returns></returns>
        T? GetById(string? id, bool reload = false);

        /// <summary>
        /// Gets the element of the list with specified Id
        /// </summary>
        /// <param name="id">The id of the element</param>
        /// <returns>The element with Id (null if not exists in the list)</returns>
        public T? this[string id]
            => GetById(id);

        /// <summary>
        /// Says if the item is contained in the list
        /// </summary>
        /// <param name="id">The Id of the item to check if is contained</param>
        /// <returns></returns>
        public bool Contains(string? id)
            => id != null && GetById(id) != null;

        /// <summary>
        /// Removes the item from the list
        /// </summary>
        /// <param name="id">The Id of the item to be removed</param>
        /// <returns></returns>
        public T? Remove(string? id)
            => Remove(GetById(id));

        /// <summary>
        /// Deletes the item contained in the list from memory and PocketBase
        /// </summary>
        /// <param name="id">The id of the item to be deleted</param>
        /// <returns></returns>
        /// <remarks>Deleting an item removes it form the list and marks it as 'to be deleted' in PocketBase</remarks>
        public bool Delete(string? id)
            => Delete(GetById(id));

    }
}
