using System;
using System.Collections.Generic;

namespace GEngine.Utils.ActiveSource
{
    [Obsolete("Use IdBlockedInput<T> instead")]
    public sealed class GuidActiveSource : IGuidActiveSource
    {
        public event Action<Guid, bool> OnActiveChanged;

        readonly IReadOnlyList<Guid> _allGuids;
        readonly Dictionary<object, List<Guid>> _blockedInputs = new ();

        public GuidActiveSource(IReadOnlyList<Guid> allGuids)
        {
            _allGuids = allGuids;
        }

        public bool IsActive(Guid uid)
        {
            foreach (List<Guid> blockedInput in _blockedInputs.Values)
            {
                bool isBlocked = blockedInput.Contains(uid);

                if (!isBlocked)
                {
                    continue;
                }

                return false;
            }

            return true;
        }

        public void SetActive(object owner, Guid uid, bool active)
        {
            bool wasActive = IsActive(uid);

            bool ownerFound = _blockedInputs.TryGetValue(owner, out List<Guid> ownerBlockedInputs);

            if (!ownerFound)
            {
                ownerBlockedInputs = new List<Guid>();
                _blockedInputs.Add(owner, ownerBlockedInputs);
            }

            if (active)
            {
                ownerBlockedInputs.Remove(uid);
            }
            else
            {
                bool alreadyBlocked = ownerBlockedInputs.Contains(uid);

                if (!alreadyBlocked)
                {
                    ownerBlockedInputs.Add(uid);
                }
            }

            bool isActive = IsActive(uid);

            if (wasActive == isActive)
            {
                return;
            }

            OnActiveChanged?.Invoke(uid, isActive);
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
            foreach (Guid guid in _allGuids)
            {
                SetActive(owner, guid, active);
            }
        }
    }
}
