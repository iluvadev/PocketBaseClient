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
    /// Definition for types that belongs to an ItemBase
    /// </summary>
    public interface IOwnedByItem
    {
        /// <summary>
        /// The Owner 
        /// </summary>
        ItemBase? Owner { get; set; }

        /// <summary>
        /// Notifies a modification of the field to the Owner
        /// </summary>
        void NotifyModificationToOwner()
            //=> Owner?.SetPropertyModified(Name);
            => Owner?.SetModified();
    }
}
