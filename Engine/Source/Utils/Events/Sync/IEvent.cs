namespace GEngine.Utils.Events
{
    /// <summary>
    /// Similar to a C# event <see cref="System.Action"/>, but it can be passed around.
    /// It can also be referenced either as an <see cref="IListenEvent{T}"/> or <see cref="IRaiseEvent{T}"/>.
    /// </summary>
    public interface IEvent<T> : IListenEvent<T>, IRaiseEvent<T>
    {
    }
}
