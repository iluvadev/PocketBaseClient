// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using pocketbase_csharp_sdk.Models.Files;
using PocketBaseClient.Orm.Structures;
using System.IO;
using System.Web;

namespace PocketBaseClient.Orm
{

    /// <summary>
    /// Class definition for a field of type File
    /// </summary>
    public class FieldFileBase : IFile, IOwnedByItem
    {
        private ItemBase? _Owner = null;
        /// <inheritdoc />
        ItemBase? IOwnedByItem.Owner { get => _Owner; set => _Owner = value; }

        /// <summary>
        /// The Max size of the file
        /// </summary>
        public virtual long? MaxSize { get; } = null;

        internal string? UrlFile
        {
            get
            {
                if (_Owner == null) return null;
                if (FileName == null) return null;
                return $"/api/files/{HttpUtility.UrlEncode(_Owner.CollectionId)}/{HttpUtility.UrlEncode(_Owner.Id)}/{FileName}";
            }
        }

        /// <summary>
        /// The Column name in the PocketBase field
        /// </summary>
        internal string? FieldName { get; private set; }
        string? IFile.FieldName { get => FieldName; set => FieldName = value; }

        /// <summary>
        /// The File Name
        /// </summary>
        public string? FileName { get; internal set; }
        string? IFile.FileName { get => FileName; set => FileName = value; }

        internal protected bool IsFromServer { get; private set; } = true;

        public bool HasChanges { get; private set; } = false;
        public bool IsEmpty => string.IsNullOrEmpty(FileName);

        private Func<string?, Task<Stream>>? _StreamGetterAsync = null;
        internal Func<string?, Task<Stream>> StreamGetterAsync
        {
            get => _StreamGetterAsync ?? (IsFromServer ? (thumb) => GetStreamFromPbAsync(thumb) : (_) => Task.Run(() => Stream.Null));
            private set => _StreamGetterAsync = value;
        }

        #region Ctor
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="owner"></param>
        protected FieldFileBase(string fieldName, ItemBase? owner)
        {
            _Owner = owner;
            FieldName = fieldName;
            //IsFromServer = false;
        }

        #endregion Ctor

        /// <summary>
        /// Gets the Stream of the file from PocketBase server
        /// </summary>
        /// <param name="thumb">The optional Thumb options to ask for</param>
        /// <returns></returns>
        internal async Task<Stream> GetStreamFromPbAsync(string? thumb = null)
        {
            var urlFile = UrlFile;
            if (_Owner == null || urlFile == null)
                return Stream.Null;

            return await _Owner.Collection.App.Sdk.HttpGetStreamAsync(urlFile, thumb);
        }

        /// <summary>
        /// Gets the Stream of the File with thumb option (async)
        /// </summary>
        /// <param name="thumb">The optional Thumb options to ask for</param>
        /// <returns></returns>
        internal async Task<Stream> GetStreamAsync(string? thumb)
            => await StreamGetterAsync.Invoke(thumb);

        /// <summary>
        /// Gets the Stream of the File (async)
        /// </summary>
        /// <returns></returns>
        public async Task<Stream> GetStreamAsync()
            => await GetStreamAsync(null);

        /// <summary>
        /// Gets the Stream of the File 
        /// </summary>
        /// <returns></returns>
        Stream IFile.GetStream()
            => Task.Run(async () => await GetStreamAsync()).GetAwaiter().GetResult(); //WARNING: Async to Sync conversion


        /// <summary>
        /// Loads the Field with a local File
        /// </summary>
        /// <param name="localPathFile">The entire path of the local file</param>
        public void LoadFromLocalFile(string localPathFile)
        {
            IsFromServer = false;
            HasChanges = true;
            FileName = Path.GetFileName(localPathFile);
            StreamGetterAsync = (_) => Task.Run(() => new FileStream(localPathFile, FileMode.Open) as Stream);

            ((IOwnedByItem)this).NotifyModificationToOwner();
        }


        /// <summary>
        /// Saves the remote file to local file (async)
        /// </summary>
        /// <param name="localPathFile"></param>
        /// <param name="thumb"></param>
        /// <returns></returns>
        internal async Task SaveToLocalFileAsync(string localPathFile, string? thumb)
        {
            using (var localFileStream = File.Create(localPathFile))
                using (var remoteFileStream = await GetStreamAsync(thumb))
                    await remoteFileStream.CopyToAsync(localFileStream);
        }

        /// <summary>
        /// Saves the remote file to local file (async)
        /// </summary>
        /// <param name="localPathFile"></param>
        /// <returns></returns>
        public async Task SaveToLocalFileAsync(string localPathFile)
            => await SaveToLocalFileAsync(localPathFile, null);


        public void Remove()
        {
            HasChanges |= !IsEmpty;
            FileName = null;
            IsFromServer = false;
            _StreamGetterAsync = null;

            ((IOwnedByItem)this).NotifyModificationToOwner();
        }


        /*
         * IEPA!!
         * Fer que sigui No nullable i set privat
         * Afegir mètode/propietat:
         *      IsEmpty
         *      Remove()
         *      
         * Fer constructor dels mapejos internal
         */ 
    }
}
