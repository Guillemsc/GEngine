using System;
using System.Collections.Generic;
using GEngine.Utils.Enums.Utils;

namespace GEngine.Utils.ActiveSource
{
    public sealed class EnumActiveSource<T> : IEnumActiveSource<T> where T : Enum
    {
        public event Action<T, bool> OnActiveChanged;

        readonly T[] _allValues;
        readonly Dictionary<object, List<T>> _blockedInputs = new ();

        public EnumActiveSource()
        {
            _allValues = EnumInfo<T>.Values;
        }

        public bool IsActive(T type)
        {
            foreach (List<T> blockedInput in _blockedInputs.Values)
            {
                bool isBlocked = blockedInput.Contains(type);

                if (!isBlocked)
                {
                    continue;
                }

                return false;
            }

            return true;
        }

        public void SetActive(object owner, T type, bool active)
        {
            bool wasActive = IsActive(type);

            bool ownerFound = _blockedInputs.TryGetValue(owner, out List<T> ownerBlockedInputs);

            if (!ownerFound)
            {
                ownerBlockedInputs = new List<T>();
                _blockedInputs.Add(owner, ownerBlockedInputs);
            }

            if (active)
            {
                ownerBlockedInputs.Remove(type);
            }
            else
            {
                bool alreadyBlocked = ownerBlockedInputs.Contains(type);

                if (!alreadyBlocked)
                {
                    ownerBlockedInputs.Add(type);
                }
            }

            bool isActive = IsActive(type);

            if (wasActive == isActive)
            {
                return;
            }

            OnActiveChanged?.Invoke(type, isActive);
        }

        public void DeactivateAll(object owner)
        {
            SetActiveAll(owner, false);
        }

        public void ActivateAll(object owner)
        {
            SetActiveAll(owner, true);
        }

        public void SetActiveAll(
            object owner,
            bool active)
        {
            foreach (T value in _allValues)
            {
                SetActive(owner, value, active);
            }
        }
    }
}
