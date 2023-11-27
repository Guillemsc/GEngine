using System;
using System.Collections.Generic;

namespace GEngine.Utils.Notifying.Serialization
{
    [Serializable]
    public sealed class SerializableNotificationData
    {
        public Dictionary<string, List<string>> RaisedNotifications = new();
        public Dictionary<string, List<string>> ForeverConsumedNotifications = new();
    }
}
