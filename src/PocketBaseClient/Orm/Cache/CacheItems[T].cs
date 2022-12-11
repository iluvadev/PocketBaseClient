// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

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

        public int Count => Items.Values.Where(i => !i.Metadata_.IsTrash && i.Metadata_.IsLoaded).Count();

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

                // Update Item cached if item has more recent data and cached item is not modified
                bool needToUpdate = cachedItem.Metadata_.IsTrash;
                if (!item.Metadata_.IsTrash && item.Metadata_.IsLoaded)
                    needToUpdate |= !cachedItem.Metadata_.IsLoaded ||
                                    cachedItem.Metadata_.LastLoad! < item.Metadata_.LastLoad!;
                if (needToUpdate)
                    cachedItem.UpdateWith(item);
            }

            return cachedItem;
        }

        public T? Get(string id)
        {
            if (!Items.ContainsKey(id)) return null;

            var item = Items[id];
            if (item.Metadata_.IsTrash) return null;

            return item;
        }

        public T? Remove(string id)
        {
            if (!Items.ContainsKey(id)) return null;

            var item = Items[id];
            Items.Remove(id);
            return item;
        }

        public void RemoveTrash()
        {
            var trashIds = Items.Values.Where(i => i.Metadata_.IsTrash).Select(i => i.Id!).ToList();
            foreach (var id in trashIds)
                Remove(id);
        }

        public IEnumerable<T> AllItems => Items.Values;

        public IEnumerable<T> NewItems => Items.Values.Where(i => !i.Metadata_.IsTrash && i.Metadata_.IsNew);

        public IEnumerable<T> NotNewItems => Items.Values.Where(i => !i.Metadata_.IsTrash && !i.Metadata_.IsNew);

    }
}
