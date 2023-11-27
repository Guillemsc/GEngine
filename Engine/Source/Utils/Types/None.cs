namespace GEngine.Utils.Types
{
    /// <summary>
    /// Normally used in combination with <see cref="Some"/>.
    /// Useful when used with discriminated unions.
    /// </summary>
    public sealed class None
    {
        public static readonly None Instance = new ();

        None() { }
    }
}
