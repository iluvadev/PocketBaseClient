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
    /// <summary>
    /// Base class for Data access encapsulation
    /// </summary>
    public abstract class DataServiceBase : ServiceBase
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="app"></param>
        public DataServiceBase(PocketBaseClientApplication app) : base(app) { }

        #region Collections
        private static Dictionary<Type, CollectionBase> RegisteredCollections { get; } = new Dictionary<Type, CollectionBase>();

        protected void RegisterCollection(Type type, CollectionBase collection) => RegisteredCollections[type] = collection;

        protected abstract void RegisterCollections();

        /// <summary>
        /// Gets the Collection for specified Type
        /// </summary>
        /// <typeparam name="T">The type of elemens in the Collection</typeparam>
        /// <returns></returns>
        public static CollectionBase<T>? GetCollection<T>()
            where T : ItemBase, new()
        {
            var type = typeof(T);
            if (!RegisteredCollections.ContainsKey(type))
                return null;
            return RegisteredCollections[type] as CollectionBase<T>;
        }
        /// <summary>
        /// Gets the collection by its Id
        /// </summary>
        /// <param name="id">The Id of the Collection</param>
        /// <returns></returns>
        public static CollectionBase? GetCollectionById(string? id)
        {
            if (id == null) return null;
            return RegisteredCollections.Values.FirstOrDefault(c => c.Id == id);
        }
        /// <summary>
        /// Gets all Collections
        /// </summary>
        public static IEnumerable<CollectionBase> Collections => RegisteredCollections.Values;
        #endregion Collections

        #region Get Item
        /// <summary>
        /// Gets the element with specified Id (async)
        /// </summary>
        /// <typeparam name="T">The type of the element to find</typeparam>
        /// <param name="id">The id of the element</param>
        /// <param name="forceLoad">True for a reload the element from server</param>
        /// <returns></returns>
        public static async Task<T?> GetByIdAsync<T>(string id, bool forceLoad = false) where T : ItemBase, new()
        {
            var collection = GetCollection<T>();
            if (collection == null) return null;

            return await collection.GetByIdAsync(id, forceLoad);
        }
        #endregion Get Item

        #region DiscardChanges
        /// <summary>
        /// Discard all Changes in all Collections
        /// </summary>
        public void DiscardChanges()
        {
            foreach (var collection in Collections)
                collection.DiscardChanges();
        }
        #endregion DiscardChanges

        #region SaveChanges
        /// <summary>
        /// Save all changed items to PocketBase, performing Create, Update or Delete for every Item changed to server (async)
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SaveChangesAsync()
        {
            bool bRet = true;
            foreach (var collection in Collections)
                bRet &= await collection.SaveChangesAsync();

            return bRet;
        }
        #endregion SaveChanges

    }
}
