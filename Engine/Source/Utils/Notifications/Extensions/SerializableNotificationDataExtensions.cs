using System;
using System.Collections.Generic;

namespace GEngine.Utils.Notifying.Serialization
{
    public static class SerializableNotificationDataExtensions
    {
        public static void SetNotificationsData(
            this SerializableNotificationData serializableNotificationData,
            string notificationName,
            IReadOnlyList<string> raisedNotifications,
            IReadOnlyList<string> forEverConsumedNotifications
            )
        {
            serializableNotificationData.RaisedNotifications.Remove(notificationName);
            serializableNotificationData.RaisedNotifications.Add(notificationName, new List<string>(raisedNotifications));

            serializableNotificationData.ForeverConsumedNotifications.Remove(notificationName);
            serializableNotificationData.ForeverConsumedNotifications.Add(notificationName, new List<string>(forEverConsumedNotifications));
        }

        public static void AddRaisedNotification(
            this SerializableNotificationData serializableNotificationData,
            string notificationName,
            string id
            )
        {
            bool found = serializableNotificationData.RaisedNotifications.TryGetValue(notificationName, out List<string> raisedNotifications);

            if (!found)
            {
                raisedNotifications = new List<string>();
                serializableNotificationData.RaisedNotifications.Add(notificationName, raisedNotifications);
            }

            raisedNotifications.Add(id);
        }

        public static void RemoveRaisedNotification(
            this SerializableNotificationData serializableNotificationData,
            string notificationName,
            string id
        )
        {
            bool found = serializableNotificationData.RaisedNotifications.TryGetValue(notificationName, out List<string> raisedNotifications);

            if (!found)
            {
                return;
            }

            raisedNotifications.Remove(id);
        }

        public static void AddForEverConsumedNotification(
            this SerializableNotificationData serializableNotificationData,
            string notificationName,
            string id
        )
        {
            bool found = serializableNotificationData.ForeverConsumedNotifications.TryGetValue(notificationName, out List<string> forEverConsumedNotifications);

            if (!found)
            {
                forEverConsumedNotifications = new List<string>();
                serializableNotificationData.ForeverConsumedNotifications.Add(notificationName, forEverConsumedNotifications);
            }

            forEverConsumedNotifications.Add(id);
        }

        public static IReadOnlyList<string> GetRaisedNotificationsData(this SerializableNotificationData serializableNotificationData, string notificationName)
        {
            bool found = serializableNotificationData.RaisedNotifications.TryGetValue(notificationName, out List<string> raisedNotifications);

            if (!found)
            {
                return Array.Empty<string>();
            }

            return raisedNotifications;
        }

        public static IReadOnlyList<string> GetForeverConsumedNotificationsData(this SerializableNotificationData serializableNotificationData, string notificationName)
        {
            bool found = serializableNotificationData.ForeverConsumedNotifications.TryGetValue(notificationName, out List<string> raisedNotifications);

            if (!found)
            {
                return Array.Empty<string>();
            }

            return raisedNotifications;
        }
    }
}
