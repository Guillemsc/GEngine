namespace GEngine.Utils.Notifying.EventArgs
{
    public readonly struct AnyConsumedEventArgs
    {
        public string Id { get; }
        public bool ForEver { get; }

        public AnyConsumedEventArgs(
            string id,
            bool forEver
            )
        {
            Id = id;
            ForEver = forEver;
        }
    }
}
