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
    /// Definition for field types of Lists 
    /// </summary>
    public interface IFieldBasicList<T> : IBasicList<T>, IOwnedByItem, ILimitable
    {
        /// <summary>
        /// The number of elements in the list
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Notifies a modification of the field to the Owner
        /// </summary>
        void NotifyModificationToOwner()
            //=> Owner?.SetPropertyModified(Name);
            => Owner?.SetModified();

        /// <summary>
        /// Says if the element is contained in the list
        /// </summary>
        /// <param name="element">The element to check if is contained</param>
        /// <returns></returns>
        bool Contains(T? element);
    }
}
