using PocketBaseClient.Orm.Filters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketBaseClient.Orm
{
    public class FilteredCollection<C, T> : IEnumerable<T>
        where C : CollectionBase<T>
        where T : ItemBase, new()
    {
        private FilterCommand Filter { get; set; }
        private C Collection { get; set; }

        public FilteredCollection(C collection, FilterCommand filter)
        {
            Collection = collection;
            Filter = filter;
        }

        private IEnumerable<T> GetItems()
        {
            foreach (var item in Collection.GetItemsFromPb(Filter.Command))
                yield return item;
        }

        public IEnumerator<T> GetEnumerator()
            => GetItems().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}
