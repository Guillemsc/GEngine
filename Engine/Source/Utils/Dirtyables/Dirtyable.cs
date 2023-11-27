namespace GEngine.Utils.Dirtyables
{
    public sealed class Dirtyable<T> : IDirtyable<T>
    {
        public bool IsDirty { get; private set; }
        public T Value { get; private set; }

        public void SetValue(T value, bool setDirty = true)
        {
            if (setDirty)
            {
                IsDirty = true;
            }

            Value = value;
        }

        public void Clean()
        {
            IsDirty = false;
        }
    }
}
