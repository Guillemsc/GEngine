namespace GEngine.Utils.Notifying.Notifications
{
    /// <summary>
    /// Represents a notification group that can consume notifications.
    /// </summary>
    /// <remarks>
    /// A notification group is just a container of string ids (notifications), where they can be added (raised), or removed (consumed).
    /// </remarks>
    public interface IConsumeNotificationGroup
    {
        /// <summary>
        /// Consumes the specified notification ID if it had been raised previously.
        /// </summary>
        /// <param name="id">The ID of the notification to consume.</param>
        /// <param name="forever">Specifies whether the notification should be consumed forever (default: false).
        /// If a notification gets consumed forever it means that it won't be able to be raised again.</param>
        /// <returns>True if the notification was consumed, false otherwise.</returns>
        bool Consume(string id, bool forever = false);

        /// <summary>
        /// Consumes all notifications in the group.
        /// </summary>
        void ConsumeAll();
    }
}
