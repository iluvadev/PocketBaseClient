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

        public T AddOrUpdate(T item)
        {
            if (item.Id == null) throw new ArgumentException("Can not cache a non valid item");

            T cachedItem;
            if (!Items.ContainsKey(item.Id))
            {
                CachedItems.Add(new CachedItem(item));
                Items.Add(item.Id, item);
                cachedItem = item;
            }
            else
            {
                cachedItem = Items[item.Id];
                //IEPA!!
                // Update Item cached properties if item has more recent data and cached item is not modified
                if (item.IsLoaded() && (!cachedItem.IsLoaded() || (cachedItem.Metadata.LastLoad ?? DateTime.MinValue) < (item.Metadata.LastLoad ?? DateTime.MinValue)))
                    cachedItem.UpdateWith(item);

                //if (!item.Metadata.IsNew)
                //    cachedItem.Metadata.MarkAsNotNew();
            }

            return cachedItem;
        }

        public T? Get(string id)
        {
            if (!Items.ContainsKey(id)) return null;

            var item = Items[id];
            if (!item.IsLoaded()) return null;

            return item;
        }

        public IEnumerable<T> AllItems => Items.Values;

    }
}
