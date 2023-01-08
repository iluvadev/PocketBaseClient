// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

namespace PocketBaseClient.Orm
{
    /// <summary>
    /// Statuses of synchronization for an Item
    /// </summary>
    public enum ItemSyncStatuses
    {
        /// <summary> Unknown status </summary>
        Unknown = 0,

        /// <summary> The item is not Created in PocketBase </summary>
        ToBeCreated = 1,

        /// <summary> The item is Loaded (partially or not) from PocketBase </summary>
        Loaded = 2,

        /// <summary> The item is deleted but not yet in PocketBase </summary>
        ToBeDeleted = 3,
    }
}
