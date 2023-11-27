namespace GEngine.Utils.Types
{
    /// <summary>
    /// Normally used in combination with <see cref="True"/>.
    /// Useful when used with discriminated unions.
    /// </summary>
    public sealed class False
    {
        public static readonly False Instance = new ();

        False() { }
    }
}
