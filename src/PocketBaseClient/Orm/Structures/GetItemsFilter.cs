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
    /// <summary>
    /// Filter to apply in GetItems
    /// </summary>
    [Flags]
    public enum GetItemsFilter : short
    {
        /// <summary> No filter </summary>
        None = 0,

        /// <summary> Load items from PocketBase </summary>
        Load = 1,

        /// <summary> Created items not synced to PocketBase </summary>
        New = 2,

        /// <summary> Removed items not synced to PocketBase </summary>
        Erased = 4,
    }
}
