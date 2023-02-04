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
    /// Definition for Lists of items that resides in Remote (PocketBase)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRemoteItemList<T> : IItemList<T>
         where T : ItemBase, new()
    {
        /// <summary>
        /// Gets the item of the list, with its id, downloading from Pocketbase if is not cached
        /// </summary>
        /// <param name="id">The id of the item to get</param>
        /// <param name="reload">True if is forced to reload from PocketBase (default is false)</param>
        /// <returns></returns>
        Task<T?> GetByIdAsync(string? id, bool reload = false);

        /// <summary>
        /// Says if the item is contained in the list
        /// </summary>
        /// <param name="id">The Id of the item to check if is contained</param>
        /// <returns></returns>
        public async Task<bool> ContainsAsync(string? id)
            => id != null && await GetByIdAsync(id) != null;

        /// <summary>
        /// Says if the item is contained in the list
        /// </summary>
        /// <param name="id">The Id of the item to check if is contained</param>
        /// <returns></returns>
        public async Task<bool> ContainsAsync(T? item)
            => await ContainsAsync(item?.Id);

        /// <summary>
        /// Removes the item from the list
        /// </summary>
        /// <param name="id">The Id of the item to be removed</param>
        /// <returns></returns>
        Task<T?> RemoveAsync(string? id);

        /// <summary>
        /// Deletes the item contained in the list from memory and PocketBase
        /// </summary>
        /// <param name="id">The id of the item to be deleted</param>
        /// <returns></returns>
        /// <remarks>Deleting an item removes it form the list and marks it as 'to be deleted' in PocketBase</remarks>
        public async Task<bool> DeleteAsync(string? id)
            => Delete(await GetByIdAsync(id));
    }
}
