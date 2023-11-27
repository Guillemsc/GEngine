using System;

namespace GEngine.Utils.ActiveSource
{
    public sealed class NopIdActiveSource<T> : IIdActiveSource<T>
    {
        public static readonly NopIdActiveSource<T> Instance = new();

#pragma warning disable 67
        public event Action<T, bool> OnActiveChanged;
#pragma warning restore 67

        public bool IsActive(
            T id)
        {
            return true;
        }

        public void SetActive(
            object owner,
            T id,
            bool active)
        {
        }

        public void Track(
            T id)
        {
        }

        public void Untrack(
            T id)
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
