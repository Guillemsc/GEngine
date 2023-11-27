using System;
using System.Collections.Generic;

namespace GEngine.Utils.DynamicVisitor
{
    public sealed class ByAssignableTypeStore<T>
    {
        readonly Dictionary<Type, T> _values = new ();

        public bool TryGetAction(
            Type type,
            out T value)
        {
            if (_values.TryGetValue(type, out value))
            {
                return true;
            }

            if (!TryGetAssignableKeyType(type, out var assignableKeyType))
            {
                return false;
            }

            return _values.TryGetValue(assignableKeyType, out value);
        }

        bool TryGetAssignableKeyType(Type type, out Type assignableKeyType)
        {
            foreach (var keyValuePair in _values)
            {
                if (keyValuePair.Key.IsAssignableFrom(type))
                {
                    assignableKeyType = keyValuePair.Key;
                    return true;
                }
            }

            assignableKeyType = default;
            return false;
        }

        public T this[Type key]
        {
            get => _values[key];
            set => _values[key] = value;
        }
    }
}
