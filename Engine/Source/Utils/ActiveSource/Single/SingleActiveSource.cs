using System;
using System.Collections.Generic;

namespace GEngine.Utils.ActiveSource
{
    public sealed class SingleActiveSource : ISingleActiveSource
    {
        public event Action<bool> OnActiveChanged;

        readonly HashSet<object> _blockedInputs = new ();

        public bool IsActive()
        {
            return _blockedInputs.Count == 0;
        }

        public void SetActive(object owner,  bool active)
        {
            bool wasActive = IsActive();

            if (active)
            {
                _blockedInputs.Remove(owner);
            }
            else
            {
                _blockedInputs.Add(owner);
            }

            bool isActive = IsActive();

            if (wasActive == isActive)
            {
                return;
            }

            OnActiveChanged?.Invoke(isActive);
        }

        public void DeactivateAll(object owner)
        {
            SetActive(owner, false);
        }

        public void ActivateAll(object owner)
        {
            SetActive(owner, true);
        }

        public void SetActiveAll(
            object owner,
            bool active)
        {
            SetActive(owner, active);
        }
    }
}
