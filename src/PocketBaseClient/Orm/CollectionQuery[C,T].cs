// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using pocketbase_csharp_sdk.Models;
using PocketBaseClient.Orm.Filters;
using PocketBaseClient.Orm.Structures;
using System.Collections;
using System.Runtime.CompilerServices;

namespace PocketBaseClient.Orm
{
    /// <summary>
    /// Class that maps a Query to be executed over a Collection in PocketBase
    /// </summary>
    /// <typeparam name="C">The type of the collection</typeparam>
    /// <typeparam name="S">The type that defines the sorting options for the collection</typeparam>
    /// <typeparam name="T">The type mapped to registries in the collection</typeparam>
    public class CollectionQuery<C, S, T> : IEnumerable<T>, IAsyncEnumerable<T>
        where C : CollectionBase<T>
        where T : ItemBase, new()
        where S : ItemBaseSorts, new()
    {
        internal FilterCommand? Filter { get; set; }
        internal SortCommand? Sort { get; set; }
        internal C Collection { get; set; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="filter"></param>
        public CollectionQuery(C collection, FilterCommand? filter)
        {
            Collection = collection;
            Filter = filter;
        }

        /// <summary>
        /// Sorts the Filter in server by the selector
        /// </summary>
        /// <param name="commandSelector">Selector for the sort</param>
        /// <returns></returns>
        public IEnumerable<T> SortBy(Func<S, SortCommand> commandSelector)
        {
            Sort = commandSelector.Invoke(new());
            return this;
        }

        private IEnumerable<T> GetItems()
        {
            foreach (var item in Collection.GetItemsFromPb(Filter?.Command, Sort?.Command))
                yield return item;
        }
        
        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator()
            => GetItems().GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        private async IAsyncEnumerable<T> GetItemsAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            await foreach (var item in Collection.GetItemsFromPbAsync(Filter?.Command, Sort?.Command).WithCancellation(cancellationToken))
            {
                yield return item;
            }
        }
        /// <inheritdoc />
        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
            => GetItemsAsync(cancellationToken).GetAsyncEnumerator();


        /// <summary>
        /// Gets a page of items from PocketBase.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="perPage">The number of items per page.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a PagedCollectionModel of items.</returns>
        public Task<PagedCollectionModel<T>> GetPagedItemsAsync(int pageNumber, int perPage) => Collection.GetPagedItemsAsync(pageNumber, perPage, Filter?.Command, Sort?.Command);
        

        /// <summary>
        /// Gets a page of items from PocketBase.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="perPage">The number of items per page.</param>
        /// <returns>A PagedCollectionModel of items.</returns>
        public PagedCollectionModel<T> GetPagedItems(int pageNumber, int perPage) => Collection.GetPagedItems(pageNumber, perPage, Filter?.Command, Sort?.Command);


        /// <summary>
        /// Asynchronously retrieves the full list of items from the Collection.
        /// This method returns a Task representing an asynchronous operation that, when completed, 
        /// contains an IEnumerable of items of type T.
        /// </summary>
        /// <param name="reload">If true, forces reload of all cached items. Default is false.</param>
        /// <param name="include">Filter to determine which items to include in the returned list. Default is Load and New.</param>
        /// <returns>A Task that represents the asynchronous operation, containing an IEnumerable of items of type T.</returns>
        public Task<IEnumerable<T>> GetFullListAsync(bool reload = false, GetItemsFilter include = GetItemsFilter.Load | GetItemsFilter.New) => Collection.GetFullListAsync(reload, include);

        /// <summary>
        /// Synchronously retrieves the full list of items from the Collection.
        /// This method directly returns an IEnumerable of items of type T.
        /// </summary>
        /// <param name="reload">If true, forces reload of all cached items. Default is false.</param>
        /// <param name="include">Filter to determine which items to include in the returned list. Default is Load and New.</param>
        /// <returns>An IEnumerable of items of type T.</returns>
        public IEnumerable<T> GetFullList(bool reload = false, GetItemsFilter include = GetItemsFilter.Load | GetItemsFilter.New) => Collection.GetFullList(reload, include);


    }
}
