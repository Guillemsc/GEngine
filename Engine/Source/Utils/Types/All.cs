namespace GEngine.Utils.Types
{
    /// <summary>
    /// Normally used in combination with <see cref="Nothing"/>.
    /// Useful when used with discriminated unions.
    /// </summary>
    public sealed class All
    {
        public static readonly All Instance = new ();

        All() { }
    }
}
