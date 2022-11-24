namespace PocketBaseClient.Orm
{
    public class CollectionMetadata
    {
        public CollectionBase Collection { get; private init; }

        public bool IsLoaded => Count != null && Collection.CachedItemsCount >= Count;

        public int? Count { get; internal set; } = null;


        public CollectionMetadata(CollectionBase collection)
        {
            Collection = collection;
        }
    }
}
