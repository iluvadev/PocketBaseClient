// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using pocketbase_csharp_sdk;
using PocketBaseClient.Services;

namespace PocketBaseClient.Orm
{
    public abstract class CollectionBase
    {
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

        protected DataServiceBase Context { get; }
        protected PocketBase PocketBase => Context.App.Sdk;

        public CollectionBase(DataServiceBase context)
        {
            Context = context;
        }

        internal abstract Task<bool> FillFromPbAsync<T>(T elem) where T : ItemBase;

        internal abstract bool CacheContains<T>(T elem) where T : ItemBase;

        internal abstract bool AddToCache<T>(T elem) where T : ItemBase;

        internal abstract bool ChangeIdInCache<T>(string oldId, T elem) where T : ItemBase;

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
