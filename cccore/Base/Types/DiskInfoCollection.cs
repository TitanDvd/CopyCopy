using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CcCore.Base.Types;

namespace CcCore.Base.Types
{
    public class DiskInfoCollection : IList<DiskInfo>
    {
        private List<DiskInfo> _iccdl;
        public DiskInfo this[int index] { get => _iccdl[index]; set => _iccdl[index] = value; }

        public int Count => _iccdl.Count;

        public bool IsReadOnly => false;

        public DiskInfoCollection() { _iccdl = new List<DiskInfo>(); }


        public void Add(DiskInfo item)
        {
            lock (_iccdl)
                _iccdl.Add(item);
        }

        public void AddRange(IEnumerable<DiskInfo> items)
        {
            lock (_iccdl)
                _iccdl.AddRange(items);
        }

        public void Clear()
        {
            lock (_iccdl)
                _iccdl.Clear();
        }

        public bool Contains(DiskInfo item)
        {
            lock (_iccdl)
                return _iccdl.FirstOrDefault(i => i.Letter == item.Letter) != null ? true : false;
        }

        public void CopyTo(DiskInfo[] array, int arrayIndex)
        {
            lock (_iccdl)
                _iccdl.CopyTo(array, arrayIndex);
        }

        public IEnumerator<DiskInfo> GetEnumerator()
        {
            lock (_iccdl)
                return _iccdl.GetEnumerator();
        }

        public int IndexOf(DiskInfo item)
        {
            lock (_iccdl)
                return _iccdl.FindIndex(id=>id.Letter == item.Letter);
        }

        public void Insert(int index, DiskInfo item)
        {
            lock (_iccdl)
                _iccdl.Insert(index, item);
        }

        public bool Remove(DiskInfo item)
        {
            lock (_iccdl)
            {
                int idx = _iccdl.FindIndex(i => i.Letter == item.Letter);
                if(idx != -1)
                    _iccdl.RemoveAt(idx);
                return !_iccdl.Contains(item);
            }
                
        }

        public void RemoveAt(int index)
        {
            lock (_iccdl)
                _iccdl.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            lock (_iccdl)
                return _iccdl.GetEnumerator();
        }

        internal DiskInfo FirstOrDefault(Func<DiskInfo, bool> p)
        {
            return _iccdl.FirstOrDefault(p);
        }
    }
}
