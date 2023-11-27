namespace GEngine.Utils.Types
{
    /// <summary>
    /// Normally used in combination with <see cref="Yes"/> and <see cref="No"/>.
    /// Useful when used with discriminated unions.
    /// </summary>
    public sealed class Maybe
    {
        public static readonly Maybe Instance = new ();

        Maybe() { }
    }
}
