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
using System.Text.Json.Serialization;

namespace PocketBaseClient.Orm.Structures
{

    /// <summary>
    /// Class definition for a field of type File
    /// </summary>
    public class FieldFile : IOwnedByItem
    {
        /// <inheritdoc />
        public ItemBase? Owner { get; set; }

        /// <summary>
        /// The File Name
        /// </summary>
        public string? FileName { get; set; }

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
        #endregion Metadata


        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="filename"></param>
        public FieldFile(string filename)
        {
            FileName = filename;
        }
    }
}
