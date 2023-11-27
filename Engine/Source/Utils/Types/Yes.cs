namespace GEngine.Utils.Types
{
    /// <summary>
    /// Normally used in combination with <see cref="No"/> and <see cref="Maybe"/>.
    /// Useful when used with discriminated unions.
    /// </summary>
    public sealed class Yes
    {
        public static readonly Yes Instance = new ();

        Yes() { }
    }
}
