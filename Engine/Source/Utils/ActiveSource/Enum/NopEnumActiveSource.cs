using System;

namespace GEngine.Utils.ActiveSource
{
    public sealed class NopEnumActiveSource<T> : IEnumActiveSource<T> where T : Enum
    {
        public static readonly NopEnumActiveSource<T> Instance = new();

#pragma warning disable 67
        public event Action<T, bool> OnActiveChanged;
#pragma warning restore 67

        NopEnumActiveSource()
        {
        }

        public bool IsActive(T type)
        {
            return true;
        }

        public void SetActive(object owner, T type, bool active)
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
