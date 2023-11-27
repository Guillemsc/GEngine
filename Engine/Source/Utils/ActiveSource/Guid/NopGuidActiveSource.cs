using System;

namespace GEngine.Utils.ActiveSource
{
    [Obsolete("Use IdBlockedInput<T> instead")]
    public sealed class NopGuidActiveSource : IGuidActiveSource
    {
        public static readonly NopGuidActiveSource Instance = new NopGuidActiveSource();

#pragma warning disable 67
        public event Action<Guid, bool> OnActiveChanged;
#pragma warning restore 67

        NopGuidActiveSource()
        {
        }

        public bool IsActive(Guid uid)
        {
            return true;
        }

        public void SetActive(object owner, Guid uid, bool active)
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
