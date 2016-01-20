using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAP.FileService.Domain
{
    /// <summary>
    ///
    /// </summary>
    public class FileMetadataEAVProperty
    {
        public string FileId { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }

    public class PropertyCollection : ICollection<FileMetadataEAVProperty>
    {
        private volatile Hashtable _entriesTable;
        private volatile FileMetadataEAVProperty _nullKeyEntry;
        private List<FileMetadataEAVProperty> _list;

        private bool _readOnly;
        private int _version;
        private IEqualityComparer _keyComparer;
        private static StringComparer defaultComparer = StringComparer.InvariantCultureIgnoreCase;

        private void Reset()
        {
            _readOnly = false;
            _entriesTable = new Hashtable();
            _nullKeyEntry = null;
            _list = new List<FileMetadataEAVProperty>();
            _version++;
        }

        public PropertyCollection()
            : this(defaultComparer)
        {
            Reset();
        }

        protected PropertyCollection(IEqualityComparer equalityComparer)
        {
            _keyComparer = (equalityComparer == null) ? defaultComparer : equalityComparer;
            Reset();
        }

        protected FileMetadataEAVProperty FindEntry(string name)
        {
            if (name != null)
            {
                return (FileMetadataEAVProperty)_entriesTable[name];
            }
            else
            {
                return _nullKeyEntry;
            }
        }

        protected void BaseAdd(string name, object value)
        {
            if (_readOnly)
                throw new NotSupportedException("只读集合不支持修改");

            FileMetadataEAVProperty entry = new FileMetadataEAVProperty
            {
                Name = name,
                Value = (value as FileMetadataEAVProperty).Value,
                FileId = (value as FileMetadataEAVProperty).FileId
            };

            // insert entry into hashtable
            if (name != null)
            {
                if (_entriesTable[name] == null)
                {
                    _entriesTable.Add(name, entry);
                }
                else
                {
                    throw new NotSupportedException("Name已存在");
                }
            }
            else
            { // null key -- special case -- hashtable doesn't like null keys
                if (_nullKeyEntry == null)
                {
                    _nullKeyEntry = entry;
                }
            }
            _list.Add(entry);
            _version++;
        }

        protected void BaseRemove(String name)
        {
            if (_readOnly)
                throw new NotSupportedException("只读集合不支持修改");

            if (name != null)
            {
                // remove from hashtable
                _entriesTable.Remove(name);

                // remove from array
                for (int i = _list.Count - 1; i >= 0; i--)
                {
                    if (_keyComparer.Equals(name, BaseGetKey(i)))
                        _list.RemoveAt(i);
                }
            }
            else
            { // null key -- special case
                // null out special 'null key' entry
                _nullKeyEntry = null;

                // remove from array
                for (int i = _list.Count - 1; i >= 0; i--)
                {
                    if (BaseGetKey(i) == null)
                        _list.RemoveAt(i);
                }
            }

            _version++;
        }

        protected void BaseRemoveAt(int index)
        {
            if (_readOnly)
                throw new NotSupportedException("只读集合不支持修改");

            String key = BaseGetKey(index);

            if (key != null)
            {
                // remove from hashtable
                _entriesTable.Remove(key);
            }
            else
            { // null key -- special case
                // null out special 'null key' entry
                _nullKeyEntry = null;
            }

            // remove from array
            _list.RemoveAt(index);

            _version++;
        }

        protected void BaseClear()
        {
            if (_readOnly)
                throw new NotSupportedException("只读集合不支持修改");

            Reset();
        }

        private bool HasKeys()
        {
            return (_entriesTable.Count > 0);  // any entries with keys?
        }

        protected Object BaseGet(string name)
        {
            FileMetadataEAVProperty e = FindEntry(name);

            return (e != null) ? e.Value : null;
        }

        protected void BaseSet(String name, Object value)
        {
            if (_readOnly)
                throw new NotSupportedException("只读集合不支持修改");

            FileMetadataEAVProperty entry = FindEntry(name);
            if (entry != null)
            {
                entry.Value = (String)value;
                _version++;
            }
            else
            {
                BaseAdd(name, value);
            }
        }

        protected Object BaseGet(int index)
        {
            FileMetadataEAVProperty entry = (FileMetadataEAVProperty)_list[index];
            return entry;
        }

        protected String BaseGetKey(int index)
        {
            FileMetadataEAVProperty entry = (FileMetadataEAVProperty)_list[index];
            return entry.Name;
        }

        protected void BaseSet(int index, Object value)
        {
            if (_readOnly)
                throw new NotSupportedException("只读集合不支持修改");

            FileMetadataEAVProperty entry = (FileMetadataEAVProperty)_list[index];
            entry.Value = (String)value;
            _version++;
        }

        protected String[] BaseGetAllKeys()
        {
            int n = _list.Count;
            String[] allKeys = new String[n];

            for (int i = 0; i < n; i++)
                allKeys[i] = BaseGetKey(i);

            return allKeys;
        }

        protected FileMetadataEAVProperty[] BaseGetAllValues()
        {
            int n = _list.Count;
            FileMetadataEAVProperty[] allValues = new FileMetadataEAVProperty[n];

            for (int i = 0; i < n; i++)
                allValues[i] = (FileMetadataEAVProperty)BaseGet(i);

            return allValues;
        }

        public object this[string key]
        {
            get
            {
                return BaseGet(key);
            }
            set
            {
                BaseSet(key, value);
            }
        }

        public void Add(FileMetadataEAVProperty item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("c");
            }

            if (item != null)
            {
                BaseAdd(item.Name, item);
            }
            else
            {
                BaseAdd(item.Name, null);
            }
        }

        public void Clear()
        {
            _entriesTable.Clear();
            _list.Clear();
        }

        public bool Contains(FileMetadataEAVProperty item)
        {
            return _entriesTable.Contains(item.Name);
        }

        public void CopyTo(FileMetadataEAVProperty[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }

            if (array.Rank != 1)
            {
                throw new ArgumentException("MultiRank");
            }

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException("index", "IndexOutOfRange");
            }

            if (array.Length - arrayIndex < _list.Count)
            {
                throw new ArgumentException("InsufficientSpace");
            }

            for (IEnumerator e = this.GetEnumerator(); e.MoveNext(); )
                array.SetValue(e.Current, arrayIndex++);
        }

        public int Count
        {
            get { return _list.Count; }
        }

        public bool IsReadOnly
        {
            get { return this._readOnly; }
        }

        public bool Remove(FileMetadataEAVProperty item)
        {
            BaseRemove(item.Name);
            return true;
        }

        public virtual IEnumerator<FileMetadataEAVProperty> GetEnumerator()
        {
            return new PropertyCollectionEnumerator(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator() as System.Collections.IEnumerator;
        }

        [Serializable]
        internal class PropertyCollectionEnumerator : IEnumerator<FileMetadataEAVProperty>, System.Collections.IEnumerator
        {
            private PropertyCollection _coll;
            private int _pos;
            private int _version;

            internal PropertyCollectionEnumerator(PropertyCollection coll)
            {
                _coll = coll;
                _version = coll._version;
                _pos = -1;
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (_version != _coll._version)
                    throw new InvalidOperationException("EnumFailedVersion");

                if (_pos < _coll.Count - 1)
                {
                    _pos++;
                    return true;
                }
                else
                {
                    _pos = _coll.Count;
                    return false;
                }
            }

            public void Reset()
            {
                if (_version != _coll._version)
                    throw new InvalidOperationException("EnumFailedVersion");
                _pos = -1;
            }

            public FileMetadataEAVProperty Current
            {
                get
                {
                    if (_pos >= 0 && _pos < _coll.Count)
                    {
                        return _coll.BaseGet(_pos) as FileMetadataEAVProperty;
                    }
                    else
                    {
                        throw new InvalidOperationException("EnumFailedVersion");
                    }
                }
            }

            Object System.Collections.IEnumerator.Current
            {
                get
                {
                    if (_pos >= 0 && _pos < _coll.Count)
                    {
                        return _coll.BaseGet(_pos);
                    }
                    else
                    {
                        throw new InvalidOperationException("EnumFailedVersion");
                    }
                }
            }
        }
    }
}