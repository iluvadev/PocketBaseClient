// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using PocketBaseClient.Orm.Filters;

namespace PocketBaseClient.Orm
{
    /// <summary>
    /// Class that maps a Query to be executed over a Collection in PocketBase
    /// </summary>
    /// <typeparam name="C">The type of the collection</typeparam>
    /// <typeparam name="S">The type that defines the sorting options for the collection</typeparam>
    /// <typeparam name="T">The type mapped to registries in the collection</typeparam>
    public class CollectionQuery<C, S, T> : QuerySortable<T>
        where C : CollectionBase<T>
        where T : ItemBase, new()
        where S : ItemBaseSorts, new()
    {

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="filter"></param>
        public CollectionQuery(C collection, FilterCommand? filter)
            : base(filter, (f, s) => collection.GetItemsFromPb(f, s))
        {
        }
    }
}
