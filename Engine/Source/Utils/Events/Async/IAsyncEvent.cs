namespace GEngine.Utils.Events
{
    /// <summary>
    /// Similar to a <see cref="System.Action"/>, but it can be passed around.
    /// It can also be passed arround either as an <see cref="IListenEvent{T}"/> or <see cref="IRaiseEvent{T}"/>.
    /// </summary>
    public interface IAsyncEvent<T> : IListenAsyncEvent<T>, IRaiseAsyncEvent<T>
    {
        new IEvent<T> OnBeforeRaise { get; }
    }
}
