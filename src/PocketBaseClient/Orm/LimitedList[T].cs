using System.Collections;

namespace PocketBaseClient.Orm
{
    [Serializable]
    public class LimitedList<T> : IList<T>
    {
        private List<T> InnerList { get; }

        public int MaxSize { get; }
        public int Count => InnerList.Count;

        bool ICollection<T>.IsReadOnly => false;

        public LimitedList() : this(1000)
        {
        }

        public LimitedList(int maxsize)
        {
            InnerList = new List<T>(maxsize);
            MaxSize = maxsize;
        }

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
                if (index > MaxSize - 1)
                    throw new Exception($"Index {index} is out of range {MaxSize - 1}");
                InnerList[index] = value;
            }
        }

        public int IndexOf(T item)
            => InnerList.IndexOf(item);

        public void Insert(int index, T item)
            => this[index] = item;

        public void RemoveAt(int index)
            => InnerList.RemoveAt(index);


        IEnumerator<T> IEnumerable<T>.GetEnumerator()
            => InnerList.GetEnumerator();

        public IEnumerator GetEnumerator()
            => InnerList.GetEnumerator();

        public void Add(T value)
        {
            if (Count == MaxSize)
                throw new Exception($"List is full: limited to {MaxSize}");

            InnerList.Add(value);
        }

        public void Clear()
            => InnerList.Clear();

        public bool Contains(T item)
            => InnerList.Contains(item);

        public void CopyTo(T[] array, int arrayIndex)
            => InnerList.CopyTo(array, arrayIndex);

        public bool Remove(T item)
            => InnerList.Remove(item);

    }
}
