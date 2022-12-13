// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using System;
using System.Collections;

namespace PocketBaseClient.Orm
{
    public class LimitableList<T> : IList<T>, ILimitableList
    {
        public int? MaxSize { get; }

        private List<T> InnerList { get; }

        public ItemBase? Owner { get; set; }
        public int Count => InnerList.Count;

        bool ICollection<T>.IsReadOnly => false;

        #region Ctor
        public LimitableList() : this(null) { }
        public LimitableList(int maxSize): this(null, maxSize) { }
        public LimitableList(ItemBase? owner) : this(owner, null) { }
        public LimitableList(ItemBase? owner, int? maxSize)
        {
            InnerList = new List<T>(maxSize ?? 512);

            MaxSize = maxSize;
            Owner = owner;
        }
        #endregion Ctor

        public T this[int index]
        {
            get
            {
                if (index > Count - 1)
                    throw new Exception($"Index {index} is out of range {Count - 1}");
                return InnerList[index];
            }
            set
            {
                if (MaxSize != null && index > MaxSize - 1)
                    throw new Exception($"Index {index} is out of range {MaxSize - 1}");

                InnerList[index] = value;
                Owner?.SetModified();
            }
        }

        public int IndexOf(T item)
            => InnerList.IndexOf(item);

        public void Insert(int index, T item)
            => this[index] = item;

        public void RemoveAt(int index)
        {
            InnerList.RemoveAt(index);
            Owner?.SetModified();
        }


        IEnumerator<T> IEnumerable<T>.GetEnumerator()
            => InnerList.GetEnumerator();

        public IEnumerator GetEnumerator()
            => InnerList.GetEnumerator();

        public void Add(T value)
        {
            if (MaxSize != null && Count == MaxSize)
                throw new Exception($"List is full: limited to {MaxSize}");

            InnerList.Add(value);
            Owner?.SetModified();
        }

        public void Clear()
        {
            InnerList.Clear();
            Owner?.SetModified();
        }

        public bool Contains(T item)
            => InnerList.Contains(item);

        public void CopyTo(T[] array, int arrayIndex)
            => InnerList.CopyTo(array, arrayIndex);

        public bool Remove(T item)
        {
            bool bRet = InnerList.Remove(item);
            if (bRet)
                Owner?.SetModified();
            return bRet;
        }

        void ILimitableList.UpdateWith(ILimitableList? limitableList)
        {
            if (limitableList != null && limitableList is LimitableList<T> list)
            {
                // Remove old items
                for(int i=0; i<Count; i++)
                    InnerList.RemoveAt(i);

                // Add new items
                for (int i = 0; i < list.Count; i++)
                    InnerList.Add(list[i]);

                Owner?.SetModified();
            }
        }
    }
}
