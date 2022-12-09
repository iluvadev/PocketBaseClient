// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using PocketBaseClient.Orm;

namespace PocketBaseClient.Services
{
    public abstract class DataServiceBase : ServiceBase
    {
        public DataServiceBase(PocketBaseClientApplication app) : base(app) { }

        #region Collections
        private static Dictionary<Type, CollectionBase> RegisteredCollections { get; } = new Dictionary<Type, CollectionBase>();

        protected void RegisterCollection(Type type, CollectionBase collection) => RegisteredCollections[type] = collection;

        protected abstract void RegisterCollections();

        internal static T UpdateCached<T>(T item)
            where T : ItemBase, new()
            => GetCollection<T>()!.UpdateCached(item);

        public static CollectionBase<T>? GetCollection<T>()
            where T : ItemBase, new()
        {
            var type = typeof(T);
            if (!RegisteredCollections.ContainsKey(type))
                return null;
            return RegisteredCollections[type] as CollectionBase<T>;
        }
        public static CollectionBase? GetCollectionById(string? id)
        {
            if (id == null) return null;
            return RegisteredCollections.Values.FirstOrDefault(c => c.Id == id);
        }
        public static IEnumerable<CollectionBase> Collections => RegisteredCollections.Values;
        #endregion Collections

        #region Get Item
        public static T? GetById<T>(string id, bool forceLoad = false) where T : ItemBase, new()
            => GetCollection<T>()?.GetById(id, forceLoad);

        public static async Task<T?> GetByIdAsync<T>(string id, bool forceLoad = false) where T : ItemBase, new()
        {
            var collection = GetCollection<T>();
            if (collection == null) return null;

            return await collection.GetByIdAsync(id, forceLoad);
        }

        #endregion Get Item

        #region DiscardChanges
        public void DiscardChanges()
        {
            foreach (var collection in Collections)
                collection.DiscardChanges();
        }
        public void DiscardChanges(ItemBase item)
            => item.DiscardChanges();

        public void DiscardChanges(CollectionBase collection)
            => collection.DiscardChanges();

        #endregion DiscardChanges

        #region Save Item
        public bool Save(ItemBase item)
            => item.Save();

        public async Task<bool> SaveAsync(ItemBase item)
            => await item.SaveAsync();
        #endregion Save Item

        #region Delete Item
        public bool Delete(ItemBase item)
            => item.Delete();

        public async Task<bool> DeleteAsync(ItemBase item)
            => await item.DeleteAsync();
        #endregion Delete Item

    }
}
