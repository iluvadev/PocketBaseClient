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
        public abstract bool Contains(object? element);

        /// <inheritdoc />
        public abstract object? Add(object? element);

        /// <inheritdoc />
        public abstract object? Remove(object? element);


        /// <inheritdoc />
        bool IBasicList.SaveChanges(ListSaveDiscardModes mode)
            => SaveChanges(true);

        /// <inheritdoc />
        void IBasicList.DiscardChanges(ListSaveDiscardModes mode)
            => DiscardChanges();

    }
}
