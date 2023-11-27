using GEngine.Utils.Events;
using GEngine.Utils.Notifying.EventArgs;

namespace GEngine.Utils.Notifying.Notifications
{
    /// <summary>
    /// Represents a notification group that listens for raised notifications and provides information about their status.
    /// </summary>
    /// <remarks>
    /// A notification group is just a container of string ids (notifications), where they can be added (raised), or removed (consumed).
    /// </remarks>
    public interface IListenNotificationGroup
    {
        /// <summary>
        /// Gets a value indicating whether any notification in the group has been raised.
        /// </summary>
        bool AnyRaised { get; }

        /// <summary>
        /// Gets the number of notifications that have been raised in the group.
        /// </summary>
        int RaisedCount { get; }

        /// <summary>
        /// Checks if a notification with the specified ID has been raised in the group.
        /// </summary>
        /// <param name="id">The ID of the notification to check.</param>
        /// <returns>True if the notification has been raised, false otherwise.</returns>
        bool IsRaised(string id);

        /// <summary>
        /// Event that is triggered when the <see cref="AnyRaised"/> state changes.
        /// </summary>
        IListenEvent<bool> AnyRaisedStateChangedEvent { get; }

        /// <summary>
        /// Event that is triggered when any notification in the group is raised.
        /// </summary>
        IListenEvent<string> AnyRaisedEvent { get; }

        /// <summary>
        /// Event that is triggered when any notification in the group is consumed (handled).
        /// </summary>
        IListenEvent<AnyConsumedEventArgs> AnyConsumedEvent { get; }
    }
}
