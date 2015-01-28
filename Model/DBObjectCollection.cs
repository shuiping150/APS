using System.Collections.Generic;

namespace APSV1.Model
{
    public class DBObjectCollection<T> : ICollection<T> where T : DBObject
    {
        private Dictionary<string, T> _objects = new Dictionary<string, T>();

        public virtual void Add(T item)
        {
            if (!_objects.ContainsKey(item._ID))
            {
                _objects.Add(item._ID, item);
            }
        }

        public virtual void Clear()
        {
            _objects.Clear();
        }

        public bool Contains(T item)
        {
            return _objects.ContainsKey(item._ID);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            int i = arrayIndex;
            foreach (var kvp in _objects)
            {
                array[i] = kvp.Value;
                i++;
            }
        }

        public int Count
        {
            get { return _objects.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public virtual bool Remove(T item)
        {
            return _objects.Remove(item._ID);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _objects.Values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _objects.Values.GetEnumerator();
        }

        public T GetByID(string id)
        {
            if (_objects.ContainsKey(id))
            {
                return _objects[id];
            }
            else
            {
                return null;
            }
        }

        public void AddRange(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                Add(item);
            }
        }
    }
}
