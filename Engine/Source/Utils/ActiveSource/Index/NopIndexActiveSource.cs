using System;

namespace GEngine.Utils.ActiveSource
{
    public sealed class NopIndexActiveSource : IIndexActiveSource
    {
        public static readonly NopIndexActiveSource Instance = new NopIndexActiveSource();

#pragma warning disable 67
        public event Action<int, bool> OnActiveStateChanged;
#pragma warning restore 67

        NopIndexActiveSource()
        {
        }

        public bool IsActive(int index)
        {
            return true;
        }

        public void SetActive(object owner, int index, bool active)
        {
        }

        public void SetBlocked(int index, bool blocked)
        {
        }

        public void DeactivateAll(object owner)
        {
        }

        public void ActivateAll(object owner)
        {
        }

        public void SetActiveAll(
            object owner,
            bool active)
        {
        }
    }
}
