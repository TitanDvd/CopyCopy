using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

namespace IpcCore
{
    public class ThreadSafeList<T> : IList<T>
    {
        private readonly List<T> _list;
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        private int _disposed;

        public ThreadSafeList()
        {
            _list = new List<T>();
        }

        public ThreadSafeList(int capacity)
        {
            _list = new List<T>(capacity);
        }

        public ThreadSafeList(IEnumerable<T> collection)
        {
            _list = new List<T>(collection);
        }

        private bool Disposed
        {
            get => Thread.VolatileRead(ref _disposed) == 1;
            set => Thread.VolatileWrite(ref _disposed, value ? 1 : 0);
        }

        public int Capacity
        {
            get
            {
                _lock.EnterReadLock();

                try
                {
                    return _list.Capacity;
                }
                finally
                {
                    _lock.ExitReadLock();
                }
            }
            set
            {
                _lock.EnterWriteLock();

                try
                {
                    _list.Capacity = value;
                }
                finally
                {
                    _lock.ExitWriteLock();
                }
            }
        }

        public IEnumerator GetEnumerator()
        {
            List<T> localList;

            _lock.EnterReadLock();

            try
            {
                localList = new List<T>(_list);
            }
            finally
            {
                _lock.ExitReadLock();
            }

            foreach (var item in localList)
                yield return item;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            List<T> localList;


            _lock.EnterReadLock();

            try
            {
                localList = new List<T>(_list);
            }
            finally
            {
                _lock.ExitReadLock();
            }

            foreach (var item in localList)
                yield return item;
        }

