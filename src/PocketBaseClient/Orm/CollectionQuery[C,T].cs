using PocketBaseClient.Orm.Filters;

namespace PocketBaseClient.Orm
{
    public class CollectionQuery<C, T>
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

        public async IAsyncEnumerable<T> GetItemsAsync()
        {
            await foreach (var item in Collection.GetItemsFromPbAsync(Filter.FilterString)) 
                yield return item;
        }
        public IEnumerable<T> GetItems()
        {
            foreach (var item in Collection.GetItemsFromPb(Filter.FilterString))
                yield return item;
        }

    }
}
