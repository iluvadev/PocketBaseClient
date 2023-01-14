﻿// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase


using System.Text.Json.Serialization;

namespace PocketBaseClient.Orm.Structures
{
    /// <summary>
    /// Metadata about a File
    /// </summary>
    public class FieldFileMetadata
    {
        /// <summary> The Field </summary>
        [JsonIgnore]
        internal FieldFile Field { get; private init; }


        internal FieldFileMetadata(FieldFile field)
        {
            Field = field;
        }
    }
}
