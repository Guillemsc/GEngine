namespace GEngine.Utils.Types
{
    /// <summary>
    /// Useful when used with discriminated unions.
    /// </summary>
    public sealed class NotFound
    {
        public static readonly NotFound Instance = new ();

        NotFound() { }
    }
}
