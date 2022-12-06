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


        public T? GetById<T>(string id, bool forceLoad = false) where T : ItemBase, new()
            => GetCollection<T>()?.GetById(id, forceLoad);

        public async Task<T?> GetByIdAsync<T>(string id, bool forceLoad = false) where T : ItemBase, new()
        {
            var collection = GetCollection<T>();
            if(collection == null) return null;

            return await collection.GetByIdAsync(id, forceLoad);
        }
    }
}
