using System.Collections;

namespace PocketBaseClient.Orm
{
    public partial class CollectionBase
    {
        internal abstract IEnumerable GetCachedObjects();
        internal abstract IEnumerable GetObjects();

        internal abstract Task<bool> FillFromPbAsync<T>(T elem) where T : ItemBase;

        #region Cache
        internal abstract bool CacheContains<T>(T elem) where T : ItemBase;

        internal abstract bool ChangeIdInCache<T>(string oldId, T elem) where T : ItemBase;
        #endregion Cache

        #region  Save
        internal abstract Task<bool> SaveAsync<T>(T elem, bool onlyIfChanges = true) where T : ItemBase;
        /// <summary>
        /// Save all Collection items to PocketBase, performing Create, Update or Delete for every Item to server (async)
        /// </summary>
        /// <param name="onlyIfChanges">False to force saving unmodified items</param>
        /// <returns></returns>
        public async Task<bool> SaveChangesAsync(bool onlyIfChanges = true)
        {
            bool result = true;
            foreach (var cached in GetCachedObjects())
                if (cached is ItemBase item)
                    result &= await SaveAsync(item, onlyIfChanges);

            return result;
        }

        #endregion  Save

        #region DiscardChanges
        /// <summary>
        /// Discards all changes not saved in PocketBase of all Items managed by the collection
        /// </summary>
        public abstract void DiscardChanges();
        #endregion DiscardChanges
    }
}
