using System.Collections.Generic;

namespace GEngine.Utils.Repositories
{
    /// <inheritdoc />
    public class ListRepository<TObject> : IListRepository<TObject>
    {
        readonly List<TObject> _items = new();

        public int Count => _items.Count;
        public IReadOnlyList<TObject> Items => _items;

        public void Add(TObject obj)
        {
            _items.Add(obj);
        }

        public void Insert(TObject obj, int index)
        {
            _items.Insert(index, obj);
        }

        public bool Remove(TObject obj)
        {
            return _items.Remove(obj);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool Contains(TObject obj)
        {
            return _items.Contains(obj);
        }
    }
}
