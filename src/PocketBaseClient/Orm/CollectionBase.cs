using pocketbase_csharp_sdk;
using PocketBaseClient.Services;

namespace PocketBaseClient.Orm
{
    public abstract class CollectionBase
    {
        private CollectionMetadata? _Metadata = null;
        public CollectionMetadata Metadata => _Metadata ??= new CollectionMetadata(this);

        internal abstract int CachedItemsCount { get; }

        public abstract string Id { get; }
        public abstract string Name { get; }
        public abstract bool System { get; }

        //public DateTime? Created { get; set; }
        //public DateTime? Updated { get; set; }
        //public string? ListRule { get; set; }
        //public string? ViewRule { get; set; }
        //public string? CreateRule { get; set; }
        //public string? UpdateRule { get; set; }
        //public string? DeleteRule { get; set; }
        //public IEnumerable<SchemaFieldModel>? Schema { get; set; }

        protected internal DataServiceBase Context { get; }
        protected PocketBase PocketBase => Context.App.Sdk;

        public CollectionBase(DataServiceBase context)
        {
            Context = context;
        }

    }
}
