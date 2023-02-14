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

namespace PocketBaseClient.Orm
{

    /// <summary>
    /// Class definition for a field of type File
    /// </summary>
    public class FieldFileBase : IOwnedByItem
    {
        internal ItemBase? Item { get; set; }
        /// <inheritdoc />
        ItemBase? IOwnedByItem.Owner { get => Item; set => Item = value; }

        /// <summary>
        /// The Max size of the file
        /// </summary>
        public virtual long? MaxSize { get; } = null;

        internal protected FileOrigins Origin { get; private set; } = FileOrigins.PocketBase;

        /// <summary>
        /// The Column name in the PocketBase field
        /// </summary>
        internal string? FieldName { get; private set; }

        /// <summary>
        /// The File Name
        /// </summary>
        public string? FileName { get; internal set; }

        public string? EntireFileName
        {
            get
            {
                if(Origin== FileOrigins.PocketBase)
                    return Item?.Collection.UrlFile()
            }
        }


        public bool HasChanges { get; private set; } = false;
        public bool IsEmpty => string.IsNullOrEmpty(FileName);


        private Func<string?, Task<Stream>>? _StreamGetterAsync = null;
        internal Func<string?, Task<Stream>> StreamGetterAsync
        {
            get => _StreamGetterAsync ?? (Origin == FileOrigins.PocketBase ? (thumb) => CollectionBase.GetFileStreamFromPbAsync(this, thumb) : (_) => Task.Run(() => Stream.Null));
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
            Item = owner;
            FieldName = fieldName;
        }

        #endregion Ctor

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
        /// Loads the Field with a local File
        /// </summary>
        /// <param name="localPathFile">The entire path of the local file</param>
        public void LoadFromLocalFile(string localPathFile)
        {
            Origin = FileOrigins.LocalSystem;
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
            Origin = FileOrigins.Unknown;
            _StreamGetterAsync = null;

            ((IOwnedByItem)this).NotifyModificationToOwner();
        }

        internal IFile GetSdkFile()
        {
            if (Origin == FileOrigins.PocketBase)
                return new StreamFile()
                {
                    FileName = FileName,
                    FieldName = FieldName,
                    Stream = Task.Run(async () => await GetStreamAsync()).GetAwaiter().GetResult(), //WARNING: Async to Sync conversion
                };
            if (Origin == FileOrigins.LocalSystem)
                return new FilepathFile()
                {
                    FileName = 
                };
        }
    }
}
