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
using System.Collections;

namespace PocketBaseClient.Orm
{
    public class QuerySortable<T> : IEnumerable<T>
    {
        internal FilterCommand? Filter { get; set; }
        internal SortCommand? Sort { get; set; }

        internal Func<string?, string?, IEnumerable<T>> FilterSortExecutor { get; set; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="filterSortExecutor"></param>
        public QuerySortable(FilterCommand? filter, Func<string?, string?, IEnumerable<T>> filterSortExecutor)
        {
            Filter = filter;
            FilterSortExecutor = filterSortExecutor;
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
            foreach (var item in FilterSortExecutor(Filter?.Command, Sort?.Command))
                yield return item;
        }

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator()
            => GetItems().GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}
