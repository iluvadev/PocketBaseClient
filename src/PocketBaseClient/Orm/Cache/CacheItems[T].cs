namespace PocketBaseClient.Orm.Cache
{
    internal class CacheItems<T>
        where T : ItemBase
    {
        private class CachedItem
        {
            public DateTime DateCached { get; }
            public string Id { get; }
            public CachedItem(T item)
            {
                if (item.Id == null) throw new ArgumentException("Can not cache a non saved item");

                DateCached = DateTime.UtcNow;
                Id = item.Id;
            }
        }

        private List<CachedItem> CachedItems { get; } = new List<CachedItem>();
        private Dictionary<string, T> Items { get; } = new Dictionary<string, T>();

        public int Count => Items.Values.Where(i => i.IsLoaded()).Count();

        public T Add(T item)
        {
            if (item.Id == null) throw new ArgumentException("Can not cache a non valid item");

            if (!Items.ContainsKey(item.Id))
            {
                CachedItems.Add(new CachedItem(item));
                Items.Add(item.Id, item);
            }
            else
            {
                // Update Item cached properties
                if (item.IsLoaded())
                    Items[item.Id].UpdateWith(item);
            }

            return item;
        }

        public T? Get(string id)
        {
            if (!Items.ContainsKey(id)) return null;

            var item = Items[id];
            if (!item.IsLoaded()) return null;

            return item;
        }


    }
}
