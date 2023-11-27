using System;
using System.Collections.Generic;

namespace GEngine.Utils.ActiveSource
{
    public sealed class IndexActiveSource : IIndexActiveSource
    {
        public event Action<int, bool> OnActiveStateChanged;

        readonly Dictionary<object, List<int>> _blockedInputs = new ();
        readonly int _maxIndex;


        public IndexActiveSource(int maxIndex)
        {
            _maxIndex = maxIndex;
        }

        public bool IsActive(int index)
        {
            foreach (List<int> blockedInput in _blockedInputs.Values)
            {
                bool isBlocked = blockedInput.Contains(index);

                if (!isBlocked)
                {
                    continue;
                }

                return false;
            }

            return true;
        }

        public void SetActive(object owner, int index, bool active)
        {
            bool wasActive = IsActive(index);

            bool ownerFound = _blockedInputs.TryGetValue(owner, out List<int> ownerBlockedInputs);

            if (!ownerFound)
            {
                ownerBlockedInputs = new List<int>();
                _blockedInputs.Add(owner, ownerBlockedInputs);
            }

            if (active)
            {
                ownerBlockedInputs.Remove(index);
            }
            else
            {
                bool alreadyBlocked = ownerBlockedInputs.Contains(index);

                if (!alreadyBlocked)
                {
                    ownerBlockedInputs.Add(index);
                }
            }

            bool isActive = IsActive(index);

            if (wasActive == isActive)
            {
                return;
            }

            OnActiveStateChanged?.Invoke(index, isActive);
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
            for (int i = 0; i <= _maxIndex; ++i)
            {
                SetActive(owner, i, active);
            }
        }
    }
}
