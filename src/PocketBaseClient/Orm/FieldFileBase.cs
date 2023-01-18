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
    public class FieldFileBase : IFile, IOwnedByItem
    {
        private ItemBase? _Owner = null;
        /// <inheritdoc />
        ItemBase? IOwnedByItem.Owner { get => _Owner; set => _Owner = value; }

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


        internal protected bool IsFromServer { get; private set; }

        internal Func<Task<Stream>>? StreamGetterAsync { get; private set; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="owner"></param>
        protected internal FieldFileBase(string fieldName, ItemBase? owner)
        {
            IsFromServer = true;
            _Owner = owner;
            FieldName = fieldName;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="fileName"></param>
        /// <param name="streamGetter"></param>
        protected internal FieldFileBase(string fieldName, string fileName, Func<Stream> streamGetter)
            : this(fieldName, fileName, async () => await Task.Run(() => streamGetter())) { }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="fileName"></param>
        /// <param name="streamGetterAsync"></param>
        protected internal FieldFileBase(string fieldName, string fileName, Func<Task<Stream>> streamGetterAsync)
        {
            IsFromServer = false;
            FieldName = fieldName;
            FileName = fileName;
            StreamGetterAsync = streamGetterAsync;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="localPathFile"></param>
        protected internal FieldFileBase(string fieldName, string localPathFile)
            : this(fieldName, Path.GetFileName(localPathFile), () => new FileStream(localPathFile, FileMode.Open)) { }


        public async Task<Stream?> GetStreamAsync()
        {
            if (StreamGetterAsync != null)
                return await StreamGetterAsync.Invoke();
            return null;
        }

        public Stream? GetStream()
            => GetStreamAsync().Result;

        public void LoadFromFile(string localPathFile)
        {
            IsFromServer = false;
            StreamGetterAsync = async () => await Task.Run(() => new FileStream(localPathFile, FileMode.Open));
        }

        //etc

        /*
         * Mantenir info de si el fitxer és local o server
         *  Si és local, retornar Stream local
         *  Si és server, fer un GetStreamAsync de App
         *  
         *  A constructor: marcer que és local (quan no és local? -> Constructor buit)
         *  
         *  És local si té StreamGetter definit? (ctor buit és public)
         */
    }
}
