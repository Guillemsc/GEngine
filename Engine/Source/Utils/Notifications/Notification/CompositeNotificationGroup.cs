using GEngine.Utils.Caching.Cache;
using GEngine.Utils.Events;
using GEngine.Utils.Notifying.EventArgs;

namespace GEngine.Utils.Notifying.Notifications
{
    /// <inheritdoc />
    /// <summary>
    /// Same as a regural <see cref="NotificationGroup"/>, but you can listen to all their combined notifications.
    /// </summary>
    public sealed class CompositeNotificationGroup : IListenNotificationGroup
    {
        readonly IEvent<bool> _activeStateChanged = new Event<bool>();
        readonly IEvent<string> _anyRaisedEvent = new Event<string>();
        readonly IEvent<AnyConsumedEventArgs> _anyConsumedEvent = new Event<AnyConsumedEventArgs>();

        readonly CachedResult<int> _getActiveCountCachedResult;
        readonly IListenNotificationGroup[] _notifications;

        bool _previousActiveState;

        public bool AnyRaised => GetActiveCount() > 0;
        public int RaisedCount => _getActiveCountCachedResult.Get();

        public IListenEvent<bool> AnyRaisedStateChangedEvent => _activeStateChanged;
        public IListenEvent<string> AnyRaisedEvent => _anyRaisedEvent;
        public IListenEvent<AnyConsumedEventArgs> AnyConsumedEvent => _anyConsumedEvent;

        public CompositeNotificationGroup(params IListenNotificationGroup[] notifications)
        {
            _getActiveCountCachedResult = new CachedResult<int>(GetActiveCount);
            _notifications = notifications;

            foreach (IListenNotificationGroup notification in _notifications)
            {
                RegisterNotification(notification);
            }

            _previousActiveState = AnyRaised;
        }

        public bool IsRaised(string id)
        {
            foreach (IListenNotificationGroup notification in _notifications)
            {
                bool isRaised = notification.IsRaised(id);

                if (isRaised)
                {
                    return true;
                }
            }

            return false;
        }

        int GetActiveCount()
        {
            int count = 0;

            foreach (IListenNotificationGroup notification in _notifications)
            {
                if (notification.AnyRaised)
                {
                    ++count;
                }
            }

            return count;
        }

        void RegisterNotification(IListenNotificationGroup notificationGroup)
        {
            notificationGroup.AnyRaisedStateChangedEvent.AddListener(AnyNotificationStateChanged);
            notificationGroup.AnyRaisedEvent.AddListener(_anyRaisedEvent.Raise);
            notificationGroup.AnyConsumedEvent.AddListener(_anyConsumedEvent.Raise);
        }

        void AnyNotificationStateChanged(bool state)
        {
            _getActiveCountCachedResult.ClearCache();

            if (_previousActiveState == AnyRaised)
            {
                return;
            }

            _previousActiveState = AnyRaised;

            _activeStateChanged.Raise(AnyRaised);
        }
    }
}
