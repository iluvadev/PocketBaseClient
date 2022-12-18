// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

namespace PocketBaseClient.Orm.Structures
{
    public enum ListSaveDiscardModes
    {
        /// <summary>
        /// Save or Discard only changes in the list references
        /// </summary>
        OnlyListChanges = 0,

        /// <summary>
        /// Save or Discard only changes in the objects of the list
        /// </summary>
        OnlyItemsChanges = 1,

        /// <summary>
        /// Save or Discard all changes: list references and referenced objects
        /// </summary>
        AllChanges = 2
    }
}
