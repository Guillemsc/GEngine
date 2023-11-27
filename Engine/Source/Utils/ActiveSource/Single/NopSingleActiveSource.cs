using System;

namespace GEngine.Utils.ActiveSource
{
    public sealed class NopSingleActiveSource : ISingleActiveSource
    {
        public static readonly NopSingleActiveSource Instance = new NopSingleActiveSource();

#pragma warning disable 67
        public event Action<bool> OnActiveChanged;
#pragma warning restore 67

        NopSingleActiveSource()
        {
        }

        public bool IsActive()
        {
            return true;
        }

        public void SetActive(object owner,  bool active)
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
