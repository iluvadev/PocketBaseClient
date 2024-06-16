// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase


namespace PocketBaseClient.Orm.Structures
{
    /// <summary>
    /// Class Definition for field types of Lists of Items
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FieldItemList<T> : FieldBasicList<T>, IFieldItemList<T>
        where T : ItemBase, new()
    {

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="propertyName"></param>
        /// <param name="propertyId"></param>
        /// <param name="maxSize"></param>
        public FieldItemList(ItemBase? owner, string propertyName, string propertyId, int? maxSize = null)

            : base(owner, propertyName, propertyId, maxSize)
        {
        }

        /// <inheritdoc />
        public T? GetById(string? id, bool reload = false)
        {
            if (id == null) return null;
            var item = InnerList.FirstOrDefault(i => i.Id == id);
            if (item == null) return null;

            if (reload) item.Metadata_.SetNeedBeLoaded();
            return item;
        }

        /// <inheritdoc />
        IEnumerable<T> IItemList<T>.GetItems(bool reload, GetItemsFilter include)
        {
            foreach (var item in this)
            {
                if (reload)
                    item.Metadata_.SetNeedBeLoaded();

                // Check if item must be returned
                if (item.Metadata_.MatchFilter(include))
                    yield return item;
            }

        }

        /// <inheritdoc />
        public bool Delete(T? item)
            => Remove(item)?.Delete() ?? false;

        public async IAsyncEnumerable<T> GetItemsAsync(bool reload = false, GetItemsFilter include = GetItemsFilter.Load | GetItemsFilter.New, CancellationToken cancellationToken = default)
        {
            foreach (var item in InnerList)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    yield break;
                }

                if (reload)
                {
                    item.Metadata_.SetNeedBeLoaded();
                }

                if (item.Metadata_.MatchFilter(include))
                {
                    await Task.Yield(); // Simulate asynchronous operation
                    yield return item;
                }
            }
        }
    }
}
