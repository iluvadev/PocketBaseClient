using System.Collections;
using System.Runtime.InteropServices;

namespace PocketBaseClient.Orm
{
    [Serializable]
    public class LimitedList<T> : IEnumerable<T>
    {
        private T[] _thing;
        public LimitedList(int maxsize)
        {
            _thing = new T[maxsize];
            MaxSize = maxsize;
        }
        private int MaxSize { get; }
        public int Count { get; private set; }
        public T this[int index]
        {
            get
            {
                if (index > _thing.Length - 1)
                    throw new Exception($"Index {index} is out of range {_thing.Length - 1}");
                return _thing[index];
            }
            set
            {
                if (index > _thing.Length - 1)
                    throw new Exception($"Index {index} is out of range {_thing.Length - 1}");
                if (index < MaxSize)
                {
                    _thing[index] = value;
                    Count++;
                    if (Count > MaxSize)
                        Count = MaxSize;
                }
            }
        }
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new Enumerator<T>(this);
        }
        public IEnumerator GetEnumerator()
        {
            return new Enumerator<T>(this);
        }
        public void Add(T value)
        {
            if (Count == MaxSize)
            {
                LeftShift();
                _thing[MaxSize - 1] = value;
            }
            else
            {
                _thing[Count] = value;
                Count++;
            }
        }
        public void LeftShift()
        {
            var TArray = new T[MaxSize];
            Array.Copy(_thing, 1, TArray, 0, MaxSize - 1);
            _thing = TArray;
        }
        public void Clear()
        {
            _thing = new T[MaxSize];
            Count = 0;
        }
        public bool Contains(T item)
        {
            var size = MaxSize;
            var equalityComparer = EqualityComparer<T>.Default;
            while (size-- > 0)
                if (item == null)
                {
                    if (_thing[size] == null)
                        return true;
                }
                else if (_thing[size] != null && equalityComparer.Equals(_thing[size], item))
                {
                    return true;
                }
            return false;
        }
        public T[] ToArray()
        {
            var objArray = new T[MaxSize];
            Array.Copy(_thing, 0, objArray, 0, MaxSize);
            return objArray;
        }
        public List<T> ToList()
        {
            return new List<T>(_thing);
        }
        public HashSet<T> ToHashSet()
        {
            return new HashSet<T>(_thing);
        }
        public void CopyTo(T[] array)
        {
            Array.Copy(_thing, 0, array, 0, MaxSize);
        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public struct Enumerator<K> : IEnumerator<K>
        {
            private readonly LimitedList<K> thing;
            private int index;
            internal Enumerator(LimitedList<K> thing)
            {
                this.thing = thing;
                index = 0;
                Current = default;
            }
            public void Dispose()
            {
            }
            public bool MoveNext()
            {
                var tthing = thing;
                if (index < tthing.MaxSize)
                {
                    Current = tthing._thing[index];
                    index++;
                    return true;
                }
                index = thing.MaxSize + 1;
                Current = default;
                return false;
            }
            public K? Current { get; private set; }
            object? IEnumerator.Current => Current;
            void IEnumerator.Reset()
            {
                index = 0;
                Current = default;
            }
        }
    }
}
