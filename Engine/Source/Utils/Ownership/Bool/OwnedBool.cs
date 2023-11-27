using System;
using System.Collections.Generic;

namespace GEngine.Utils.Ownership.Bool
{
    [Obsolete]
    public sealed class OwnedBool : IOwnedBool
    {
        readonly List<object> _owners = new();

        public event Action<bool> OnChanged;

        public bool Value { get; private set; }

        public bool Set(bool value, object owner)
        {
            bool wasActive = _owners.Count > 0;

            if (value)
            {
                bool alreadyAdded = _owners.Contains(owner);

                if (!alreadyAdded)
                {
                    _owners.Add(owner);
                }
            }
            else
            {
                _owners.Remove(owner);
            }

            bool isActive = _owners.Count > 0;

            if (isActive == wasActive)
            {
                return false;
            }

            Value = value;

            OnChanged?.Invoke(value);

            return true;
        }
    }
}
