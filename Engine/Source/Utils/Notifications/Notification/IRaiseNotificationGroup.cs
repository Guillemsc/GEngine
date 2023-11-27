namespace GEngine.Utils.Notifying.Notifications
{
    /// <summary>
    /// Represents a notification group that can raise notifications.
    /// </summary>
    /// <remarks>
    /// A notification group is just a container of string ids (notifications), where they can be added (raised), or removed (consumed).
    /// </remarks>
    public interface IRaiseNotificationGroup
    {
        /// <summary>
        /// Raises the notification id if it was not already raised.
        /// </summary>
        void Raise(string id);
    }
}
