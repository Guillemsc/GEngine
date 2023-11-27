using System;
using System.Collections.Generic;

namespace GEngine.Utils.Repositories
{
    /// <inheritdoc />
    public class SingleRepository<TObject> : ISingleRepository<TObject>
    {
        TObject _item;

        public bool HasValue { get; private set; }

        public void Set(TObject obj)
        {
            _item = obj;
            HasValue = true;
        }

        public void Clear()
        {
            _item = default;
            HasValue = false;
        }

        public bool TryGet(out TObject obj)
        {
            obj = _item;
            return HasValue;
        }

        public TObject Get()
        {
            return GetUnsafe();
        }

        public TObject GetUnsafe()
        {
            if (!TryGet(out var obj))
            {
                throw new InvalidOperationException();
            }

            return obj;
        }

        public bool Contains(TObject obj)
        {
            if (!HasValue)
            {
                return false;
            }

            return EqualityComparer<TObject>.Default.Equals(_item, obj);
        }
    }
}
