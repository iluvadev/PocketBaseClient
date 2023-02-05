using PocketBaseClient.Orm.Structures;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
