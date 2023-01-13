// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using System.Collections;

namespace PocketBaseClient.Orm.Structures
{
    /// <summary>
    /// Class Definition for field types of Lists 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FieldBasicList<T> : IFieldBasicList<T>
    {
        /// <inheritdoc />
        public string? Name { get; }

        /// <inheritdoc />
        public string? Id { get; }

        /// <inheritdoc />
        public ItemBase? Owner { get; set; }

        /// <inheritdoc />
        public int? MaxSize { get; }

        protected List<T> InnerList { get; }

        /// <inheritdoc />
        public int Count => InnerList.Count;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="propertyName"></param>
        /// <param name="propertyId"></param>
        /// <param name="maxSize"></param>
        public FieldBasicList(ItemBase? owner, string propertyName, string propertyId, int? maxSize = null)
        {
            Owner = owner;
            Name = propertyName;
            Id = propertyId;
            MaxSize = maxSize;
            InnerList = new List<T>(maxSize ?? 512);
        }

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator()
            => InnerList.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
            => InnerList.GetEnumerator();

        /// <inheritdoc />
        public bool Contains(T? element)
            => element != null && InnerList.Contains(element);

        /// <inheritdoc />
        bool IBasicList.Contains(object? element)
            => false;

        /// <inheritdoc />
        public T? Add(T? element)
        {
            if (MaxSize != null && Count == MaxSize)
                throw new Exception($"List is full: limited to {MaxSize}");

            if (element == null) return default;

            InnerList.Add(element);
            ((IFieldBasicList<T>)this).NotifyModificationToOwner();
            return (element);
        }

        /// <inheritdoc />
        object? IBasicList.Add(object? element)
        {
            return element is T item ? Add(item) : default;
        }


        /// <inheritdoc />
        public T? Remove(T? element)
        {
            if (element == null) return default;

            bool bRet = InnerList.Remove(element);
            if (!bRet) return default;

            ((IFieldBasicList<T>)this).NotifyModificationToOwner();
            return element;
        }

        /// <inheritdoc />
        object? IBasicList.Remove(object? element)
        {
            return element is T item ? Remove(item) : default;
        }

        /// <inheritdoc />
        public void DiscardChanges(ListSaveDiscardModes mode)
        {
            //IEPA!!
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool SaveChanges(ListSaveDiscardModes mode)
        {
            //IEPA!!
            throw new NotImplementedException();
        }
    }
}
