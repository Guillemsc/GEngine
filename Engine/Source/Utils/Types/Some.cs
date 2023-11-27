namespace GEngine.Utils.Types
{
    /// <summary>
    /// Normally used in combination with <see cref="None"/>.
    /// Useful when used with discriminated unions.
    /// </summary>
    public sealed class Some
    {
        public static readonly Some Instance = new ();

        Some() { }
    }
}
