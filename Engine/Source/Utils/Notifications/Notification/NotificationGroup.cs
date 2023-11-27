using System.Collections.Generic;
using GEngine.Utils.Events;
using GEngine.Utils.Notifying.EventArgs;

namespace GEngine.Utils.Notifying.Notifications
{
    /// <inheritdoc />
    public sealed class NotificationGroup : INotificationGroup
    {
        readonly List<string> _raisedNotifications = new();
        readonly List<string> _foreverConsumedNotifications = new();

        readonly IEvent<bool> _activeStateChangedEvent = new Event<bool>();
        readonly IEvent<string> _anyRaisedEvent = new Event<string>();
        readonly IEvent<AnyConsumedEventArgs> _anyConsumedEvent = new Event<AnyConsumedEventArgs>();

        public bool AnyRaised => _raisedNotifications.Count > 0;
        public int RaisedCount => _raisedNotifications.Count;

        public IReadOnlyList<string> RaisedNotifications => _raisedNotifications;
        public IReadOnlyList<string> ForeverConsumedNotifications => _foreverConsumedNotifications;

        public IListenEvent<bool> AnyRaisedStateChangedEvent => _activeStateChangedEvent;
        public IListenEvent<string> AnyRaisedEvent => _anyRaisedEvent;
        public IListenEvent<AnyConsumedEventArgs> AnyConsumedEvent => _anyConsumedEvent;

        public NotificationGroup(IReadOnlyList<string> raisedNotifications, IReadOnlyList<string> foreverConsumedNotifications)
        {
            _raisedNotifications.AddRange(raisedNotifications);
            _foreverConsumedNotifications.AddRange(foreverConsumedNotifications);
        }

        public NotificationGroup()
        {

        }

        public bool IsRaised(string id)
        {
            return _raisedNotifications.Contains(id);
        }

        public void Raise(string id)
        {
            bool consumedForEver = _foreverConsumedNotifications.Contains(id);

            if (consumedForEver)
            {
                return;
            }

            bool alreadyRaised = _raisedNotifications.Contains(id);

            if (alreadyRaised)
            {
                return;
            }

            bool wasActive = AnyRaised;

            _raisedNotifications.Add(id);

            _anyRaisedEvent.Raise(id);

            if (wasActive == AnyRaised)
            {
                return;
            }

            _activeStateChangedEvent.Raise(AnyRaised);
        }

        public bool Consume(string id, bool forever)
        {
            bool wasActive = AnyRaised;

            bool removed = _raisedNotifications.Remove(id);

            if (!removed)
            {
                return false;
            }

            if (forever)
            {
                _foreverConsumedNotifications.Add(id);
            }

            _anyConsumedEvent.Raise(new AnyConsumedEventArgs(id, forever));

            if (wasActive == AnyRaised)
            {
                return true;
            }

            _activeStateChangedEvent.Raise(AnyRaised);

            return true;
        }

        public void ConsumeAll()
        {
            for (int i = _raisedNotifications.Count - 1; i >= 0; --i)
            {
                string raisedNotification = _raisedNotifications[i];

                Consume(raisedNotification, false);
            }
        }
    }
}
