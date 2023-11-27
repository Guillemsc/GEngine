using System;
using System.Collections.Generic;

namespace GEngine.Utils.Repositories
{
    /// <inheritdoc />
    public class KeyValueRepository<TId, TObject> : IKeyValueRepository<TId, TObject>
    {
        readonly Dictionary<TId, TObject> _items = new();

        public IEnumerable<KeyValuePair<TId, TObject>> Items => _items;
        public IEnumerable<TId> Keys => _items.Keys;
        public IEnumerable<TObject> Values => _items.Values;
        public int Count => _items.Count;

        public void Add(TId id, TObject obj)
        {
            _items.Add(id, obj);
        }

        public void Set(TId id, TObject obj)
        {
            _items[id] = obj;
        }

        public bool Remove(TId id)
        {
            return _items.Remove(id);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool Contains(TId id)
        {
            return _items.ContainsKey(id);
        }

        public bool TryGet(TId id, out TObject obj)
        {
            return _items.TryGetValue(id, out obj);
        }

        public TObject Get(TId id)
        {
            if (!TryGet(id, out var obj))
            {
                throw new KeyNotFoundException();
            }

            return obj;
        }
    }
}
