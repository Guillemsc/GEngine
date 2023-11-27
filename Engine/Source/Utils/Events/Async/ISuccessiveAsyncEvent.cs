namespace GEngine.Utils.Events
{
    /// <summary>
    /// Async implementation of <see cref="IEvent{T}"/> where async listeners are called one after the other
    /// </summary>
    public interface ISuccessiveAsyncEvent<T> : IAsyncEvent<T>
    {
    }
}
