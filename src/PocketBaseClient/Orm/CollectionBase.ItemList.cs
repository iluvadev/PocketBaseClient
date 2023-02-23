// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using PocketBaseClient.Orm.Structures;
using System.Collections;

namespace PocketBaseClient.Orm
{
    public partial class CollectionBase
    {
        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
            => GetObjects().GetEnumerator();


        /// <inheritdoc />
        object? IBasicList.Add(object? element)
            => AddInternal(element);
        internal abstract object? AddInternal(object? element);

        /// <inheritdoc />
        object? IBasicList.Remove(object? element)
            => RemoveInternal(element);
        protected abstract object? RemoveInternal(object? element);


        /// <inheritdoc />
        async Task<bool> IBasicCollection.SaveChangesAsync(ListSaveDiscardModes mode)
            => await SaveChangesAsync(true);

        /// <inheritdoc />
        bool IBasicCollection.SaveChanges(ListSaveDiscardModes mode)
            => SaveChanges(true);

        /// <inheritdoc />
        void IBasicCollection.DiscardChanges(ListSaveDiscardModes mode)
            => DiscardChanges();

    }
}
