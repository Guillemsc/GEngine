namespace GEngine.Utils.Notifying.Notifications
{
    /// <summary>
    /// Container of string ids, where they can be added (raised), or removed (consumed).
    /// </summary>
    public interface INotificationGroup :
        IRaiseNotificationGroup,
        IConsumeNotificationGroup,
        IListenNotificationGroup
    {
    }
}
