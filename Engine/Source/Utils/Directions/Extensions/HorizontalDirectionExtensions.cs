namespace GEngine.Utils.Directions
{
    public static class HorizontalDirectionExtensions
    {
        /// <summary>
        /// From <see cref="HorizontalDirection"/> if it's left, returns -1, and if it's right returns 1.
        /// </summary>
        public static float ToFloat(this HorizontalDirection direction)
        {
            return direction == HorizontalDirection.Left ? -1f : 1f;
        }
    }
}
