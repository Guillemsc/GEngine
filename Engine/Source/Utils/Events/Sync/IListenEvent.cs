using System;

namespace GEngine.Utils.Events
{
    /// <summary>
    /// An event that can be listened to.
    /// </summary>
    public interface IListenEvent<out T>
    {
        event Action<T> OnEvent;

        void AddListener(Action<T> listener);
        void RemoveListener(Action<T> listener);
        void ClearListeners();
    }
}