        public void Add(T item)
        {
            _lock.EnterWriteLock();

            try
            {
                _list.Add(item);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public void Clear()
        {
            _lock.EnterReadLock();

            try
            {
                _list.Clear();
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public bool Contains(T item)
        {
            _lock.EnterReadLock();

            try
            {
                return _list.Contains(item);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _lock.EnterReadLock();

            try
            {
                _list.CopyTo(array, arrayIndex);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public int Count
        {
            get
            {
                _lock.EnterReadLock();

                try
                {
                    return _list.Count;
                }
                finally
                {
                    _lock.ExitReadLock();
                }
            }
        }

        public int IndexOf(T item)
        {
            _lock.EnterReadLock();

            try
            {
                return _list.IndexOf(item);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public void Insert(int index, T item)
        {
            _lock.ExitWriteLock();

            try
            {
                _list.Insert(index, item);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public bool IsReadOnly => false;

        public bool Remove(T item)
        {
            _lock.EnterWriteLock();

            try
            {
                return _list.Remove(item);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public void RemoveAt(int index)
        {
            _lock.EnterWriteLock();

            try
            {
                _list.RemoveAt(index);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public T this[int index]
        {
            get
            {
                _lock.EnterReadLock();

                try
                {
                    return _list[index];
                }
                finally
                {
                    _lock.ExitReadLock();
                }
            }
            set
            {
                _lock.EnterWriteLock();

                try
                {
                    _list[index] = value;
                }
                finally
                {
                    _lock.ExitWriteLock();
                }
            }
        }

        public static implicit operator ThreadSafeList<T>(List<T> value)
        {
            return new ThreadSafeList<T>(value);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (Disposed) return;

            Disposed = true;
        }

        ~ThreadSafeList()
        {
            Dispose(false);
        }

        public void AddRange(IEnumerable<T> collection)
        {
            _lock.EnterWriteLock();

            try
            {
                _list.AddRange(collection);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public bool AddIfNotExist(T item)
        {
            _lock.EnterWriteLock();

            try
            {
                if (_list.Contains(item))
                    return false;

                _list.Add(item);
                return true;
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public ReadOnlyCollection<T> AsReadOnly()
        {
            _lock.EnterReadLock();

            try
            {
                return _list.AsReadOnly();
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public int BinarySearch(T item)
        {
            _lock.EnterReadLock();

            try
            {
                return _list.BinarySearch(item);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public int BinarySearch(T item, IComparer<T> comparer)
        {
            _lock.EnterReadLock();

            try
            {
                return _list.BinarySearch(item, comparer);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public int BinarySearch(int index, int count, T item, IComparer<T> comparer)
        {
            _lock.EnterReadLock();

            try
            {
                return _list.BinarySearch(index, count, item, comparer);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public List<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter)
        {
            _lock.EnterReadLock();

            try
            {
                return _list.ConvertAll(converter);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public bool Exists(Predicate<T> match)
        {
            _lock.EnterReadLock();

            try
            {
                return _list.Exists(match);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public T Find(Predicate<T> match)
        {
            _lock.EnterReadLock();

            try
            {
                return _list.Find(match);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public List<T> FindAll(Predicate<T> match)
        {
            _lock.EnterReadLock();

            try
            {
                return _list.FindAll(match);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public int FindIndex(Predicate<T> match)
        {
            _lock.EnterReadLock();

            try
            {
                return _list.FindIndex(match);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public int FindIndex(int startIndex, Predicate<T> match)
        {
            _lock.EnterReadLock();

            try
            {
                return _list.FindIndex(startIndex, match);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public int FindIndex(int startIndex, int count, Predicate<T> match)
        {
            _lock.EnterReadLock();

            try
            {
                return _list.FindIndex(startIndex, count, match);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public T FindLast(Predicate<T> match)
        {
            _lock.EnterReadLock();

            try
            {
                return _list.FindLast(match);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public int FindLastIndex(Predicate<T> match)
        {
            _lock.EnterReadLock();

            try
            {
                return _list.FindLastIndex(match);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public int FindLastIndex(int startIndex, Predicate<T> match)
        {
            _lock.EnterReadLock();

            try
            {
                return _list.FindLastIndex(startIndex, match);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public int FindLastIndex(int startIndex, int count, Predicate<T> match)
        {
            _lock.EnterReadLock();

            try
            {
                return _list.FindLastIndex(startIndex, count, match);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public void ForEach(Action<T> action)
        {
            _lock.EnterWriteLock();

            try
            {
                _list.ForEach(action);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public List<T> GetRange(int index, int count)
        {
            _lock.EnterReadLock();

            try
            {
                return _list.GetRange(index, count);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public int IndexOf(T item, int index)
        {
            _lock.EnterReadLock();

            try
            {
                return _list.IndexOf(item, index);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public int IndexOf(T item, int index, int count)
        {
            _lock.EnterReadLock();

            try
            {
                return _list.IndexOf(item, index, count);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public void InsertRange(int index, IEnumerable<T> range)
        {
            _lock.EnterWriteLock();

            try
            {
                _list.InsertRange(index, range);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public int LastIndexOf(T item)
        {
            _lock.EnterReadLock();

            try
            {
                return _list.LastIndexOf(item);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public int LastIndexOf(T item, int index)
        {
            _lock.EnterReadLock();

            try
            {
                return _list.LastIndexOf(item, index);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public int LastIndexOf(T item, int index, int count)
        {
            _lock.EnterReadLock();

            try
            {
                return _list.LastIndexOf(item, index, count);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public int RemoveAll(Predicate<T> match)
        {
            _lock.EnterWriteLock();

            try
            {
                return _list.RemoveAll(match);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public void RemoveRange(int index, int count)
        {
            _lock.EnterWriteLock();

            try
            {
                _list.RemoveRange(index, count);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public void Reverse()
        {
            _lock.EnterWriteLock();

            try
            {
                _list.Reverse();
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public void Reverse(int index, int count)
        {
            _lock.EnterWriteLock();

            try
            {
                _list.Reverse(index, count);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public void Sort()
        {
            _lock.EnterWriteLock();

            try
            {
                _list.Sort();
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public void Sort(Comparison<T> comparison)
        {
            _lock.EnterWriteLock();

            try
            {
                _list.Sort(comparison);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public void Sort(IComparer<T> comparer)
        {
            _lock.EnterWriteLock();

            try
            {
                _list.Sort(comparer);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public void Sort(int index, int count, IComparer<T> comparer)
        {
            _lock.EnterWriteLock();

            try
            {
                _list.Sort(index, count, comparer);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public T[] ToArray()
        {
            _lock.EnterReadLock();

            try
            {
                return _list.ToArray();
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public void TrimExcess()
        {
            _lock.EnterWriteLock();

            try
            {
                _list.TrimExcess();
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public bool TrueForAll(Predicate<T> match)
        {
            _lock.EnterWriteLock();

            try
            {
                return _list.TrueForAll(match);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }
    }
}