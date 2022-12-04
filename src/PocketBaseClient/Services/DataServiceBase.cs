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
        #endregion Collections

        //Mètodes simples per Get?

    }
}
