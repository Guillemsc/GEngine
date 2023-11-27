namespace GEngine.Utils.Events
{
    /// <summary>
    /// An event that can be raised.
    /// </summary>
    public interface IRaiseEvent<in T>
    {
        public int ListenerCount { get; }

        /// <summary>
        /// Invokes the event with some arguments.
        /// </summary>
        void Raise(T data);
    }
}
