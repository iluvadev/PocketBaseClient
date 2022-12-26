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
    public class CollectionQuery<C, T>:IEnumerable<T>
        where C : CollectionBase<T>
        where T : ItemBase, new()
    {
        private FilterQuery Filter { get; set; }
        private C Collection { get; set; }

        public CollectionQuery(C collection, FilterQuery filter)
        {
            Collection = collection;
            Filter = filter;
        }

        private IEnumerable<T> GetItems()
        {
            foreach (var item in Collection.GetItemsFromPb(Filter.FilterString))
                yield return item;
        }

        public IEnumerator<T> GetEnumerator()
            => GetItems().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}
