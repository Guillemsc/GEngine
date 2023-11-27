namespace GEngine.Utils.Types
{
    /// <summary>
    /// Normally used in combination with <see cref="Yes"/> and <see cref="Maybe"/>.
    /// Useful when used with discriminated unions.
    /// </summary>
    public sealed class No
    {
        public static readonly No Instance = new ();

        No() { }
    }
}
