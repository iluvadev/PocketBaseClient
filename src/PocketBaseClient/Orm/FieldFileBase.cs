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
using System.Text.Json.Serialization;

namespace PocketBaseClient.Orm
{

    /// <summary>
    /// Class definition for a field of type File
    /// </summary>
    public class FieldFileBase : IFile, IOwnedByItem
    {
        /// <inheritdoc />
        public ItemBase? Owner { get; set; }

        /// <summary>
        /// The Column name in the PocketBase field
        /// </summary>
        internal string? FieldName { get; private set; }

        /// <summary>
        /// The File Name
        /// </summary>
        public string? FileName { get; internal set; }

        #region Metadata
        private FieldFileMetadata? _Metadata_ = null;
        /// <summary>
        /// The Metadata information about the object and mapping to PocketBase
        /// </summary>
        [JsonIgnore]
        public FieldFileMetadata Metadata_
        {
            get => _Metadata_ ??= new FieldFileMetadata(this);
            private set => _Metadata_ = value;
        }
        string? IFile.FieldName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        string? IFile.FileName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        #endregion Metadata


        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="fieldName"></param>
        public FieldFileBase(ItemBase? owner, string fieldName) 
        { 
            Owner = owner;
            FieldName = fieldName;
        }

        public Stream? GetStream()
        {
            throw new NotImplementedException();
        }
    }
}
