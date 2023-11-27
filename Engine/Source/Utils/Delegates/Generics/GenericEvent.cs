namespace GEngine.Utils.Delegates.Generics
{
    /// <summary>
    /// Generic delegate that requieres the sender and the event arguments.
    /// </summary>
    /// <param name="sender">The object raising the event.</param>
    /// <param name="eventArgs">The arguments being raised for this event.</param>
    public delegate void GenericEvent<TSender, TEventArgs>(TSender sender, TEventArgs eventArgs);
}
