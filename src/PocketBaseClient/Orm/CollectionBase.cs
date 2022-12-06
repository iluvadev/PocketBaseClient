using pocketbase_csharp_sdk;
using PocketBaseClient.Services;
using System.Web;

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

        internal async Task<bool> FillFromPbAsync<T>(T item)
            where T : ItemBase
        {
            if (item.Id == null) throw new Exception("Id must be informed to fill the object from PocketBase");

            // /api/collections/collectionIdOrName/records/recordId
            string url = $"/api/collections/{HttpUtility.UrlEncode(Name)}/records/{HttpUtility.UrlEncode(item.Id)}";

            var loadedItem = await PocketBase.HttpGetAsync<T>(url);
            if (loadedItem == null) return false;

            item.UpdateWith(loadedItem);
            item.Metadata.SetLoaded();
            return true;
        }

        internal abstract bool CacheContains<T>(T elem) where T : ItemBase;
    }
}
