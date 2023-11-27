using System;
using System.Collections.Generic;

namespace GEngine.Utils.Events
{
    public class Event<T> : IEvent<T>
    {
        readonly List<Action<T>> _events = new();
        readonly List<Action<T>> _eventsCache = new();

        public int ListenerCount => _events.Count;

        public event Action<T> OnEvent
        {
            add => AddListener(value);
            remove => RemoveListener(value);
        }

        public void Raise(T data)
        {
            _eventsCache.Clear();
            _eventsCache.AddRange(_events);
            foreach (var action in _eventsCache)
            {
                action.Invoke(data);
            }
            _eventsCache.Clear();
        }

        public void AddListener(Action<T> listener)
        {
            _events.Add(listener);
        }

        public void RemoveListener(Action<T> listener)
        {
            _events.Remove(listener);
        }

        public void ClearListeners()
        {
            _events.Clear();
        }
    }
}
