using System;

namespace GEngine.Utils.Events
{
    public sealed class NopEvent<T> : IEvent<T>
    {
        public static readonly NopEvent<T> Instance = new();

        public int ListenerCount => 0;

        NopEvent() { }

#pragma warning disable 67 // Event not used
        public event Action<T> OnEvent;
#pragma warning restore 67

        public void AddListener(Action<T> listener) { }
        public void RemoveListener(Action<T> listener) { }
        public void ClearListeners() { }
        public void Raise(T data) { }
    }
}
