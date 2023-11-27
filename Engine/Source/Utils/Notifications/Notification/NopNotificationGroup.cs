using GEngine.Utils.Events;
using GEngine.Utils.Notifying.EventArgs;

namespace GEngine.Utils.Notifying.Notifications
{
    public sealed class NopNotificationGroup : INotificationGroup
    {
        public static readonly NopNotificationGroup Instance = new();

        public bool AnyRaised => false;
        public int RaisedCount => 0;

        public IListenEvent<bool> AnyRaisedStateChangedEvent => NopEvent<bool>.Instance;
        public IListenEvent<string> AnyRaisedEvent => NopEvent<string>.Instance;
        public IListenEvent<AnyConsumedEventArgs> AnyConsumedEvent => NopEvent<AnyConsumedEventArgs>.Instance;

        NopNotificationGroup()
        {

        }

        public void Raise(string id) { }
        public bool Consume(string id, bool forever) => false;

        public void ConsumeAll() { }
        public bool IsRaised(string id) => false;
    }
}
