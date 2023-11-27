namespace GEngine.Utils.Types
{
    /// <summary>
    /// Represents nothing. Useful when you need to pass or return a dummy object.
    /// </summary>
    public sealed class Nothing
    {
        public static readonly Nothing Instance = new();

        Nothing() { }
    }
}
