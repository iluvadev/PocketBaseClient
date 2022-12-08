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

        internal abstract Task<bool> FillFromPbAsync<T>(T elem) where T : ItemBase;

        internal abstract bool CacheContains<T>(T elem) where T : ItemBase;

        #region DiscardChanges
        public abstract void DiscardChanges();
        #endregion DiscardChanges

        #region  Save Item
        internal abstract Task<bool> SaveAsync<T>(T elem, bool onlyIfChanges = false) where T : ItemBase;
        #endregion  Save Item

        #region Delete Item
        internal abstract Task<bool> DeleteAsync<T>(T elem) where T : ItemBase;

        #endregion Delete Item

    }
}
