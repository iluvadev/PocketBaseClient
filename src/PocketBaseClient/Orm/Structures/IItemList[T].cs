// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using System.Runtime.CompilerServices;

namespace PocketBaseClient.Orm.Structures
{
    /// <summary>
    /// Represents a list of items mapped to PocketBase registries
    /// </summary>
    /// <typeparam name="T">The type mapped to the PocketBase registry</typeparam>
    public interface IItemList<T> : IBasicList<T>
        where T : ItemBase, new()
    {
        /// <summary>
        /// Gets all items in the list
        /// </summary>
        /// <param name="reload">True: the elements must be reloaded from PocketBase</param>
        /// <param name="include">Indicates what elements must be returned (default: loaded and new ones)</param>
        /// <returns></returns>
        IEnumerable<T> GetItems(bool reload = false, GetItemsFilter include = GetItemsFilter.Load | GetItemsFilter.New);

        /// <summary>
        /// Gets all ids of the items in the list
        /// </summary>
        /// <param name="reload"></param>
        /// <param name="include">Indicates what elements must be returned (default: loaded and new ones)</param>
        /// <returns></returns>
        IEnumerable<string> GetItemIds(bool reload = false, GetItemsFilter include = GetItemsFilter.Load | GetItemsFilter.New)
            => GetItems(reload, include).Select(i => i.Id!);

        /// <summary>
        /// Adds a new item to the list
        /// </summary>
        /// <returns></returns>
        public T AddNew() 
            => Add(new T())!;

        /// <summary>
        /// Deletes the item contained in the list from memory and PocketBase
        /// </summary>
        /// <param name="item">The item to be deleted</param>
        /// <returns></returns>
        /// <remarks>Deleting an item removes it form the list and marks it as 'to be deleted' in PocketBase</remarks>
        bool Delete(T? item);

        /// <summary>
        /// Deletes all items in the list
        /// </summary>
        /// <returns></returns>
        /// <remarks>Deleting an item removes it form the list and marks it as 'to be deleted' in PocketBase</remarks>
        bool DeleteAll()
        {
            bool result = true;
            foreach (var item in this)
                result &= Delete(item);

            return result;
        }
    }
}
