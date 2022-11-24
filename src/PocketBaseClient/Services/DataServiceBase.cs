using PocketBaseClient.Orm;

namespace PocketBaseClient.Services
{
    public abstract class DataServiceBase: ServiceBase
    {
        public DataServiceBase(PocketBaseClientApplication app): base(app) { }

        #region Collections
        private Dictionary<Type, CollectionBase> RegisteredCollections { get; } = new Dictionary<Type, CollectionBase>();

        protected void RegisterCollection(Type type, CollectionBase collection) => RegisteredCollections[type] = collection;

        protected abstract void RegisterCollections();

        public CollectionBase<T>? GetCollection<T>()
            where T : ItemBase
        {
            var type = typeof(T);
            if (!RegisteredCollections.ContainsKey(type))
                return null;
            return RegisteredCollections[type] as CollectionBase<T>;
        }
        #endregion Collections

        //Mètodes simples per Get?

    }
}
