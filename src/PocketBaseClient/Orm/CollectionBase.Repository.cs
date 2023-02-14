using PocketBaseClient.Orm.Structures;
using System.Collections;
using System.Web;

namespace PocketBaseClient.Orm
{
    public partial class CollectionBase
    {
        #region Url
        internal string UrlRecords => $"/api/collections/{HttpUtility.UrlEncode(Id)}/records";
        internal string UrlRecord(string id) => $"{UrlRecords}/{HttpUtility.UrlEncode(id)}";
        internal string UrlFile(string recordId, string fileName) => $"/api/files/{HttpUtility.UrlEncode(Id)}/{HttpUtility.UrlEncode(recordId)}/{fileName}";
        internal string UrlFile(FieldFileBase file) => UrlFile(file.Item!.Id!, file.FileName!);
        #endregion  Url

        internal abstract IEnumerable GetCachedObjects();
        internal abstract IEnumerable GetObjects();

        internal abstract Task<bool> FillFromPbAsync<T>(T elem) where T : ItemBase;
        internal abstract bool FillFromPb<T>(T elem) where T : ItemBase;

        internal static async Task<Stream> GetFileStreamFromPbAsync(FieldFileBase file, string? thumb = null)
        {
            if (file == null) return Stream.Null;
            if (file.Item == null) return Stream.Null;
            if (file.FileName == null) return Stream.Null;
            if (file.Origin != FileOrigins.PocketBase) return Stream.Null;

            var collection = file.Item.Collection;
            var urlFile = collection.UrlFile(file);
            return await collection.App.Sdk.HttpGetStreamAsync(urlFile, thumb);
        }
        #region Cache
        internal abstract bool CacheContains<T>(T elem) where T : ItemBase;

        internal abstract bool ChangeIdInCache<T>(string oldId, T elem) where T : ItemBase;
        #endregion Cache

        #region  Save
        internal abstract Task<bool> SaveAsync<T>(T elem, bool onlyIfChanges = true) where T : ItemBase;
        internal abstract bool Save<T>(T elem, bool onlyIfChanges = true) where T : ItemBase;

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
        /// <summary>
        /// Save all Collection items to PocketBase, performing Create, Update or Delete for every Item to server
        /// </summary>
        /// <param name="onlyIfChanges">False to force saving unmodified items</param>
        /// <returns></returns>
        public bool SaveChanges(bool onlyIfChanges = true)
        {
            bool result = true;
            foreach (var cached in GetCachedObjects())
                if (cached is ItemBase item)
                    result &= Save(item, onlyIfChanges);

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
