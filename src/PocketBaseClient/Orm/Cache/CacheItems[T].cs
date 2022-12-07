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

        public int Count => Items.Values.Where(i => !i.Metadata.IsTrash && i.Metadata.IsLoaded).Count();

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
                bool needToUpdate = cachedItem.Metadata.IsTrash;
                if (!item.Metadata.IsTrash && item.Metadata.IsLoaded)
                    needToUpdate |= !cachedItem.Metadata.IsLoaded ||
                                    cachedItem.Metadata.LastLoad! < item.Metadata.LastLoad!;
                if (needToUpdate)
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
            if (item.Metadata.IsTrash) return null;

            return item;
        }

        public T? Remove(string id)
        {
            if (!Items.ContainsKey(id)) return null;

            var item = Items[id];
            Items.Remove(id);
            return item;
        }

        public IEnumerable<T> AllItems => Items.Values;

    }
}
